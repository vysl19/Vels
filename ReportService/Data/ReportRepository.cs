using Microsoft.EntityFrameworkCore;
using ReportService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Data
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _appDbContext;
        public ReportRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Add(Report report)
        {
            _appDbContext.Reports.Add(report);
        }

        public void AddReportResult(ReportResult reportResult)
        {
            _appDbContext.ReportResults.Add(reportResult);
        }

        public List<Report> Get()
        {
            return _appDbContext.Reports.ToList();
        }

        public Report Get(string reportUuid)
        {
            return  _appDbContext.Reports.Include(x => x.ReportResults).FirstOrDefault(x => x.Uuid == reportUuid);
        }

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }

        public void UpdateReport(string uuid, ReportStatus status)
        {
            var report = _appDbContext.Reports.FirstOrDefault(x => x.Uuid == uuid);
            if(report!= null)
            {
                report.ReportStatus = status;
            }
        }
    }
}
