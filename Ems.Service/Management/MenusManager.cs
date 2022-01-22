using Ems.Data.Model;
using Ems.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Management
{
    public class MenusManager : BaseManager<MenuPermissions>
    {
        public List<MenuPermissions> GetPermissions(int userId)
        {
            var entity = repo.Select(x => x.UserId == userId).ToList();
            List<MenuPermissions> list = new List<MenuPermissions>();
            foreach(var item in entity)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
