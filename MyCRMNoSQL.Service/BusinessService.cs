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
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IUserRepository _userRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IClientActivityRepository _clientActivityRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly INoteRepository _noteRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUpcomingTaskRepository _upcomingTaskRepository;

        public BusinessService(
            IBusinessRepository businessRepository, 
            IUserRepository userRepository,
            IStaffRepository staffRepository,
            IClientActivityRepository clientActivityRepository,
            IAddressRepository addressRepository,
            INoteRepository noteRepository,
            IPurchaseRepository purchaseRepository,
            IUpcomingTaskRepository upcomingTaskRepository)
        {
            _businessRepository = businessRepository;
            _userRepository = userRepository;
            _staffRepository = staffRepository;
            _clientActivityRepository = clientActivityRepository;
            _addressRepository = addressRepository;
            _noteRepository = noteRepository;
            _purchaseRepository = purchaseRepository;
            _upcomingTaskRepository = upcomingTaskRepository;
        }

        public bool CheckByName(string name)
        {
            return _businessRepository.CheckByName(name);
        }

        public List<Business> GetAllWithLatestActivity()
        {
            return _businessRepository.GetAllWithLatestActivity();
        }

        public List<Business> GetAllByIndustry(string industry)
        {
            return _businessRepository.GetAllByIndustry(industry);
        }

        public object Get(string id)
        {
            Business business = _businessRepository.Get(id);
                business.PointOfContact = _staffRepository.Get(business.PocId);
            
            List<Address> addressList = _addressRepository.GetAllByBusiness(id);
            
            List<Staff> staffList = _staffRepository.GetAllByBusiness(id);
            
            List<Note> noteList = _noteRepository.GetAllByBusiness(id);
            
            List<ClientActivity> activityList = _clientActivityRepository.GetAllByBusiness(id);
            
            List<Purchase> purchaseList = _purchaseRepository.GetAllByBusiness(id);
            
            List<UpcomingTask> taskList = _upcomingTaskRepository.GetAllByBusiness(id);

            dynamic obj = new
            {
                Business = business,
                AddressList = addressList,
                StaffList = staffList,
                NoteList = noteList,
                ClientActivity = activityList,
                Purchase = purchaseList,
                TaskList = taskList
            }; 
            
            return obj;
        }

        public List<Business> GetAll()
        {
            return _businessRepository.GetAll();
        }

        public string Create(Business business, Address address, Staff staff)
        {
            string Id = _businessRepository.Create(business);
            
            staff.BusinessId = Id;
            
            string sId = _staffRepository.Create(staff);
            
            business.PocId = sId;
            
            _businessRepository.Update(business);
            
            address.BuisinessId = Id;
            
            _addressRepository.Create(address);

            return Id;
        }

        public string Update(Business business)
        {
            return _businessRepository.Update(business);
        }

        public bool Delete(string id)
        {
            bool b = _businessRepository.Delete(id);
            
            if(b == false)
            {
                return false;
            }
            
            bool a = _addressRepository.DeleteAllByBusiness(id);
            
            if(a == false)
            {
                return false;
            }
            
            bool s = _staffRepository.DeleteAllByBusiness(id);
            
            if(s == false)
            {
                return false;
            }
           
            bool c = _clientActivityRepository.DeleteAllByBusiness(id);

            if(c == false)
            {
                return false;
            }

            bool t = _upcomingTaskRepository.DeleteAllByBusiness(id);

            if(t == false)
            {
                return false;
            }

            bool n = _noteRepository.DeleteAllByBusiness(id);

            if(n == false)
            {
                return false;
            }

            bool p = _purchaseRepository.DeleteAllByBusiness(id);

            if(p == false)
            {
                return false;
            }

            return true;
        }
    }
}
