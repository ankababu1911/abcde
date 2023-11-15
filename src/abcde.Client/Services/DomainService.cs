using abcde.Client.Services.Base;
using abcde.Client.Services.Interfaces;
using abcde.Model;
using abcde.Model.Base;

namespace abcde.Client.Services
{
    public class DomainService : BaseService<Domain, BaseSummary, BaseFilter>, IDomainService
    {
        private readonly string filter;
        private HttpClient _httpClient;
        public DomainService(HttpClient httpClient) : base(httpClient)
        {
            BaseResource = "Domains";
            _httpClient = httpClient;
        }
    }
}
