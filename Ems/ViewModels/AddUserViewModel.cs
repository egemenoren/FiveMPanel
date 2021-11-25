using Ems.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ems.ViewModels
{
    public class AddUserViewModel
    {
        public List<Jobs> Jobs { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }
        public ulong DiscordId { get; set; }
        public string SteamHex { get; set; }
        public bool AccessManagementPanel { get; set; }
        public int JobId { get; set; }
        public int RankId { get; set; }
        public string RankName { get; set; }
        public string NameSurname { get; set; }


    }
}