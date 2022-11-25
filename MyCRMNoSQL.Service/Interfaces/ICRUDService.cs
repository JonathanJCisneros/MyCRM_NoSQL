using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface ICRUDService<T> where T : new()
    {
        bool CheckById(string id);

        T Get(string id);

        List<T> GetAll();

        string Create(T entity);

        string Update(T entity);

        bool Delete(string id);
    }
}
