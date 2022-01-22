using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Model.Log
{
    public class Logs : EmsBaseEntity
    {
        public int UserId { get; set; }
        public string Mail { get; set; }
        public string ValueEntered { get; set; }
        public string Exception { get; set; }
        public string StackTrace { get; set; }
        public string Url { get; set; }
        public LogLevel Level { get; set; }
        public LogType Type { get; set; }
    }
}
