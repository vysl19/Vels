using ReportService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.MessageBus
{
    public interface IMessageBusClient
    {
        void CreateReport(Report report);
    }
}
