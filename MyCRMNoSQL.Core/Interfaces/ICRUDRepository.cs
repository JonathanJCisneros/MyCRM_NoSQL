using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public interface ICRUDRepository<T> where T : new()
    {
        T Get(string id);

        List<T> GetAll();

        bool Create(T entity);

        bool Update(T entity);  

        bool Delete(string id);
    }
}
