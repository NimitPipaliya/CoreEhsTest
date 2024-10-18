using CoreEhsTest.Dtos;
using CoreEhsTest.Models;
using CoreEhsTest.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreEhsTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private readonly ICarModelService _carModelService;

        public CarModelController(ICarModelService carModelService)
        {
            _carModelService = carModelService;
        }

        [HttpGet("GetAllCarModels")]
        public IActionResult GetAllCarModels([FromQuery] string? searchTerm)
        {

            var response = _carModelService.GetAllCarModels(searchTerm);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        [HttpPost("AddNewCarModel")]
        public IActionResult AddCarModel([FromForm] CarModelDto model, [FromForm] List<IFormFile> images)
        {
            List<byte[]> imageDataList = new List<byte[]>();

            foreach (var image in images)
            {
                if (image != null && image.Length <= 5 * 1024 * 1024) 
                {
                    using (var ms = new MemoryStream())
                    {
                        image.CopyTo(ms);
                        imageDataList.Add(ms.ToArray()); 
                    }
                }
                else
                {
                    return BadRequest($"The image '{image.FileName}' exceeds the size limit of 5MB.");
                }
            }
            var response = _carModelService.AddCarModel(model, imageDataList);
            if (response.Success)
            {
                return Ok(response.Message);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
        [HttpPut("UpdateCarModelByCode")]
        public IActionResult UpdateCarModelByCode([FromForm] UpdateCarModelDto model, [FromForm] List<IFormFile> images)
        {
            List<byte[]> imageDataList = new List<byte[]>();

            foreach (var image in images)
            {
                if (image != null && image.Length <= 5 * 1024 * 1024)
                {
                    using (var ms = new MemoryStream())
                    {
                        image.CopyTo(ms);
                        imageDataList.Add(ms.ToArray());
                    }
                }
                else
                {
                    return BadRequest($"The image '{image.FileName}' exceeds the size limit of 5MB.");
                }
            }
            var response = _carModelService.UpdateCarModelByCode( model, imageDataList);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        [HttpDelete("Delete/{modelCode}")]
        public IActionResult DeleteCarModel(string modelCode)
        {
            var response = _carModelService.DeleteCarModel(modelCode);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
