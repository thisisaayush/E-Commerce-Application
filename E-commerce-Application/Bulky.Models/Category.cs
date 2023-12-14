using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(25)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Name")]
        [Range(1,100)]
        public int DisplayOrder { get; set; }

    }
}
