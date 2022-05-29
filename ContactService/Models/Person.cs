using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactService.Models
{
    public class Person
    {
        [Key]        
        public string Uuid { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Company { get; set; }        
        public List<Contact> Contacts { get; set; }

    }
}
