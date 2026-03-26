// Pharmacy.Core/Products/Product.cs
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Products
{
    public class Product : FullAuditedEntity<int>
    {
        [Required]
        [StringLength(150)]
        public string Name { get; protected set; }

        [Required]
        public decimal Price { get; protected set; }

        public int StockQuantity { get; protected set; }

        public bool IsActive { get; protected set; }

        protected Product() { }

        public Product(string name, decimal price, int stockQuantity)
        {
            SetName(name);
            SetPrice(price);
            SetStock(stockQuantity);
            IsActive = true;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ProductDomainException(ProductErrorCodes.NameRequired);
            Name = name;
        }

        public void SetPrice(decimal price)
        {
            if (price <= 0)
                throw new ProductDomainException(ProductErrorCodes.InvalidPrice);
            Price = price;
        }

        public void SetStock(int quantity)
        {
            if (quantity < 0)
                throw new ProductDomainException(ProductErrorCodes.InvalidStock);
            StockQuantity = quantity;
        }

        public void Activate()   => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}