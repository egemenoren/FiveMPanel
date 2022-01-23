using Ems.Data.Model.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Logger
{
    interface ILogger
    {
        void Info(int? userId, string mail, string valueEntered, string exception, string stackTrace, string url,  LogType logType);
        void Warning(int? userId, string mail, string valueEntered, string exception, string stackTrace, string url,  LogType logType);
        void Error(int? userId, string mail, string valueEntered, string exception, string stackTrace, string url,  LogType logType);
        void Fatal(int? userId, string mail, string valueEntered, string exception, string stackTrace, string url,  LogType logType);
    }
}
