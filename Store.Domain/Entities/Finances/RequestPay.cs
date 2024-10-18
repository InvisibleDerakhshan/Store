﻿using Bugeto_Store.Domain.Entities.Commons;
using Bugeto_Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugeto_Store.Domain.Entities.Finances
{
    public class RequestPay :BaseEntity
    {
        public Guid Guid { get; set; }
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public int Amount { get; set; }
        public bool IsPay {  get; set; }  
        public DateTime? PayDate { get; set; }

        public string Authority { get; set; }
        public long RefId { get; set; } = 0;

    }
}
