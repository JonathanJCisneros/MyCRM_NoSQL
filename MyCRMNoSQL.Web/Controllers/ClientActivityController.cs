using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Core;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.CustomExtensions;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;

namespace MyCRMNoSQL.Web.Controllers
{
    public class ClientActivityController : Controller
    {
        private readonly ILogger<ClientActivityController> _logger;

        public ClientActivityController(ILogger<ClientActivityController> logger)
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

        //[HttpPost]
        //public IActionResult Add(string id, ClientActivityFormModel Activity)
        //{           
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = id });
        //    }

        //    Activity = ClientActivityFormModel.DbPrep(Activity);

        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    var Query = R.Db("MyCRM").Table("Activities")
        //        .Insert(new
        //        {
        //            BusinessId = id,
        //            UserId = Uid,
        //            Type = Activity.Type,
        //            Note = Activity.Note,
        //            StaffId = Activity.StaffId,
        //            CreatedDate = DateTime.Now,
        //            UpdatedDate = DateTime.Now
        //        })
        //    .Run(Conn);

        //    return RedirectToAction("ViewOne", "CRM", new { id = id });
        //}

        //[HttpPost]
        //public IActionResult Update(string id, string Bid, ClientActivityFormModel Activity)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //    }

        //    Activity = ClientActivityFormModel.DbPrep(Activity);

        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    bool Check = R.Db("MyCRM").Table("Activities").Get(id).IsEmpty().Run(Conn);

        //    if (Check == true)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //    }

        //    var Query = R.Db("MyCRM").Table("Activities")
        //        .Update(new
        //        {
        //            BusinessId = Bid,
        //            UserId = Uid,
        //            Type = Activity.Type,
        //            Note = Activity.Note,
        //            StaffId = Activity.StaffId,
        //            UpdatedDate = DateTime.Now
        //        })
        //    .Run(Conn);

        //    return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //}

        //public IActionResult Delete(string id, string Bid)
        //{
        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    bool Check = R.Db("MyCRM").Table("Activities").Get(id).IsEmpty().Run(Conn);

        //    if (Check == true)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //    }

        //    var Query = R.Db("MyCRM").Table("Activities").Get(id).Delete().Run(Conn);

        //    return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //}
    }
}
