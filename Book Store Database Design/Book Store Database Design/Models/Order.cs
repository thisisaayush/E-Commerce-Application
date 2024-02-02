using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_Store_Database_Design.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        //Navigation properties
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
