using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core
{
    public class Business : BaseEntity
    {
        public string Name { get; set; }

        public string Website { get; set; }

        public DateTime StartDate { get; set; }

        public string Industry { get; set; }

        public string PocId { get; set; }

        public Staff PointOfContact { get; set; }

        public string UserId { get; set; }

        public List<ClientActivity> ActivityList { get; set; }

        public List<Address> AddressList { get; set; }

        public List<Staff> StaffList { get; set;}

        public List<Note> NoteList { get; set; }

        public List<Purchase> PurchaseList { get; set;}

        public List<UpcomingTask> TaskList { get; set; }
    }
}
