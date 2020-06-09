using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natroral.Core.Models
{
    public class Contact : BaseEntity
    {   
        [Display(Name ="Full Name")]
        [StringLength(100), Required]
        public string FullName { get; set; }       
        [EmailAddress(ErrorMessage ="Invalid Email.")]
        [StringLength(100), Required]
        public string Email { get; set; }  
        [StringLength(100), Required]
        public string Subject { get; set; }
        [StringLength(1000), Required]
        public string Message { get; set; }
    }
}
