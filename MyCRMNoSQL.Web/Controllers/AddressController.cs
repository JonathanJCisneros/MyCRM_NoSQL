using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Core;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.Service.Interfaces;

namespace MyCRMNoSQL.Web.Controllers
{
    public class AddressController : Controller
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IExtension _extension;
        private readonly IAddressService _addressService;

        public AddressController(ILogger<AddressController> logger, IExtension extension, IAddressService addressService)
        {
            _logger = logger;
            _extension = extension;
            _addressService = addressService;
        }

        public IActionResult Get(string id)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            Address address = _addressService.Get(id);

            return View(address);
        }

        [HttpPost]
        public IActionResult Add(string id, AddressFormModel Address)
        {
            Address.BusinessId = id;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = id });
            }

            Address = AddressFormModel.DbPrep(Address);

            Address address = new()
            {
                Street = Address.Street,
                AptSuite = Address.AptSuite,
                City = Address.City,
                State = Address.State,
                ZipCode = Address.ZipCode,
                BuisinessId = Address.BusinessId,
                CreatedDate = Address.CreatedDate,
                UpdatedDate = Address.UpdatedDate
            };

            string Id = _addressService.Create(address);

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        [HttpPost]
        public IActionResult Update(string id, string Bid, AddressFormModel Address)
        {
            Address.BusinessId = Bid;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            Address = AddressFormModel.DbPrep(Address);

            Address address = new()
            {
                Id = id,
                Street = Address.Street,
                AptSuite = Address.AptSuite,
                City = Address.City,
                State = Address.State,
                ZipCode = Address.ZipCode,
                BuisinessId = Address.BusinessId,
                UpdatedDate = Address.UpdatedDate
            };

            string Id = _addressService.Update(address);

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }

        public IActionResult Delete(string id, string Bid)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            bool Success = _addressService.Delete(id);

            if (!Success)
            {
                return Content("Delete did not work");
            }

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }
    }
}
