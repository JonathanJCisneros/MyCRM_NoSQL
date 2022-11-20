using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class NoteRepository : INoteRepository
    {
        public Note Get(string id)
        {
            return null;
        }

        public List<Note> GetAll() 
        {
            return null;
        }

        public List<Note> GetAllByBusiness(string id)
        {
            return null;
        }

        public List<Note> GetAllByUser(string id)
        {
            return null;
        }

        public string Create(Note note)
        {
            return null;
        }

        public string Update(Note note)
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
