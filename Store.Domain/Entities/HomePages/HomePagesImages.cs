﻿using Bugeto_Store.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugeto_Store.Domain.Entities.HomePages
{
    public class HomePagesImages : BaseEntity
    {
        public string Src  { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }

    public enum ImageLocation
    {
        L1 = 0,
        l2 = 1,
        R1 =2,
        CenterFullScreen=3,
        G1=4, 
        G2=5,
    }
}
