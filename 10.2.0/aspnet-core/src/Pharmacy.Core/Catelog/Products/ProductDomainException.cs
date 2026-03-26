using System;

namespace Pharmacy.Products
{
    public class ProductDomainException : Exception
    {
        public string Code { get; }

        public ProductDomainException(string code, string message = null)
            : base(message ?? code)
        {
            Code = code;
        }
    }
}