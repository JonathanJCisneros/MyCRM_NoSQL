using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyCRMNoSQL.Web.Controllers
{
    public class Extension : IExtension
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public Extension(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string UserId()
        {
            return _contextAccessor.HttpContext.Session.GetString("UserId");
        }

        public bool LoggedIn()
        {
            return UserId() != null;
        }
    }
}
