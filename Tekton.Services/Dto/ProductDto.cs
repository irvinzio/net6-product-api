using System;
using System.Collections.Generic;
using System.Text;

namespace Tekton.Service.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Master { get; set; }
        public string Detail { get; set; }
    }
}
