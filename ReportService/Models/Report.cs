using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Models
{
    public class Report
    {
        [Key]
        [Required]
        public string Uuid { get; set; }
        public DateTime Date { get; set; }
        public ReportStatus ReportStatus { get; set; }
        public List<ReportResult> ReportResults { get; set; }
    }
}
