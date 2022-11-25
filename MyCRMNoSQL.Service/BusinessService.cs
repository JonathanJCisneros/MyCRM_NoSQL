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
        private readonly IUserService _userService;
        private readonly IStaffService _staffService;
        private readonly IClientActivityService _clientActivityService;
        private readonly IAddressService _addressService;
        private readonly INoteService _noteService;
        private readonly IPurchaseService _purchaseService;
        private readonly IUpcomingTaskService _upcomingTaskService;

        public BusinessService(
            IBusinessRepository businessRepository, 
            IUserService userService,
            IStaffService staffService,
            IClientActivityService clientActivityService,
            IAddressService addressService,
            INoteService noteService,
            IPurchaseService purchaseService,
            IUpcomingTaskService upcomingTaskService)
        {
            _businessRepository = businessRepository;
            _userService = userService;
            _staffService = staffService;
            _clientActivityService = clientActivityService;
            _addressService = addressService;
            _noteService = noteService;
            _purchaseService = purchaseService;
            _upcomingTaskService = upcomingTaskService;
        }

        public bool CheckById(string id)
        {
            return _businessRepository.CheckById(id);
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

        public Business Get(string id)
        {
            Business business = _businessRepository.Get(id);
                business.PointOfContact = _staffService.Get(business.PocId);
            
            List<Address> addressList = _addressService.GetAllByBusiness(id);
            
            List<Staff> staffList = _staffService.GetAllByBusiness(id);
            
            List<Note> noteList = _noteService.GetAllByBusiness(id);
            
            List<ClientActivity> activityList = _clientActivityService.GetAllByBusiness(id);
            
            List<Purchase> purchaseList = _purchaseService.GetAllByBusiness(id);
            
            List<UpcomingTask> taskList = _upcomingTaskService.GetAllByBusiness(id);

            business.AddressList = addressList;
            business.StaffList = staffList;
            business.NoteList = noteList;
            business.ActivityList = activityList;
            business.PurchaseList = purchaseList;
            business.TaskList = taskList;
            
            return business;
        }

        public List<Business> GetAll()
        {
            return _businessRepository.GetAll();
        }

        public string CreateClient(Business business, Address address, Staff staff)
        {
            string Id = _businessRepository.Create(business);
            
            business.Id = Id;
            
            staff.BusinessId = business.Id;
            
            string sId = _staffService.Create(staff);
            
            business.PocId = sId;
            
            string bId = _businessRepository.Update(business);
            
            address.BuisinessId = Id;
            
            string aId = _addressService.Create(address);

            return Id;
        }

        public string Create(Business business)
        {
            return _businessRepository.Create(business);
        }

        public string Update(Business business)
        {
            bool Check = CheckById(business.Id);

            if(Check)
            {
                return null;
            }

            return _businessRepository.Update(business);
        }

        public bool Delete(string id)
        {
            bool check = CheckById(id);
            
            if (check)
            {
                return false;
            }

            bool b = _businessRepository.Delete(id);
            
            if(!b)
            {
                return false;
            }
            
            bool a = _addressService.DeleteAllByBusiness(id);
            
            if(!a)
            {
                return false;
            }
            
            bool s = _staffService.DeleteAllByBusiness(id);
            
            if(!s)
            {
                return false;
            }
           
            bool c = _clientActivityService.DeleteAllByBusiness(id);

            if(!c)
            {
                return false;
            }

            bool t = _upcomingTaskService.DeleteAllByBusiness(id);

            if(!t)
            {
                return false;
            }

            bool n = _noteService.DeleteAllByBusiness(id);

            if(!n)
            {
                return false;
            }

            bool p = _purchaseService.DeleteAllByBusiness(id);

            if(!p)
            {
                return false;
            }

            return true;
        }
    }
}
