using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Utility
{
    public class JsonFramework
    {
        public string Message { get; set; }
        public string Status { get; set; } = "success";
        public object Data { get; set; }
    }
}
