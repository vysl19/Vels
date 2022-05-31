namespace ReportService.Models
{
    public class ReportResult
    {
        public int Id { get; set; }
        public string ReportUuid { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
    }
}
