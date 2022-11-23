using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class ProductRepository : IProductRepository
    {
        public bool CheckById(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Products").Get(id).IsEmpty().Run(Conn);

            return Check;
        }

        public Product Get(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Products").Get(id)
                .Merge(p => new 
                { 
                })
            .Run(Conn);

            return null;
        }

        public Product GetProductWithCustomers(string id)
        {
            return null;
        }

        public List<Product> GetAll()
        {
            return null;
        }

        public List<Product> GetAllProductsWithCustomers()
        {
            return null;
        }

        public string Create(Product product)
        {
            return null;
        }

        public string Update(Product product)
        {
            return null;
        }

        public bool Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Products").Get(id).Delete().Run(Conn);

            if (Result.deleted == 0)
            {
                return false;
            }

            return true;
        }

        public bool DeleteAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Products").GetAll(id)[new { index = "BusinessId" }].Delete().Run(Conn);

            if (Result.deleted == 0)
            {
                return false;
            }

            return true;
        }
    }
}
