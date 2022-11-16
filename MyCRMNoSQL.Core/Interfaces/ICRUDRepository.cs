using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public interface ICRUDRepository<T> where T : new()
    {
        Task<T> Get(string id);

        Task<List<T>> GetAll();

        Task<bool> Create(T entity);

        Task<bool> Update(T entity);  

        Task<bool> Delete(string id);
    }
}
