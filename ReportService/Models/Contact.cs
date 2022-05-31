using System.ComponentModel.DataAnnotations;

namespace ReportService.Models
{
    public class Contact
    {        

        public int Id { get; set; }

        public string PersonUuid { get; set; }

        public string Content { get; set; }

        public ContactType ContactType { get; set; }
    }
}
