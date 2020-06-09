using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natroral.Core.Models
{
    public class Order : BaseEntity
    {
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }
        
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }

        [Display(Name = "State/Province")]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }
      
        public decimal Shipping { get; set; }

        [Display(Name = "Order Total")]
        public decimal OrderTotal { get; set; }

        [Display(Name = "Order Items")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
