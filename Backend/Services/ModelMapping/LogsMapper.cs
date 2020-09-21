using Business.Helpers;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Device;

namespace Services.ModelMapping
{
    public class LogsMapper
    {
        public string MapStringFromLogs(IEnumerable<LogEntity> logs)
        {
            if (logs is null || logs.ToList().Count == 0)
                throw new IndexOutOfRangeException();

            StringBuilder oneLineLog = new StringBuilder();

            foreach(var log in logs)
            {
                oneLineLog.AppendLine(log.Comments);
            }

            return oneLineLog.ToString();
        }
    }
}
