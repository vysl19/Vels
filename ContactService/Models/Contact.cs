using System.ComponentModel.DataAnnotations;

namespace ContactService.Models
{
    public class Contact
    {        
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string PersonUuid { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public ContactType ContactType { get; set; }
    }
}
