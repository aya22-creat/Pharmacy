using System;

namespace Pharmacy.Products.Dto
{
    public class ProductDto
    {
        public int     Id            { get; set; }
        public string  Name          { get; set; }
        public decimal Price         { get; set; }
        public int     StockQuantity { get; set; }
        public bool    IsActive      { get; set; }
    }
}