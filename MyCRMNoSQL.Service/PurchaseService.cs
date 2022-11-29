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
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public bool CheckById(string id)
        {
            return _purchaseRepository.CheckById(id);
        }

        public List<Purchase> GetAllByBusiness(string id)
        {
            return _purchaseRepository.GetAllByBusiness(id);
        }

        public List<Purchase> GetAllByIndustry(string industry)
        {
            return _purchaseRepository.GetAllByIndustry(industry);
        }

        public List<Purchase> GetAllByCity(string city)
        {
            return _purchaseRepository.GetAllByCity(city);
        }

        public List<Purchase> GetAllByZipCode(int zipCode)
        {
            return _purchaseRepository.GetAllByZipCode(zipCode);
        }

        public List<Purchase> GetAllByUser(string id)
        {
            return _purchaseRepository.GetAllByUser(id);
        }

        public Purchase Get(string id)
        {
            return _purchaseRepository.Get(id);
        }

        public List<Purchase> GetAll()
        {
            return _purchaseRepository.GetAll();
        }

        public string Create(Purchase purchase)
        {
            return _purchaseRepository.Create(purchase);
        }

        public string Update(Purchase purchase)
        {
            return _purchaseRepository.Update(purchase);
        }

        public bool Delete(string id)
        {
            return _purchaseRepository.Delete(id);
        }

        public bool DeleteAllByBusiness(string id)
        {
            return _purchaseRepository.DeleteAllByBusiness(id);
        }
    }
}
