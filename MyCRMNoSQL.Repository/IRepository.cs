using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public interface IRepository<T> where T : new()
    {
        Task<T> Get(string Id);

        Task<T> Create(T entity);

        Task<T> Update(T entity);  

        Task<string> Delete(string Id);

    }
}
