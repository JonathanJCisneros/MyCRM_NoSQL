using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.Core;
using MyCRMNoSQL.CustomExtensions;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;
using System.Diagnostics;

namespace MyCRMNoSQL.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> _logger;

        public NoteController(ILogger<NoteController> logger)
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
        //public IActionResult Add(string id, NoteFormModel Note)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = id });
        //    }

        //    Note = NoteFormModel.DbPrep(Note);

        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    var Query = R.Db("MyCRM").Table("Notes")
        //        .Insert(new
        //        {
        //            BusinessId = id,
        //            UserId = Uid,
        //            Details = Note.Details,
        //            CreatedDate = Note.CreatedDate,
        //            UpdatedDate = Note.UpdatedDate
        //        })
        //    .Run(Conn);

        //    return RedirectToAction("ViewOne", "CRM", new { id = id });
        //}

        //[HttpPost]
        //public IActionResult Update(string id, string Bid, NoteFormModel Note)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //    }

        //    Note = NoteFormModel.DbPrep(Note);

        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    bool Check = R.Db("MyCRM").Table("Notes").Get(id).IsEmpty().Run(Conn);

        //    if (Check == true)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //    }

        //    var Query = R.Db("MyCRM").Table("Notes")
        //        .Update(new
        //        {
        //            BusinessId = Bid,
        //            UserId = Uid,
        //            Details = Note.Details,
        //            UpdatedDate = Note.UpdatedDate
        //        })
        //    .Run(Conn);

        //    return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //}

        //public IActionResult Delete(string id, string Bid)
        //{
        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    bool Check = R.Db("MyCRM").Table("Notes").Get(id).IsEmpty().Run(Conn);

        //    if (Check == true)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //    }

        //    var Query = R.Db("MyCRM").Table("Notes").Get(id).Delete().Run(Conn);

        //    return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //}
    }
}
