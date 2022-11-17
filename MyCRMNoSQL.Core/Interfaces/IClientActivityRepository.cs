using MyCRMNoSQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core.Interfaces
{
    public interface IClientActivityRepository : ICRUDRepository<ClientActivity>, IBRepository<ClientActivity>
    {
        List<ClientActivity> GetAllByUser(string id);

        List<ClientActivity> GetAllByType(string type);
    }
}
