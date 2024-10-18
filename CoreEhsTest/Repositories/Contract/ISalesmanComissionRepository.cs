using CoreEhsTest.Dtos;

namespace CoreEhsTest.Repositories.Contract
{
    public interface ISalesmanComissionRepository
    {
        IEnumerable<SalesComissionDto> GenerateReport();
    }
}
