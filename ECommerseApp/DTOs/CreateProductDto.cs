using System.ComponentModel.DataAnnotations;

namespace ECommerseApp.DTOs
{
    public class CreateProductDto
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Description { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0 and less than double max value...")]
        public double Price { get; set; }
        [Required] public string PictureUrl { get; set; } = string.Empty;
        [Required] public string Type { get; set; } = string.Empty;
        [Required] public string Brand { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Stock Element must be grater than 0")]
        public int QuantityInStock { get; set; }
    }
}
