using Furlough.Models.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Furlough.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class HomeController : Controller
    {
        private readonly DAL.User _contextUser;
        private readonly DAL.Request _contextRequest;
        private readonly DAL.Employee _contextEmployee;
        private readonly DAL.Department _contextDepartment;
        private readonly DAL.RequestType _contextRequestType;
        private readonly DAL.AvailableDays _contextAvailableDays;
        private readonly DalMapper _dalMapper;
        private readonly Services.Mail.IMailService _mailService;

        public HomeController(DalMapper dalMapper, 
            Services.Mail.IMailService mailService,
            DAL.RequestType contextRequestType, DAL.Request contextRequest, DAL.AvailableDays contextAvailableDays,
            DAL.Department contextDepartment, DAL.Employee contextEmployee, DAL.User contextUser)
        {
            _contextUser = contextUser;
            _contextRequest = contextRequest;
            _contextEmployee = contextEmployee;
            _contextDepartment = contextDepartment;
            _contextRequestType = contextRequestType;
            _contextAvailableDays = contextAvailableDays;

            _dalMapper = dalMapper;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            var employeeId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Employee").Value);
            ViewBag.requestTypes = _contextRequestType.GetAll(); //.OrderBy(obj => obj.OrderNum); //stored procedure already returns it in order
            ViewBag.availableDays = _contextAvailableDays.GetByEmployeeId(employeeId);
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost(Name = "ChangePassword")]
        public IActionResult ChangePassword([FromForm]string OldPassword, [FromForm] string NewPassword)
        {
            var loggedinUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);
            var dbUser = _contextUser.GetById(loggedinUser, true);
            if (dbUser == null)
                return BadRequest("User does not exist");

            //give old password to PasswordHasher on init, it gets hashed, then give already hash-saved db password to check against in VerifyPassword method
            if (!new SecurityHandlers.PasswordHasher(OldPassword).VerifyPassword(dbUser.Password))
                return BadRequest("Wrong password");

            dbUser.Password = new SecurityHandlers.PasswordHasher(NewPassword).GetHashWithSalt();   //returns the new hashed password + the salt
            var edited = _contextUser.Edit(dbUser);

            return edited ? Ok("Password changed successfully") : StatusCode(500, "Something went wrong changing your password");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRequest(Models.Request request)
        {
            IActionResult result;

            try
            {
                var loggedinUser = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "User").Value);
                var employeeId = int.Parse(HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Employee").Value);
                var availableDays = _contextAvailableDays.GetByEmployeeId(employeeId);

                var employee = _contextEmployee.GetById(employeeId);
                var manager = _contextDepartment.GetDepartmentManager(employee.DepartmentId);
                //This gets the type's property dynamically using the type of the request,
                //after which we get that property from availableDays and get it's value
                var daysLeft = availableDays.GetType().GetProperty(_contextRequestType.GetById(request.RequestTypeId).Type).GetValue(availableDays);
                if (daysLeft == null) return BadRequest();

                //RequestTypeId = 8 is Unpaid Type
                if (request.RequestTypeId != 8 && (request.DaysAmount > (decimal)daysLeft || request.DaysAmount < 0))
                {
                    result = BadRequest("Not enough days left.");
                    return result;
                }

                request.RequestedByUserId = loggedinUser;
                request.LUBUserId = loggedinUser;


                //new value of days, adds instead of deducting if request type is unpaid (meaning unpaid leave days only increase, others decrease)
                var newDaysValue = (request.RequestTypeId == 8) ? (decimal)daysLeft + request.DaysAmount : (decimal)daysLeft - request.DaysAmount;

                using (var transScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    int requestId = _contextRequest.Add(_dalMapper.DalRequestMap(request));
                    bool dbResult = _contextAvailableDays    //deduct days left from requested paidDays
                        .SetDays(employeeId, _contextRequestType.GetById(request.RequestTypeId).Type, newDaysValue);

                    result = StatusCode(500, "Something went wrong while adding your request");

                    if (requestId > 0 && dbResult)
                    {
                        //Make sure you're either connected to ASSECO VPN or the building wifi
                        //employee email
                        await _mailService.SendEmailAsync(new Services.Mail.MailRequest(
                            employee.Email,
                            string.Format(Resources.Services.Mail.RequestCreate.employeeSubject, requestId),
                            string.Format(Resources.Services.Mail.RequestCreate.employeeBody,
                                employee.Name,
                                DateTime.Now,
                                request.Id,
                                Enum.GetName(typeof(Models.Enums.RequestType), request.RequestTypeId),
                                request.Dates,
                                request.DaysAmount,
                                employee.Email),
                            null));

                        //manager email
                        await _mailService.SendEmailAsync(new Services.Mail.MailRequest(
                            employee.Email,
                            string.Format(Resources.Services.Mail.RequestCreate.managerSubject, employee.Name),
                            string.Format(Resources.Services.Mail.RequestCreate.managerBody,
                                manager.Name,
                                employee.Name,
                                request.DaysAmount,
                                request.Dates,
                                requestId,
                                Enum.GetName(typeof(Models.Enums.RequestType), request.RequestTypeId),
                                manager.Email),
                            null));

                        result = Ok();
                        transScope.Complete();
                    }
                }
                
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();

                result = StatusCode(500);
                return result;
            }
             return result;
        }
    }
}
