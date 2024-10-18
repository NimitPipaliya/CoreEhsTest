using CoreEhsTest.Dtos;

namespace CoreEhsTest.Services.Contract
{
    public interface ISalesmanComissionService
    {
        IEnumerable<SalesComissionDto> GenerateReport();
    }
}
