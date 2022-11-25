#pragma warning disable CS8618

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
