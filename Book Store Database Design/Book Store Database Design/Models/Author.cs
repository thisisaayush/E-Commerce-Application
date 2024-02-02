using System.ComponentModel.DataAnnotations;

namespace Book_Store_Database_Design.Models
{
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        [Required]
        public string AuthorName { get; set; }
        public string AuthorBio { get; set; }
        public DateTime? BirthDate { get; set; }
        
      /*  //Navigation Property
        public virtual ICollection<Book> Books { get; set; }*/

    }
}
