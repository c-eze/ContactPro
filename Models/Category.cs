using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactPro.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string? AppUserId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string? Name { get; set; }

		//Image properties
		public byte[]? ImageData { get; set; }
		public string? ImageType { get; set; }

		[NotMapped]
		public IFormFile? ImageFile { get; set; }

		//Virtuals
		public virtual AppUser? AppUser { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();
    }
}
