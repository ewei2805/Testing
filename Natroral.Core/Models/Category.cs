using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Natroral.Core.Models
{
    public class Category : BaseEntity
    {
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}
