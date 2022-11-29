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
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public bool CheckById(string id)
        {
            return _staffRepository.CheckById(id);
        }

        public List<Staff> GetAllByPosition(string position)
        {
            return _staffRepository.GetAllByPosition(position);
        }

        public List<Staff> GetAllByBusiness(string id)
        {
            return _staffRepository.GetAllByBusiness(id);
        }

        public Staff Get(string id)
        {
            return _staffRepository.Get(id);
        }

        public List<Staff> GetAll()
        {
            return _staffRepository.GetAll();
        }

        public string Create(Staff staff)
        {
            return _staffRepository.Create(staff);
        }

        public string Update(Staff staff)
        {
            return _staffRepository.Update(staff);
        }

        public bool Delete(string id)
        {
            return _staffRepository.Delete(id);
        }

        public bool DeleteAllByBusiness(string id)
        {
            return _staffRepository.DeleteAllByBusiness(id);
        }
    }
}
