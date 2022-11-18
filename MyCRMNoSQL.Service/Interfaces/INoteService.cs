using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface INoteService
    {
        List<Note> GetAllByUser(string id);

        List<Note> GetAllByBusiness(string id);

        Note Get(string id);

        List<Note> GetAll();

        string Create(Note entity);

        string Update(Note entity);

        bool Delete(string id);

        bool DeleteAllByBusiness(string id);
    }
}
