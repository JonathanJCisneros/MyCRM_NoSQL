using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IClientActivityService
    {
        List<ClientActivity> GetAllByBusiness(string id);

        List<ClientActivity> GetAllByUser(string id);

        List<ClientActivity> GetAllByType(string type);

        ClientActivity Get(string id);

        List<ClientActivity> GetAll();

        string Create(ClientActivity activity);

        string Update(ClientActivity activity);

        bool Delete(string id);

        bool DeleteAllByBusiness(string id);
    }
}
