using Ems.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Management
{
    public class UserManager : BaseManager<Users>
    {
        public override void Add(Users user)
        {
            var entity = repo.Select(x => x.Mail == user.Mail || x.NameSurname == user.NameSurname || x.HexId == user.HexId).FirstOrDefault();
           // CheckIfExists(x => x.Mail == user.Mail || x.NameSurname == user.NameSurname || x.HexId == user.HexId || x.DiscordId == user.DiscordId);

            if(entity != null)
            {
                if(entity.HexId == user.HexId)
                    throw new Exception("Böyle bir HexID'ye sahip kullanıcı zaten var.");
                if (entity.DiscordId == user.DiscordId)
                    throw new Exception("Böyle bir Discord ID'ye sahip kullanıcı zaten var.");
                if (entity.NameSurname == user.NameSurname)
                    throw new Exception("Böyle bir Ad Soyada sahip kullanıcı zaten var.");
                if (entity.Mail == user.Mail)
                    throw new Exception("Böyle bir Mail'e sahip kullanıcı zaten var.");
            }
            else
            {
                repo.Insert(user);
                repo.SaveChanges();
            }
        }
    }
}
