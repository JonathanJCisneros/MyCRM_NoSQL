using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class UserRepository : IUserRepository
    {
        public bool CheckByEmail(string email)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Users").GetAll(email)[new { index = "Email" }].IsEmpty().Run(Conn);

            return Check;
        }

        public User Login(string email)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var DBUser = R.Db("MyCRM").Table("Users").GetAll(email)[new { index = "Email" }].Pluck("id", "Password").CoerceTo("array").Run(Conn);
            User userInfo = new()
            {
                Id = DBUser[0].id.ToString(),
                Password = DBUser[0].Password.ToString()
            };

            var Update = R.Db("MyCRM").Table("Users").Get(userInfo.Id)
                .Update(new
                {
                    LastLoggedIn = DateTime.Now
                })
            .Run(Conn);

            return userInfo;
        }

        public string Create(User user)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Users")
                .Insert(new
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    LastLoggedIn = user.LastLoggedIn,
                    CreatedDate = user.CreatedDate,
                    UpdatedDate = user.UpdatedDate,
                    Type = user.Type
                })
            .Run(Conn);

            string Id = Result.generated_keys[0].ToString();

            return Id;
        }

        public User Get(string id) 
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Users").Get(id).Run(Conn);

            User user = new()
            {
                Id = Query.id.ToString(),
                FirstName = Query.FirstName.ToString(),
                LastName = Query.LastName.ToString(),
                Email = Query.Email.ToString(),
                Type = Query.Type.ToString(),
                CreatedDate = Query.CreatedDate.ToDateTime(),
                UpdatedDate = Query.UpdatedDate.ToDateTime(),
                LastLoggedIn = Query.LastLoggedIn.ToDateTime()
            };

            return user;
        }

        public List<User> GetAll()
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Users").Run(Conn);

            List<User> UserList = new List<User>();

            foreach (var item in Query)
            {
                User user = new()
                {
                    Id = item.id,
                    FirstName = item.FirstName.ToString(),
                    LastName = item.LastName.ToString(),
                    Email = item.Email.ToString(),
                    Type = item.Type.ToString(),
                    CreatedDate = Query.CreatedDate.ToDateTime(),
                    UpdatedDate = Query.UpdatedDate.ToDateTime(),
                    LastLoggedIn = Query.LastLoggedIn.ToDateTime()
                };

                UserList.Add(user);
            }

            return UserList;
        }

        public string Update(User user)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Users").Get(user.Id)
                .Update(new 
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UpdatedDate = user.UpdatedDate
                })
            .Run(Conn);

            if(Query == null)
            {
                return null;
            }

            return Query.valid;
        }

        public bool Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Users").Get(id).Delete().Run(Conn);

            if(Query == null)
            {
                return false;
            }

            return true;
        }
    }
}
