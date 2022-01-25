using System;
using System.ComponentModel.DataAnnotations;

namespace Tekton.Service.Dto
{
    public abstract class ProductBase
    {
       public string Detail { get; set; }
    }
    public class ProductDto: ProductBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public string Product { get; set; }
    }
    public class ProductAddBase : ProductBase
    {
        [Required]
        public int PrductMockId { get; set; }
    }
    public class ProductAddDto : ProductAddBase { }
}
