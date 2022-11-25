#pragma warning disable CS8618

namespace MyCRMNoSQL.Core
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Type { get; set; }

        public DateTime LastLoggedIn { get; set; }

        public List<UpcomingTask> TaskList { get; set; }

        public List<Purchase> SalesList { get; set; }
    }
}
