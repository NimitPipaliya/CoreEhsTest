using CoreEhsTest.Dtos;
using CoreEhsTest.Models;
using CoreEhsTest.Repositories.Contract;
using CoreEhsTest.Services.Contract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Security.Claims;

namespace CoreEhsTest.Services.Implementation
{
    public class CarModelService: ICarModelService
    {
        private readonly ICarModelRepository _carModelRepository;

        public CarModelService(ICarModelRepository carModelRepository)
        {
            _carModelRepository = carModelRepository;
        }
        public ServiceResponse<List<CarModel>> GetAllCarModels(string ?searchTerm)
        {
                var response = new ServiceResponse<List<CarModel>>();
            try
            {
                var carModels = _carModelRepository.GetAllCarModels(searchTerm);
                var carModelList = new List<CarModel>();
                foreach (DataRow row in carModels.Rows)
                {
                    carModelList.Add(new CarModel
                    {
                        CarID = Convert.ToInt32(row["CarID"]),
                        Brand = row["Brand"].ToString(),
                        Class = row["Class"].ToString(),
                        ModelName = row["ModelName"].ToString(),
                        ModelCode = row["ModelCode"].ToString(),
                        Description = row["Description"].ToString(),
                        Features = row["Features"].ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        ManufactureDate = Convert.ToDateTime(row["ManufactureDate"]),
                        Active = Convert.ToBoolean(row["Active"]),
                        SortOrder = Convert.ToInt32(row["SortOrder"])
                    });
                }
              
                response.Data = carModelList;
                response.Message = "Car models retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error: {ex.Message}";
            }
            return response;
        }
        public ServiceResponse<string> AddCarModel(CarModelDto model, List<byte[]> imageDataList)
        {
            var response = new ServiceResponse<string>();

            try
            {
                ValidateCarModel(model);
                _carModelRepository.AddCarModel(model, imageDataList);
                response.Success = true;
                response.Message = "Car model inserted successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }
        public ServiceResponse<string> UpdateCarModelByCode(UpdateCarModelDto model, List<byte[]> imageDataList)
        {
            var response = new ServiceResponse<string>();

            try
            {
                _carModelRepository.UpdateCarModelByCode(model,imageDataList);

                response.Success = true;
                response.Message = "Car model updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }
        public ServiceResponse<string> DeleteCarModel(string ModelCode)
        {
            var response = new ServiceResponse<string>();

            try
            {
                _carModelRepository.DeleteCarModel(ModelCode);

                response.Success = true;
                response.Message = "Car model deleted successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        private void ValidateCarModel(CarModelDto model)
        {
            if (string.IsNullOrWhiteSpace(model.ModelName))
            {
                throw new ArgumentException("Model Name is required.");
            }

            if (string.IsNullOrWhiteSpace(model.ModelCode) || !IsAlphanumeric(model.ModelCode))
            {
                throw new ArgumentException("Model Code must be alphanumeric and not empty.");
            }

            if (model.Price <= 0)
            {
                throw new ArgumentException("Price must be greater than 0.");
            }

            if (model.ManufactureDate > DateTime.Now)
            {
                throw new ArgumentException("Manufacture Date cannot be in the future.");
            }
        }
        private bool IsAlphanumeric(string input)
        {
            return input.All(char.IsLetterOrDigit);
        }
    }
}
