namespace MyCRMNoSQL.Web.Controllers
{
    public interface IExtension
    {
        string UserId();

        string UserType();

        bool LoggedIn();
    }
}
