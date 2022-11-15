using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core
{
    public class Staff : BaseEntity
    {
        public string Position { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long PhoneNumber { get; set; }

        public string Email { get; set; }

        public string BusinessId { get; set; }
    }
}
