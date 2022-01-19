using Furlough.Models.Mapper;

namespace Furlough.BLL
{
    public class Request
    {
        private readonly DAL.Request _dalRequest;
        private readonly DAL.RequestHistory _dalRequestHistory;
        private readonly DalMapper _dalMapper;

        public Request(DalMapper dalMapper, DAL.Request dalRequest, DAL.RequestHistory dalRequestHistory)
        {
            _dalRequest = dalRequest;
            _dalRequestHistory = dalRequestHistory;

            _dalMapper = dalMapper;
        }

        public bool Edit(Models.Request request, DAL.Models.RequestHistory requestHistory)
        {
            try
            {
                var result = _dalRequestHistory.Add(requestHistory);
                return _dalRequest.Edit(_dalMapper.DalRequestMap(request));

            }
            catch (Exception e)
            {
                
                throw;
            }
        }
    }
}
