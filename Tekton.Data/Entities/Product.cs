using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tekton.Data.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public string Master { get; set; }
        [Required]
        public string Detail { get; set; }
    }
}
