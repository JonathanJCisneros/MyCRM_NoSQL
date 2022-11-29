using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
#pragma warning disable CS8603

namespace MyCRMNoSQL.Repository
{
    public class NoteRepository : INoteRepository
    {
        public bool CheckById(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Notes").Get(id).IsEmpty().Run(Conn);

            return Check;
        }

        public Note Get(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Notes").Get(id)
                .Merge(n => new
                { 
                    Author = R.Db("MyCRM").Table("Users").Get(n["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Pluck("Name", "Industry")
                })
            .Run(Conn);

            if (Query == null)
            {
                return null;
            }

            Note note = new()
            {
                Id = Query.id.ToString(),
                UserId = Query.UserId.ToString(),
                BusinessId = Query.BusinessId.ToString(),
                Details = Query.Details.ToString(),
                CreatedDate = Query.CreatedDate.ToDateTime(),
                UpdatedDate = Query.UpdatedDate.ToDateTime(),
                Author = new User()
                {
                    FirstName = Query.Author.FirstName.ToString(),
                    LastName = Query.Author.LastName.ToString()
                },
                BusinessAssociated = new Business()
                {
                    Name = Query.BusinessAssociated.Name.ToString(),
                    Industry = Query.BusinessAssociated.Industry.ToString()
                }
            };

            return note;
        }

        public List<Note> GetAll() 
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Notes")
                .Merge(n => new
                {
                    Author = R.Db("MyCRM").Table("Users").Get(n["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Pluck("Name", "Industry")
                })
            .Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<Note> NoteList = new();

            foreach (var item in Query)
            {
                Note note = new()
                {
                    Id = Query.id.ToString(),
                    UserId = Query.UserId.ToString(),
                    BusinessId = Query.BusinessId.ToString(),
                    Details = Query.Details.ToString(),
                    CreatedDate = Query.CreatedDate.ToDateTime(),
                    UpdatedDate = Query.UpdatedDate.ToDateTime(),
                    Author = new User()
                    {
                        FirstName = Query.Author.FirstName.ToString(),
                        LastName = Query.Author.LastName.ToString()
                    },
                    BusinessAssociated = new Business()
                    {
                        Name = Query.BusinessAssociated.Name.ToString(),
                        Industry = Query.BusinessAssociated.Industry.ToString()
                    }
                };

                NoteList.Add(note);
            }

            return NoteList;
        }
    

        public List<Note> GetAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Notes").GetAll(id)[new { index = "BusinessId" }]
                .Merge(n => new
                {
                    Author = R.Db("MyCRM").Table("Users").Get(n["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Pluck("Name", "Industry")
                })
            .Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<Note> NoteList = new();

            foreach (var item in Query)
            {
                Note note = new()
                {
                    Id = Query.id.ToString(),
                    UserId = Query.UserId.ToString(),
                    BusinessId = Query.BusinessId.ToString(),
                    Details = Query.Details.ToString(),
                    CreatedDate = Query.CreatedDate.ToDateTime(),
                    UpdatedDate = Query.UpdatedDate.ToDateTime(),
                    Author = new User()
                    {
                        FirstName = Query.Author.FirstName.ToString(),
                        LastName = Query.Author.LastName.ToString()
                    },
                    BusinessAssociated = new Business()
                    {
                        Name = Query.BusinessAssociated.Name.ToString(),
                        Industry = Query.BusinessAssociated.Industry.ToString()
                    }
                };

                NoteList.Add(note);
            }

            return NoteList;
        }

        public List<Note> GetAllByUser(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Notes").GetAll(id)[new { index = "UserId" }]
                .Merge(n => new
                {
                    Author = R.Db("MyCRM").Table("Users").Get(n["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Pluck("Name", "Industry")
                })
            .Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<Note> NoteList = new();

            foreach (var item in Query)
            {
                Note note = new()
                {
                    Id = Query.id.ToString(),
                    UserId = Query.UserId.ToString(),
                    BusinessId = Query.BusinessId.ToString(),
                    Details = Query.Details.ToString(),
                    CreatedDate = Query.CreatedDate.ToDateTime(),
                    UpdatedDate = Query.UpdatedDate.ToDateTime(),
                    Author = new User()
                    {
                        FirstName = Query.Author.FirstName.ToString(),
                        LastName = Query.Author.LastName.ToString()
                    },
                    BusinessAssociated = new Business()
                    {
                        Name = Query.BusinessAssociated.Name.ToString(),
                        Industry = Query.BusinessAssociated.Industry.ToString()
                    }
                };

                NoteList.Add(note);
            }

            return NoteList;
        }

        public string Create(Note note)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Notes")
                .Insert(new
                {
                    UserId = note.UserId,
                    BusinessId = note.BusinessId,
                    Details = note.Details,
                    CreatedDate = note.CreatedDate,
                    UpdatedDate = note.UpdatedDate
                })
            .Run(Conn);

            string Id = Result.generated_keys[0].ToString(); 
            
            return Id;
        }

        public string Update(Note note)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Notes").Get(note.Id)
                .Update(new
                {
                    UserId = note.UserId,
                    BusinessId = note.BusinessId,
                    Details = note.Details,
                    UpdatedDate = note.UpdatedDate
                })
            .Run(Conn);

            if(Query == null)
            {
                return null;
            }

            return note.Id;
        }

        public bool Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Notes").Get(id).Delete().Run(Conn);

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

            var Result = R.Db("MyCRM").Table("Notes").GetAll(id)[new { index = "BusinessId" }].Delete().Run(Conn);

            if (Result.deleted == 0)
            {
                return false;
            }

            return true;
        }
    }
}
