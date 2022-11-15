using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core
{
    public class Note : BaseEntity
    {
        public string Details { get; set; }

        public string UserId { get; set; }

        public string BusinessId { get; set; }
    }
}
