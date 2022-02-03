using Ems.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ems.ViewModels
{
    public class RankManagementViewModel
    {
        public int Id { get; set; }
        public string RankName { get; set; }
        public bool AccessJobPanel { get; set; }
        public string JobName { get; set; }
        public short HierarchyNo { get; set; }
        public int HourlySalary { get; set; }
    }
}