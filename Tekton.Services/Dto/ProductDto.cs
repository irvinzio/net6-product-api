using System;

namespace Tekton.Service.Dto
{
    public abstract class ProductBase
    {
  
        public string Master { get; set; }
        public string Detail { get; set; }
    }
    public class ProductDto: ProductBase
    {
        public Guid Id { get; set; }
    }
    public class ProductAddDto : ProductBase { }
}
