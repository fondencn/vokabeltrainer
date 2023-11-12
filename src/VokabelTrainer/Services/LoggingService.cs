using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VokabelTrainer.Services
{
    public class LoggingService
    {
        public void LogDebug(string msg, [CallerMemberName] string caller = null)
        {
            this.AddLogEntry(new LogEntry(msg, "DEBUG", caller));
        }

        public void LogException(string msg, Exception ex, [CallerMemberName] string caller = null)
        {
            this.AddLogEntry(new LogEntry(msg + ": " + ex.Message, "EXCEPTION", caller));
        }

        private void AddLogEntry(LogEntry entry)
        {
            Debug.WriteLine(entry.ToString());
        }

        private class LogEntry
        {
            public string Message { get; }
            public string Caller { get; }
            public DateTime LogDate { get; }
            public string LogLevel { get; }


            public LogEntry(string msg, string level, string caller)
            {
                this.Message = msg;
                this.LogLevel = level;
                this.LogDate = DateTime.Now;
                this.Caller = caller;
            }

            public override string ToString()
            {
                return $"{LogDate.ToString("dd.MM.yy HH:mm:ss")}\t{LogLevel}\t{Message}\t in {Caller}";
            }
        }
    }
}
