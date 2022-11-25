#pragma warning disable CS8618

namespace MyCRMNoSQL.Core
{
    public class ClientActivity : BaseEntity
    {
        public string UserId { get; set; }

        public User UserAssociated { get; set; }

        public string BusinessId { get; set; }

        public Business BusinessAssociated { get; set; }

        public string StaffId { get; set; }

        public Staff EmployeeAssociated { get; set; }

        public string Type { get; set; }

        public string Note { get; set; }
    }
}
