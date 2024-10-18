using System.ComponentModel.DataAnnotations;

namespace CoreEhsTest.Models
{
    public class CarModel
    {
        [Key]
        public int CarID { get; set; } 

        [Required(ErrorMessage = "Brand is required.")]
        [MaxLength(50, ErrorMessage = "Brand name cannot exceed 50 characters.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Class is required.")]
        [MaxLength(50, ErrorMessage = "Class cannot exceed 50 characters.")]
        public string Class { get; set; }

        [Required(ErrorMessage = "Model Name is required.")]
        [MaxLength(100, ErrorMessage = "Model Name cannot exceed 100 characters.")]
        public string ModelName { get; set; }

        [Required(ErrorMessage = "Model Code is required.")]
        [MaxLength(10, ErrorMessage = "Model Code cannot exceed 10 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Model Code must be alphanumeric.")]
        public string ModelCode { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } 

        [Required(ErrorMessage = "Features are required.")]
        public string Features { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; } 

        [Required(ErrorMessage = "Manufacture Date is required.")]
        [DataType(DataType.Date)]
        public DateTime ManufactureDate { get; set; }

        public bool Active { get; set; }

        [Required(ErrorMessage = "Sort Order is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Sort Order must be greater than zero.")]
        public int SortOrder { get; set; } 

        //public List<ImageModel> Images { get; set; }

    }
}
