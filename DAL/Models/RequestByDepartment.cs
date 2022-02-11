namespace Furlough.DAL.Models
{
    public class RequestByDepartment
    {
        public string EmployeeName { get; set; }
        public int EmployeePositionId { get; set; }
        public int EmployeeId { get; set; }
        public int RequestId { get; set; }
        public int RequestedByUserId { get; set; }
        public string RequestDates { get; set; }
        public DateTime RequestedOn { get; set; }
        public byte RequestStatusId { get; set; }
        public decimal RequestDaysAmount { get; set; }
    }
}
