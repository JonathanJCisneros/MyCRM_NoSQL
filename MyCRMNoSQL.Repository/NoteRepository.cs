using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
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
        public async Task<Note> Get(string id)
        {
            return null;
        }

        public async Task<List<Note>> GetAll() 
        {
            return null;
        }

        public async Task<List<Note>> GetAllByBusiness(string id)
        {
            return null;
        }

        public async Task<List<Note>> GetAllByUser(string id)
        {
            return null;
        }

        public async Task<bool> Create(Note note)
        {
            return false;
        }

        public async Task<bool> Update(Note note)
        {
            return false;
        }

        public async Task<bool> Delete(string id)
        {
            return false;
        }

        public async Task<bool> DeleteAllByBusiness(string id)
        {
            return false;
        }
    }
}
