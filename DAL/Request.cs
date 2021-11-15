using Microsoft.Data.SqlClient;
using System.Data;

namespace Furlough.DAL
{
    public class Request
    {
        private readonly FurloughContext _context;

        public Request(FurloughContext context)
        {
            _context = context;
        }

        //Object mapper; reader to model
        public Models.Request Mapper(SqlDataReader reader)
        {
            return new Models.Request()
            {
                Id = reader.GetInt32("Id"),
                DateFrom = reader.GetDateTime("DateFrom"),
                DateUntil = reader.GetDateTime("DateUntil"),
                PaidDays = reader.GetInt32("PaidDays"),
                RequestedBy = reader.GetInt32("RequestedBy"),
                RequestedOn = reader.GetDateTime("RequestedOn")
                //StatusId instead of Status
            };
        }
    }
}
