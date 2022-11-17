using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core
{
    public class Purchase : BaseEntity
    {
        public string BusinessId { get; set; }

        public Business BusinessAssociated { get; set; }

        public string ProductId { get; set; }

        public Product ProductAssociated { get; set; }

        public string AddressId { get; set; }

        public Address BusinessLocation { get; set; }

        public string UserId { get; set; }

        public User SalesRep { get; set; }
    }
}
