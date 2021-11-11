using Ems.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ems.ViewModels
{
    public class AddUserViewModel
    {   public Users User { get; set; }
        public List<Jobs> Jobs { get; set; }
        public List<Ranks> Ranks { get; set; }

    }
}