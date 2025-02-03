using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace E_BookWebRazor_Temp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order must be 1-100")]
        public int CategoryOrder { get; set; }
    }
}
