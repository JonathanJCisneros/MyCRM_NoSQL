using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
#pragma warning disable CS8603

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
                    Author = R.Db("MyCRM").Table("Products").Get(p["UserId"]).Pluck("FirstName", "LastName")
                })
            .Run(Conn);

            if(Query == null)
            {
                return null;
            }

            Product product = new() 
            { 
                Id = Query.id.ToString(),
                Name = Query.Name.ToString(),
                Price = Query.Price.ToString(),
                Description = Query.Description.ToString(),
                UserId = Query.UserId.ToString(),
                CreatedDate = Query.CreatedDate.ToString(),
                UpdatedDate = Query.UpdatedDate.ToString(),
                Author = new User()
                {
                    FirstName = Query.Author.FirstName.ToString(),
                    LastName = Query.Author.LastName.ToString()
                }
            };


            return product;
        }

        public Product GetProductWithCustomers(string id)
        {
            return null;
        }

        public List<Product> GetAll()
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Products")
                .Merge(p => new
                {
                    Author = R.Db("MyCRM").Table("Products").Get(p["UserId"]).Pluck("FirstName", "LastName")
                })
            .Run(Conn);

            if(Query.BufferedSize == 0)
            {
                return null;
            }

            List<Product> products = new();

            foreach (var i in Query)
            {
                Product product = new()
                {
                    Id = i.id.ToString(),
                    Name = i.Name.ToString(),
                    Price = i.Price.ToString(),
                    Description = i.Description.ToString(),
                    UserId = i.UserId.ToString(),
                    CreatedDate = i.CreatedDate.ToString(),
                    UpdatedDate = i.UpdatedDate.ToString(),
                    Author = new User()
                    {
                        FirstName = i.Author.FirstName.ToString(),
                        LastName = i.Author.LastName.ToString()
                    }
                };

                products.Add(product);
            }

            return products;
        }

        public List<Product> GetAllProductsWithCustomers()
        {
            return null;
        }

        public string Create(Product product)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Products")
                .Insert(new
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    UserId = product.UserId,
                    CreatedDate = product.CreatedDate,
                    UpdatedDate = product.UpdatedDate
                })
            .Run(Conn);

            string Id = Result.generated_keys[0].ToString();

            return Id;
        }

        public string Update(Product product)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Products").Get(product.Id)
                .Update(new
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    UserId = product.UserId,
                    UpdatedDate = product.UpdatedDate
                })
            .Run(Conn);

            if(Query == null)
            {
                return null;
            }

            return product.Id;
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
