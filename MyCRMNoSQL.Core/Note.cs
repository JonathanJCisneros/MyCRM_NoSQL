#pragma warning disable CS8618

namespace MyCRMNoSQL.Core
{
    public class Note : BaseEntity
    {
        public string Details { get; set; }

        public string UserId { get; set; }

        public User Author { get; set; }

        public string BusinessId { get; set; }

        public Business BusinessAssociated { get; set; }
    }
}
