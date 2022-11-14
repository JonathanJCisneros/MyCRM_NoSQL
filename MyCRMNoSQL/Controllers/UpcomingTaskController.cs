using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.CustomExtensions;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;

namespace MyCRMNoSQL.Controllers
{
    public class UpcomingTaskController : Controller
    {
        private readonly ILogger<UpcomingTaskController> _logger;

        public UpcomingTaskController(ILogger<UpcomingTaskController> logger)
        {
            _logger = logger;
        }

        private string? Uid
        {
            get
            {
                return HttpContext.Session.GetString("UserId");
            }
        }

        [HttpPost]
        public IActionResult Add(string id, UpcomingTask Task)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = id });
            }

            Task = UpcomingTask.DbPrep(Task);

            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var Query = R.Db("MyCRM").Table("Tasks")
                .Insert(new
                {
                    StaffId = Task.StaffId,
                    Type = Task.Type,
                    Details = Task.Details,
                    DueDate = Task.DueDate,
                    Status = Task.Status,
                    CreatedDate = Task.CreatedDate,
                    UpdatedDate = Task.UpdatedDate,
                    BusinessId = id,
                    UserId = Uid
                })
            .Run(Conn);

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        [HttpPost]
        public IActionResult Update(string id, string Bid, UpcomingTask Task)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            Task = UpcomingTask.DbPrep(Task);

            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Tasks").Get(id).IsEmpty().Run(Conn);

            if (Check == true)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            var Query = R.Db("MyCRM").Table("Tasks")
                .Update(new
                {
                    StaffId = Task.StaffId,
                    Type = Task.Type,
                    Details = Task.Details,
                    DueDate = Task.DueDate,
                    Status = Task.Status,
                    UpdatedDate = Task.UpdatedDate,
                    UserId = Uid
                })
            .Run(Conn);

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        public IActionResult Delete(string id, string Bid)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Tasks").Get(id).IsEmpty().Run(Conn);

            if (Check == true)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            var Query = R.Db("MyCRM").Table("Tasks").Get(id).Delete().Run(Conn);

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }
    }
}
