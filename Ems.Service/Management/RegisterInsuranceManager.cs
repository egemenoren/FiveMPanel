using Ems.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Management
{
    public class RegisterInsuranceManager : BaseManager<RegisterInsurance>
    {
        public RegisterInsuranceManager()
        {

        }
        public RegisterInsurance CheckHasValidInsurance(string nameSurname)
        {
            if (nameSurname != null)
               return repo.Select(x =>x.NameSurname == nameSurname && x.ExpireDate > DateTime.Now).FirstOrDefault();
            else
                return null;
        }
        public void UseCredits(RegisterInsurance model)
        {
            model.CreditsLeft -= 1;
            Update(model);
        }

    }
}
