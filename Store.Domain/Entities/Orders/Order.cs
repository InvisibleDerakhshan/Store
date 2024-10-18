using Bugeto_Store.Domain.Entities.Commons;
using Bugeto_Store.Domain.Entities.Finances;
using Bugeto_Store.Domain.Entities.Products;
using Bugeto_Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugeto_Store.Domain.Entities.Orders
{
    public class Order : BaseEntity 
    {
        public virtual User User { get; set; }
        public long UserId {  get; set; }
        public virtual RequestPay RequestPay { get; set; }
        public long RequestPayId { get; set; }

        public OrderState  OrderState { get; set; }
    }

    public class OrderDetail : BaseEntity
    {
        public virtual Order Order { get; set; }
        public long OrderId { get; set; }
        
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
    }


    public enum OrderState
    {
        Processing = 0,
        Canceled = 1,
        Delivered = 2,
    }
}
