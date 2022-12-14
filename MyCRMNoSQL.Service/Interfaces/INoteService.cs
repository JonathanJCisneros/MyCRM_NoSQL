using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface INoteService : ICRUDService<Note>, IBService<Note>
    {
        List<Note> GetAllByUser(string id);
    }
}
