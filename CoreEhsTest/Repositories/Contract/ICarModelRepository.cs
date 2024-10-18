using CoreEhsTest.Dtos;
using CoreEhsTest.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CoreEhsTest.Repositories.Contract
{
    public interface ICarModelRepository
    {
        DataTable GetAllCarModels(string ?searchTerm);
        int AddCarModel(CarModelDto model, List<byte[]> imageDataList);
        void UpdateCarModelByCode(UpdateCarModelDto model, List<byte[]> imageDataList);
        void DeleteCarModel(string modelCode);
    }
}
