#pragma warning disable CS8618

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

        public Business Employer { get; set; }
    }
}
