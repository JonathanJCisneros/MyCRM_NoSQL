﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.CustomExtensions;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;

namespace MyCRMNoSQL.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> _logger;

        public NoteController(ILogger<NoteController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add(string id, Note Note)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        [HttpPost]
        public IActionResult Update(string id, Note Note)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        public IActionResult Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }
    }
}
