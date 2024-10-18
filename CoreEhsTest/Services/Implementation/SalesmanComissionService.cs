    using CoreEhsTest.Dtos;
using CoreEhsTest.Repositories.Contract;
using CoreEhsTest.Services.Contract;

namespace CoreEhsTest.Services.Implementation
{
    public class SalesmanComissionService:ISalesmanComissionService
    {
        private readonly ISalesmanComissionRepository _salesmanCommissionRepository;

        public SalesmanComissionService(ISalesmanComissionRepository salesmanCommissionRepository)
        {
            _salesmanCommissionRepository = salesmanCommissionRepository;
        }

        public IEnumerable<SalesComissionDto> GenerateReport()
        {
            return _salesmanCommissionRepository.GenerateReport();
        }
    }
}
