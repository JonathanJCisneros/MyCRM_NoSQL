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
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        { 
            _addressRepository = addressRepository;
        }


        public bool CheckById(string id)
        {
            return _addressRepository.CheckById(id);
        }

        public Address Get(string id)
        {
            return _addressRepository.Get(id);
        }

        public List<Address> GetAll()
        {
            return _addressRepository.GetAll();
        }

        public List<Address> GetAllByCity(string city)
        {
            return _addressRepository.GetAllByCity(city);
        }

        public List<Address> GetAllByState(string state)
        {
            return _addressRepository.GetAllByState(state);
        }

        public List<Address> GetAllByZipCode(int zipCode)
        {
            return _addressRepository.GetAllByZipCode(zipCode);
        }

        public List<Address> GetAllByBusiness(string id)
        {
            return _addressRepository.GetAllByBusiness(id);
        }

        public string Create(Address address)
        {
            return _addressRepository.Create(address);
        }

        public string Update(Address address)
        {
            return _addressRepository.Update(address);
        }

        public bool Delete(string id)
        {
            return _addressRepository.Delete(id);
        }

        public bool DeleteAllByBusiness(string id)
        {
            return _addressRepository.DeleteAllByBusiness(id);
        }
    }
}
