﻿using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class AddressRepository : IAddressRepository
    {
        public async Task<Address> Get(string id)
        {
            return null;
        }

        public async Task<List<Address>> GetAll()
        {
            return null;
        }

        public async Task<List<Address>> GetByBusiness(string id)
        {
            return null;
        }

        public async Task<List<Address>> GetByCity(string city)
        {
            return null;
        }

        public async Task<List<Address>> GetByZipCode(int zipCode)
        {
            return null;
        }

        public async Task<bool> Create(Address address)
        {
            return false;
        }

        public async Task<bool> Update(Address address)
        {
            return false;
        }

        public async Task<bool> Delete(string id)
        {
            return false;
        }

        public async Task<bool> DeleteAllByBusiness(string id)
        {
            return false;
        }
    }
}