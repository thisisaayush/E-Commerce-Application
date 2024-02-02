using System.ComponentModel.DataAnnotations;

namespace Book_Store_Database_Design.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }
        [Required]
        public string CustomerPassword { get; set; }
        public string CustomerAddress { get; set; }

        //Navigation property
        public virtual ICollection<Order> Orders { get; set; }
    }
}
