using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Book_Store_Database_Design.Models
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
       /* [ForeignKey("Author")]
        public int AuthorID { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }*/
        [Required]
        public int QuantityInStock { get; set; }
        public int? PublicationYear { get; set; }

       /* //Navigation properties
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
*/
    }
}
