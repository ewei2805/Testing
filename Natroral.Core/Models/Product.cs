using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Natroral.Core.Models
{
    public class Product : BaseEntity
    {
        [StringLength(80)]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        public string SEOName { get; set; }
        [StringLength(500)]
        public string Summary { get; set; }
        [StringLength(3000)]
        public string Description { get; set; }
        public string Tags { get; set; }
        [StringLength(500)]
        public string Usage { get; set; }
        [StringLength(50)]
        public string Weight { get; set; }
        [Range(0, 500)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Unit Price")]
        public decimal Price { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
    }
}
