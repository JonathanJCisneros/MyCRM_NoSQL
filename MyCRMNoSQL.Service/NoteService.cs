using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
using MyCRMNoSQL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public bool CheckById(string id)
        {
            return _noteRepository.CheckById(id);
        }

        public List<Note> GetAllByUser(string id)
        {
            return _noteRepository.GetAllByUser(id);
        }

        public List<Note> GetAllByBusiness(string id)
        {
            return _noteRepository.GetAllByBusiness(id);
        }

        public Note Get(string id)
        {
            return _noteRepository.Get(id);
        }

        public List<Note> GetAll()
        {
            return _noteRepository.GetAll();
        }

        public string Create(Note note)
        {
            return _noteRepository.Create(note);
        }

        public string Update(Note note)
        {
            return _noteRepository.Update(note);
        }

        public bool Delete(string id)
        {
            return _noteRepository.Delete(id);
        }

        public bool DeleteAllByBusiness(string id)
        {
            return _noteRepository.DeleteAllByBusiness(id);
        }
    }
}
