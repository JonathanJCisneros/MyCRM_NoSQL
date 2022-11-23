using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository.Interfaces
{
    public interface ICRUDRepository<T> where T : new()
    {
        bool CheckById(string id);

        T Get(string id);

        List<T> GetAll();

        string Create(T entity);

        string Update(T entity);  

        bool Delete(string id);
    }
}
