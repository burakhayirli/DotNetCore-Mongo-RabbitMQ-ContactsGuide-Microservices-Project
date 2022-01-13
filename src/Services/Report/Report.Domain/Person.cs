﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Domain
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
    }
}
