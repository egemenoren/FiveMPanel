using Ems.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Management
{
    public class PaidSalariesManagement : BaseManager<PaidSalaries>
    {
        public PaidSalaries GetLastPaid(int userId)
        {
            try
            {
                var entity = this.GetAllByParameter(x => x.UserId == userId).Last();
                return entity;
            }
            catch
            {
                return null;
            }
        }

    }
}
