using System;
using System.ComponentModel.DataAnnotations;

namespace Tekton.Service.Dto
{
    public abstract class ProductBase
    {
        [Required]
        public string Master { get; set; }
        [Required]
        public string Detail { get; set; }
    }
    public class ProductDto: ProductBase
    {
        public Guid Id { get; set; }
    }
    public class ProductAddDto : ProductBase { }
}
