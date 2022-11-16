using MyCRMNoSQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core.Interfaces
{
    public interface INoteRepository : ICRUDRepository<Note>, IBRepository<Note>
    {
        Task<List<Note>> GetAllByUser(string id);
    }
}
