using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Data.Model.Log
{
    public enum LogType
    {
        Login = 1,
        Logout = 2,
        Insert = 3,
        Update = 4,
        Delete = 5,
        Generic = 6
    }
}
