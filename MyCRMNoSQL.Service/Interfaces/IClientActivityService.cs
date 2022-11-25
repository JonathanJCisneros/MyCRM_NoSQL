using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IClientActivityService : ICRUDService<ClientActivity>, IBService<ClientActivity>
    {
        List<ClientActivity> GetAllByUser(string id);

        List<ClientActivity> GetAllByType(string type);
    }
}
