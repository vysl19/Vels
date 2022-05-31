using ReportService.Models;
using System.Collections.Generic;

namespace ReportService.Data
{
    public interface IReportRepository:IRepository
    {
        void Add(Report report);
        void AddReportResult(ReportResult reportResult);
        void UpdateReport(string uuid, ReportStatus status);
        List<Report> Get();
        Report Get(string reportUuid);
    }
}

