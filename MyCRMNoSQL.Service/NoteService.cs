using MyCRMNoSQL.Core;
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
        public List<Note> GetAllByUser(string id)
        {
            return null;
        }

        public List<Note> GetAllByBusiness(string id)
        {
            return null;
        }

        public Note Get(string id)
        {
            return null;
        }

        public List<Note> GetAll()
        {
            return null;
        }

        public string Create(Note note)
        {
            return null;
        }

        public string Update(Note entity)
        {
            return null;
        }

        public bool Delete(string id)
        {
            return false;
        }

        public bool DeleteAllByBusiness(string id)
        {
            return false;
        }
    }
}
