using Ems.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Management
{
    public class HierarchyManager : BaseManager<Hierarchy>
    {
        public bool CheckRankIsEqualOrLower(int currentUserId, int targetUserId)
        {
            UserManager userManager = new UserManager();
            var currentUser = userManager.GetById(currentUserId);
            var targetUser = userManager.GetById(targetUserId);

            var currentUserRank = repo.Select(x => x.RankId == currentUser.RankId).FirstOrDefault();
            var targetUserRank = repo.Select(x => x.RankId == targetUser.RankId).FirstOrDefault();

            var SameJob = (currentUser.JobId == targetUser.JobId) ? true : false;
            if (SameJob || currentUser.AccessManagementPanel)
            {
                var selectHigher = currentUserRank.HierarchyRank > targetUserRank.HierarchyRank ? currentUser : targetUser;
                return selectHigher == currentUser ? true : false;
            }
            else
                return false;
        }
    }
}
