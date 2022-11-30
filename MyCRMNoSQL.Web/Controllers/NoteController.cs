using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.Core;
using MyCRMNoSQL.CustomExtensions;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using MyCRMNoSQL.Service.Interfaces;

namespace MyCRMNoSQL.Web.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> _logger;
        private readonly IExtension _extension;
        private readonly INoteService _noteService;

        public NoteController(ILogger<NoteController> logger, IExtension extension, INoteService noteService)
        {
            _logger = logger;
            _extension = extension;
            _noteService = noteService;
        }

        public IActionResult Get(string id)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            Note note = _noteService.Get(id);

            return View(note);
        }

        public IActionResult GetAll()
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            List<Note> noteList = _noteService.GetAll();

            return View(noteList);
        }

        [HttpPost]
        public IActionResult Add(string id, NoteFormModel Note)
        {
            Note.UserId = _extension.UserId();
            Note.BusinessId = id;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = id });
            }

            Note = NoteFormModel.DbPrep(Note);

            Note note = new()
            {
                Details = Note.Details,
                UserId = Note.UserId,
                BusinessId = Note.BusinessId,
                CreatedDate = Note.CreatedDate,
                UpdatedDate = Note.UpdatedDate
            };

            string Id = _noteService.Create(note);

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        [HttpPost]
        public IActionResult Update(string id, string Bid, NoteFormModel Note)
        {
            Note.UserId = _extension.UserId();
            Note.BusinessId = Bid;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            Note = NoteFormModel.DbPrep(Note);

            Note note = new()
            {
                Id = id,
                Details = Note.Details,
                UserId = Note.UserId,
                BusinessId = Note.BusinessId,
                CreatedDate = Note.CreatedDate,
                UpdatedDate = Note.UpdatedDate
            };

            string Id = _noteService.Update(note);

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }

        public IActionResult Delete(string id, string Bid)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            bool Success = _noteService.Delete(id);

            if (!Success)
            {
                return Content("Something went wrong :'(");
            }

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }
    }
}
