using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ems.ViewModels
{
    public class UserManagementViewModel
    {
        public int Id { get; set; }
        public string  NameSurname { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string RankName { get; set; }
        public int RankId { get; set; }
        public bool AccessManagement { get; set; }
    }
}