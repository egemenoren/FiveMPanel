using Ems.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ems.Service.Management
{
    public class PayChecksManager:BaseManager<PayChecks>
    {
        public void PayExtra(int userId,double pay)
        {
            var rate = 0.2;
            pay *= rate;
            var currentPay = this.GetByParameter(x => x.IsPaid == false && x.UserId == userId);
            if(currentPay != null)
            {
                currentPay.CurrentPay += pay;
                this.Update(currentPay);
            }
            else
            {
                this.Add(new PayChecks
                {
                    CreateTime = DateTime.Now,
                    CreatedBy = userId,
                    CurrentPay = 0,
                    UserId = userId
                });
                PayExtra(userId,pay);
            }
        }
    }
}
