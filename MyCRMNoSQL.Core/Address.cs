using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core
{
    public class Address : BaseEntity
    {
        public string Street { get; set; }

        public string AptSuite { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string BuisinessId { get; set; }

        public Business BusinessAssociated { get; set; }
    }
}
