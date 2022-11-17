using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
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

        public string Register(User user)
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

            User user = new User()
            {
                Id = Query.id.ToString(),
                FirstName = Query.FirstName.ToString(),
                LastName = Query.LastName.ToString(),
                Email = Query.Email.ToString(),
                Type = Query.Type.ToString(),
                LastLoggedIn = Query.LastLoggedIn.ToDateTime()
            };

            return user;
        }

        public User GetByEmail(string email)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Users").GetAll(email)[new { index = "Email" }].Pluck("id", "FirstName", "LastName", "Email", "CreatedDate", "UpdatedDate", "Type").CoerceTo("array").Run(Conn);

            if(Query.Count == 0)
            {
                return null;
            }

            User user = new User()
            {
                Id = Query.id.ToString(),
                FirstName = Query.FirstName.ToString(),
                LastName = Query.LastName.ToString(),
                Email = Query.Email.ToString(),
                CreatedDate = Query.CreatedDate.ToDateTime(),
                UpdatedDate = Query.UpdatedDate.ToDateTime(),
                Type = Query.Type.ToString()
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
                User user = new User()
                {
                    Id = item.id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Type = item.Type,
                    LastLoggedIn = item.LastLoggedIn,
                };

                UserList.Add(user);
            }

            return UserList;
        }

        public bool Create(User user)
        {
            return false;
        }

        public bool Update(User user)
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

            if(Query == false)
            {
                return false;
            }

            return true;
        }

        public bool Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Users").Get(id).Delete().Run(Conn);

            return true;
        }
    }
}
