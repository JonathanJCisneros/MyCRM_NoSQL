using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.Core;
using MyCRMNoSQL.CustomExtensions;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;

namespace MyCRMNoSQL.Web.Controllers
{
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;

        public StaffController(ILogger<StaffController> logger)
        {
            _logger = logger;
        }

        //[HttpPost]
        //public IActionResult Add(string id, StaffFormModel Staff)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = id });
        //    }

        //    Staff = StaffFormModel.DbPrep(Staff);

        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    var Query = R.Db("MyCRM").Table("Staff")
        //        .Insert(new
        //        {
        //            Position = Staff.Position,
        //            FirstName = Staff.FirstName,
        //            LastName = Staff.LastName,
        //            PhoneNumber = Staff.PhoneNumber,
        //            Email = Staff.Email,
        //            CreatedDate = Staff.CreatedDate,
        //            UpdatedDate = Staff.UpdatedDate,
        //            BusinessId = id
        //        })
        //    .Run(Conn);

        //    return RedirectToAction("ViewOne", "CRM", new { id = id });
        //}

        //[HttpPost]
        //public IActionResult Update(string id, string Bid, StaffFormModel Staff)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //    }

        //    Staff = StaffFormModel.DbPrep(Staff);

        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    bool Check = R.Db("MyCRM").Table("Staff").Get(id).IsEmpty().Run(Conn);

        //    if (Check == true)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //    }

        //    var Query = R.Db("MyCRM").Table("Staff")
        //        .Update(new
        //        {
        //            Position = Staff.Position,
        //            FirstName = Staff.FirstName,
        //            LastName = Staff.LastName,
        //            PhoneNumber = Staff.PhoneNumber,
        //            Email = Staff.Email,
        //            UpdatedDate = Staff.UpdatedDate
        //        })
        //    .Run(Conn);

        //    return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //}

        //public IActionResult Delete(string id, string Bid)
        //{
        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    bool Check = R.Db("MyCRM").Table("Staff").Get(id).IsEmpty().Run(Conn);

        //    if (Check == true)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //    }

        //    var Query = R.Db("MyCRM").Table("Staff").Get(id).Delete().Run(Conn);

        //    return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //}
    }
}
