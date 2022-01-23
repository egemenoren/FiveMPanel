using Ems.Data.Model.Log;
using Ems.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Logger
{
    public class LogManager : ILogger
    {
        private static LogManager logManager = null;
        private static GenericRepository<Logs> _repo;
        protected LogManager()
        {
            
        }
        private static void CreateInstance()
        {
            if (logManager == null)
                logManager = new LogManager();
        }
        public static LogManager GetInstance()
        {
            if (logManager == null)
                CreateInstance();
            _repo = new GenericRepository<Logs>();
            return logManager;
        }
        public void Error(int? userId,string mail,string valueEntered,string exception,string stackTrace,string url,LogType logType)
        {
            Logs log = new Logs
            {
                UserId = userId,
                Mail = mail,
                ValueEntered = valueEntered,
                Exception = exception,
                StackTrace = stackTrace,
                Url = url,
                Level = LogLevel.Error,
                Type = logType
            };
            _repo.Insert(log);
            _repo.SaveChanges();
        }

        public void Fatal(int? userId, string mail, string valueEntered, string exception, string stackTrace, string url, LogType logType)
        {
            Logs log = new Logs
            {
                UserId = userId,
                Mail = mail,
                ValueEntered = valueEntered,
                Exception = exception,
                StackTrace = stackTrace,
                Url = url,
                Level = LogLevel.Fatal,
                Type = logType
            };
            _repo.Insert(log);
            _repo.SaveChanges();
        }

        public void Info(int? userId, string mail, string valueEntered, string exception, string stackTrace, string url,  LogType logType)
        {
            Logs log = new Logs
            {
                UserId = userId,
                Mail = mail,
                ValueEntered = valueEntered,
                Exception = exception,
                StackTrace = stackTrace,
                Url = url,
                Level = LogLevel.Info,
                Type = logType
            };
            _repo.Insert(log);
            _repo.SaveChanges();
        }

        public void Warning(int? userId, string mail, string valueEntered, string exception, string stackTrace, string url,LogType logType)
        {
            Logs log = new Logs
            {
                UserId = userId,
                Mail = mail,
                ValueEntered = valueEntered,
                Exception = exception,
                StackTrace = stackTrace,
                Url = url,
                Level = LogLevel.Warning,
                Type = logType
            };
            _repo.Insert(log);
            _repo.SaveChanges();
        }
    }
}
