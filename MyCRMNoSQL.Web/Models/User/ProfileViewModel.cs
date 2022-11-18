using MyCRMNoSQL.Core;

namespace MyCRMNoSQL.Web.Models.User
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Type { get; set; }

        public DateTime LastLoggedIn { get; set; } = DateTime.Now;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public List<UpcomingTask> TaskList { get; set; }

        public List<ClientActivity> ActivityList { get; set; }

        public List<Purchase> Sales { get; set; }
    }
}
