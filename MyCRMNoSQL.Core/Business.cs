﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core
{
    public class Business : BaseEntity
    {
        public string Name { get; set; }

        public string Website { get; set; }

        public DateTime StartDate { get; set; }

        public string Industry { get; set; }

        public string PocId { get; set; }

        public string UserId { get; set; }
    }
}
