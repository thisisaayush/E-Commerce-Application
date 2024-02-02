using System.ComponentModel.DataAnnotations;

namespace Book_Store_Database_Design.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string Description { get; set; }

       /* // Navigation property
        public virtual ICollection<Book> Books { get; set; }*/
    }
}
