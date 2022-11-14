using MyCRMNoSQL.Models;

namespace MyCRMNoSQL.CustomExtensions
{
    public class MyExtensions
    {
        public static string StringToUpper(string s)
        {
            s = s.Trim().ToLower();
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static User DbPrep(User u)
        {
            u.FirstName = StringToUpper(u.FirstName);
            u.LastName = StringToUpper(u.LastName);
            u.Email = u.Email.Trim().ToLower();
            u.Password = u.Password.Trim();

            return u;
        }
    }
}
