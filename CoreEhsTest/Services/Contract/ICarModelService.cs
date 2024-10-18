using CoreEhsTest.Dtos;
using CoreEhsTest.Models;
using System.Data;

namespace CoreEhsTest.Services.Contract
{
    public interface ICarModelService
    {
        ServiceResponse<List<CarModel>> GetAllCarModels(string? searchTerm);
        ServiceResponse<string> AddCarModel(CarModelDto model, List<byte[]> imageDataList);
        ServiceResponse<string> UpdateCarModelByCode(UpdateCarModelDto model,List<byte[]> imageDataList);
        ServiceResponse<string> DeleteCarModel(string ModelCode);
    }
}
