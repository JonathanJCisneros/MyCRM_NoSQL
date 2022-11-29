using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
using MyCRMNoSQL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service
{
    public class ClientActivityService : IClientActivityService
    {
        private readonly IClientActivityRepository _clientActivityRepository;

        public ClientActivityService(IClientActivityRepository clientActivityRepository)
        {
            _clientActivityRepository = clientActivityRepository;
        }

        public bool CheckById(string id)
        {
            return _clientActivityRepository.CheckById(id);
        }

        public List<ClientActivity> GetAllByBusiness(string id)
        {
            return _clientActivityRepository.GetAllByBusiness(id);
        }

        public List<ClientActivity> GetAllByUser(string id)
        {
            return _clientActivityRepository.GetAllByUser(id);
        }

        public List<ClientActivity> GetAllByType(string type)
        {
            return _clientActivityRepository.GetAllByType(type);
        }

        public ClientActivity Get(string id)
        {
            return _clientActivityRepository.Get(id);
        }

        public List<ClientActivity> GetAll()
        {
            return _clientActivityRepository.GetAll();
        }

        public string Create(ClientActivity activity)
        {
            return _clientActivityRepository.Create(activity);
        }

        public string Update(ClientActivity activity)
        {
            return _clientActivityRepository.Update(activity);
        }

        public bool Delete(string id)
        {
            return _clientActivityRepository.Delete(id);
        }

        public bool DeleteAllByBusiness(string id)
        {
            return _clientActivityRepository.DeleteAllByBusiness(id);
        }
    }
}
