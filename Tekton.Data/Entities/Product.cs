using System;

namespace Tekton.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Master { get; set; }
        public string Detail { get; set; }
    }
}
