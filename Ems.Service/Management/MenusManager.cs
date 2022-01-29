using Ems.Data.Model;
using Ems.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ems.Service.Management
{
    public class MenusManager : BaseManager<MenuPermissions>
    {
        public List<MenuPermissions> GetPermissions(int? userId,int rankId,int jobId)
        {
            var entity = repo.Select(x => x.UserId == userId || x.RankId == rankId || x.JobId == jobId).ToList();
            List<MenuPermissions> list = new List<MenuPermissions>();
            foreach(var item in entity)
            {
                list.Add(item);
            }
            return list;
        }
        public void EditPermissions(int userId,int rankId,int subMenuId,bool hasAccess)
        {
            var menusManager = new MenusManager();
            var accesible = menusManager.GetByParameter(x=>x.RankId == rankId && x.SubMenuId == subMenuId) != null ? true:false;
            var mainMenu = new MainMenusManager().GetById(new SubMenusManager().GetById(subMenuId).MainMenuId);
            if(accesible == false && hasAccess == true)
            {
                MenuPermissions perms = new MenuPermissions
                {
                    RankId = rankId,
                    SubMenuId = subMenuId,
                    MainMenuId = mainMenu.Id,
                    CreatedBy = userId
                };
                Add(perms);
            }
            if(accesible == true && hasAccess == false)
            {
                var entity = menusManager.GetByParameter(x => x.SubMenuId == subMenuId && x.RankId == rankId);
                entity.RankId = null;
                Update(entity);
            }
        }
    }
}
