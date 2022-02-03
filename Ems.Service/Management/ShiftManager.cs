using Ems.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Management
{
    public class ShiftManager : BaseManager<Shifts>
    {
        public List<Shifts> GetUsersShifts(int userId)
        {
            return GetAllByParameter(x => x.UserId == userId);
        }
        public bool CheckUserHaveActiveShift(int userId)
        {
            var entity = GetByParameter(x => x.UserId == userId && x.StartDate != null && x.EndDate == null);
            if (entity != null)
                return true;
            return false;
        }
        public Shifts GetUsersOpenShift(int userId)
        {
            try
            {
                var entity = GetByParameter(x => x.UserId == userId && x.StartDate != null && x.EndDate == null);
                return entity;
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }
    }
}
