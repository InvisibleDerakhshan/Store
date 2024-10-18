using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugeto_Store.Domain.Entities.Commons
{
    public abstract class BaseEntityNotId
    {
        public DateTime InsertTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; }
        public bool IsRomoved { get; set; } = false;
        public DateTime RemoveTime { get; set; }
    }
}
