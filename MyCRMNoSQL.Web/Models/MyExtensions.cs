using MyCRMNoSQL.Models;
using Microsoft.AspNetCore.Http;

namespace MyCRMNoSQL.CustomExtensions
{
    public class MyExtensions
    {
        public static string StringToUpper(string s)
        {
            s = s.Trim().ToLower();
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
