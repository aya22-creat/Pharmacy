
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Products.Dto
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
    }
}