﻿using MyCRMNoSQL.Core;
using MyCRMNoSQL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service
{
    public class StaffService : IStaffService
    {
        public List<Staff> GetAllByPosition(string position)
        {
            return null;
        }

        public List<Staff> GetAllByBusiness(string id)
        {
            return null;
        }

        public Staff Get(string id)
        {
            return null;
        }

        public List<Staff> GetAll()
        {
            return null;
        }

        public string Create(Staff staff)
        {
            return null;
        }

        public string Update(Staff staff)
        {
            return null;
        }

        public bool Delete(string id)
        {
            return false;
        }

        public bool DeleteAllByBusiness(string id)
        {
            return false;
        }
    }
}
