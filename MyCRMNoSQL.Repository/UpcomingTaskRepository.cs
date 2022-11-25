using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
#pragma warning disable CS8603

namespace MyCRMNoSQL.Repository
{
    public class UpcomingTaskRepository : IUpcomingTaskRepository
    {
        public bool CheckById(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Tasks").Get(id).IsEmpty().Run(Conn);

            return Check;
        }

        public UpcomingTask Get(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Tasks").Get(id)
                .Merge(t => new
                {
                    UserAssociated = R.Db("MyCRM").Table("Users").Get(t["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(t["BusinessId"]).Pluck("Name", "Industry"),
                    EmployeeAssociated = R.Db("MyCRM").Table("Staff").Get(t["StaffId"]).Pluck("FirstName", "LastName", "Position")
                })
            .Run(Conn);

            if(Query == null)
            {
                return null;
            }

            UpcomingTask task = new()
            {
                Id = Query.id.ToString(),
                UserId = Query.UserId.ToString(),
                BusinessId = Query.BusinessId.ToString(),
                StaffId = Query.StaffId.ToString(),
                Type = Query.Type.ToString(),
                Details = Query.Details.ToString(),
                DueDate = Query.DueDate.ToDateTime(),
                Status = Query.Status.ToString(),
                CreatedDate = Query.CreatedDate.ToDateTime(),
                UpdatedDate = Query.UpdatedDate.ToDateTime(),
                UserAssociated = new User()
                {
                    FirstName = Query.UserAssociated.FirstName.ToString(),
                    LastName = Query.UserAssociated.LastName.ToString()
                },
                BusinessAssociated = new Business()
                {
                    Name = Query.BusinessAssociated.Name.ToString(),
                    Industry = Query.BusinessAssociated.Industry.ToString()
                },
                EmployeeAssociated = new Staff()
                {
                    FirstName = Query.EmployeeAssociated.FirstName.ToString(),
                    LastName = Query.EmployeeAssociated.LastName.ToString(),
                    Position = Query.EmployeeAssociated.Position.ToString()
                }
            };

            return task;
        }

        public List<UpcomingTask> GetAll()
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Tasks")
                .Merge(t => new
                {
                    UserAssociated = R.Db("MyCRM").Table("Users").Get(t["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(t["BusinessId"]).Pluck("Name", "Industry"),
                    EmployeeAssociated = R.Db("MyCRM").Table("Staff").Get(t["StaffId"]).Pluck("FirstName", "LastName", "Position")
                })
            .Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<UpcomingTask> TaskList = new();

            foreach (var item in Query)
            {
                UpcomingTask task = new()
                {
                    Id = item.id.ToString(),
                    UserId = item.UserId.ToString(),
                    BusinessId = item.BusinessId.ToString(),
                    StaffId = item.StaffId.ToString(),
                    Type = item.Type.ToString(),
                    Details = item.Details.ToString(),
                    DueDate = item.DueDate.ToDateTime(),
                    Status = item.Status.ToString(),
                    CreatedDate = item.CreatedDate.ToDateTime(),
                    UpdatedDate = item.UpdatedDate.ToDateTime(),
                    UserAssociated = new User()
                    {
                        FirstName = item.UserAssociated.FirstName.ToString(),
                        LastName = item.UserAssociated.LastName.ToString()
                    },
                    BusinessAssociated = new Business()
                    {
                        Name = item.BusinessAssociated.Name.ToString(),
                        Industry = item.BusinessAssociated.Industry.ToString()
                    },
                    EmployeeAssociated = new Staff()
                    {
                        FirstName = item.EmployeeAssociated.FirstName.ToString(),
                        LastName = item.EmployeeAssociated.LastName.ToString(),
                        Position = item.EmployeeAssociated.Position.ToString()
                    }
                };

                TaskList.Add(task);
            }

            return TaskList;
        }

        public List<UpcomingTask> GetAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Tasks").GetAll(id)[new { index = "BusinessId" }]
                .Merge(t => new
                {
                    UserAssociated = R.Db("MyCRM").Table("Users").Get(t["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(t["BusinessId"]).Pluck("Name", "Industry"),
                    EmployeeAssociated = R.Db("MyCRM").Table("Staff").Get(t["StaffId"]).Pluck("FirstName", "LastName", "Position")
                })
            .Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<UpcomingTask> TaskList = new();

            foreach (var item in Query)
            {
                UpcomingTask task = new()
                {
                    Id = item.id.ToString(),
                    UserId = item.UserId.ToString(),
                    BusinessId = item.BusinessId.ToString(),
                    StaffId = item.StaffId.ToString(),
                    Type = item.Type.ToString(),
                    Details = item.Details.ToString(),
                    DueDate = item.DueDate.ToDateTime(),
                    Status = item.Status.ToString(),
                    CreatedDate = item.CreatedDate.ToDateTime(),
                    UpdatedDate = item.UpdatedDate.ToDateTime(),
                    UserAssociated = new User()
                    {
                        FirstName = item.UserAssociated.FirstName.ToString(),
                        LastName = item.UserAssociated.LastName.ToString()
                    },
                    BusinessAssociated = new Business()
                    {
                        Name = item.BusinessAssociated.Name.ToString(),
                        Industry = item.BusinessAssociated.Industry.ToString()
                    },
                    EmployeeAssociated = new Staff()
                    {
                        FirstName = item.EmployeeAssociated.FirstName.ToString(),
                        LastName = item.EmployeeAssociated.LastName.ToString(),
                        Position = item.EmployeeAssociated.Position.ToString()
                    }
                };

                TaskList.Add(task);
            }

            return TaskList;
        }

        public List<UpcomingTask> GetAllByUser(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Tasks").GetAll(id)[new { index = "UserId" }]
                .Merge(t => new
                {
                    UserAssociated = R.Db("MyCRM").Table("Users").Get(t["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(t["BusinessId"]).Pluck("Name", "Industry"),
                    EmployeeAssociated = R.Db("MyCRM").Table("Staff").Get(t["StaffId"]).Pluck("FirstName", "LastName", "Position")
                })
            .Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<UpcomingTask> TaskList = new();

            foreach (var item in Query)
            {
                UpcomingTask task = new()
                {
                    Id = item.id.ToString(),
                    UserId = item.UserId.ToString(),
                    BusinessId = item.BusinessId.ToString(),
                    StaffId = item.StaffId.ToString(),
                    Type = item.Type.ToString(),
                    Details = item.Details.ToString(),
                    DueDate = item.DueDate.ToDateTime(),
                    Status = item.Status.ToString(),
                    CreatedDate = item.CreatedDate.ToDateTime(),
                    UpdatedDate = item.UpdatedDate.ToDateTime(),
                    UserAssociated = new User()
                    {
                        FirstName = item.UserAssociated.FirstName.ToString(),
                        LastName = item.UserAssociated.LastName.ToString()
                    },
                    BusinessAssociated = new Business()
                    {
                        Name = item.BusinessAssociated.Name.ToString(),
                        Industry = item.BusinessAssociated.Industry.ToString()
                    },
                    EmployeeAssociated = new Staff()
                    {
                        FirstName = item.EmployeeAssociated.FirstName.ToString(),
                        LastName = item.EmployeeAssociated.LastName.ToString(),
                        Position = item.EmployeeAssociated.Position.ToString()
                    }
                };

                TaskList.Add(task);
            }

            return TaskList;
        }

        public List<UpcomingTask> GetAllByStatus(string status)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Tasks").GetAll(status)[new { index = "Status" }]
                .Merge(t => new
                {
                    UserAssociated = R.Db("MyCRM").Table("Users").Get(t["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(t["BusinessId"]).Pluck("Name", "Industry"),
                    EmployeeAssociated = R.Db("MyCRM").Table("Staff").Get(t["StaffId"]).Pluck("FirstName", "LastName", "Position")
                })
            .Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<UpcomingTask> TaskList = new();

            foreach (var item in Query)
            {
                UpcomingTask task = new()
                {
                    Id = item.id.ToString(),
                    UserId = item.UserId.ToString(),
                    BusinessId = item.BusinessId.ToString(),
                    StaffId = item.StaffId.ToString(),
                    Type = item.Type.ToString(),
                    Details = item.Details.ToString(),
                    DueDate = item.DueDate.ToDateTime(),
                    Status = item.Status.ToString(),
                    CreatedDate = item.CreatedDate.ToDateTime(),
                    UpdatedDate = item.UpdatedDate.ToDateTime(),
                    UserAssociated = new User()
                    {
                        FirstName = item.UserAssociated.FirstName.ToString(),
                        LastName = item.UserAssociated.LastName.ToString()
                    },
                    BusinessAssociated = new Business()
                    {
                        Name = item.BusinessAssociated.Name.ToString(),
                        Industry = item.BusinessAssociated.Industry.ToString()
                    },
                    EmployeeAssociated = new Staff()
                    {
                        FirstName = item.EmployeeAssociated.FirstName.ToString(),
                        LastName = item.EmployeeAssociated.LastName.ToString(),
                        Position = item.EmployeeAssociated.Position.ToString()
                    }
                };

                TaskList.Add(task);
            }

            return TaskList; ;
        }

        public List<UpcomingTask> GetAllByType(string type)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Tasks").GetAll(type)[new { index = "Type" }]
                .Merge(t => new
                {
                    UserAssociated = R.Db("MyCRM").Table("Users").Get(t["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(t["BusinessId"]).Pluck("Name", "Industry"),
                    EmployeeAssociated = R.Db("MyCRM").Table("Staff").Get(t["StaffId"]).Pluck("FirstName", "LastName", "Position")
                })
            .Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<UpcomingTask> TaskList = new();

            foreach (var item in Query)
            {
                UpcomingTask task = new()
                {
                    Id = item.id.ToString(),
                    UserId = item.UserId.ToString(),
                    BusinessId = item.BusinessId.ToString(),
                    StaffId = item.StaffId.ToString(),
                    Type = item.Type.ToString(),
                    Details = item.Details.ToString(),
                    DueDate = item.DueDate.ToDateTime(),
                    Status = item.Status.ToString(),
                    CreatedDate = item.CreatedDate.ToDateTime(),
                    UpdatedDate = item.UpdatedDate.ToDateTime(),
                    UserAssociated = new User()
                    {
                        FirstName = item.UserAssociated.FirstName.ToString(),
                        LastName = item.UserAssociated.LastName.ToString()
                    },
                    BusinessAssociated = new Business()
                    {
                        Name = item.BusinessAssociated.Name.ToString(),
                        Industry = item.BusinessAssociated.Industry.ToString()
                    },
                    EmployeeAssociated = new Staff()
                    {
                        FirstName = item.EmployeeAssociated.FirstName.ToString(),
                        LastName = item.EmployeeAssociated.LastName.ToString(),
                        Position = item.EmployeeAssociated.Position.ToString()
                    }
                };

                TaskList.Add(task);
            }

            return TaskList;
        }

        public List<UpcomingTask> GetAllPastDue()
        {
            return null;
        }

        public List<UpcomingTask> GetAllUpcoming()
        {
            return null;
        }

        public string Create(UpcomingTask task)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Tasks")
                .Insert(new
                {
                    UserId = task.UserId,
                    BusinessId = task.BusinessId,
                    StaffId = task.StaffId,
                    Type = task.Type,
                    Details = task.Details,
                    DueDate = task.DueDate,
                    Status = task.Status,
                    CreatedDate = task.CreatedDate,
                    UpdatedDate = task.UpdatedDate
                })
            .Run(Conn);

            string Id = Query.generated_keys[0].ToString();

            return Id;
        }

        public string Update(UpcomingTask task)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Tasks").Get(task.Id)
                .Update(new
                {
                    UserId = task.UserId,
                    BusinessId = task.BusinessId,
                    StaffId = task.StaffId,
                    Type = task.Type,
                    Details = task.Details,
                    DueDate = task.DueDate,
                    Status = task.Status,
                    UpdatedDate = task.UpdatedDate
                })
            .Run(Conn);

            return task.Id;
        }

        public bool Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Tasks").Get(id).Delete().Run(Conn);

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

            var Result = R.Db("MyCRM").Table("Tasks").GetAll(id)[new { index = "BusinessId" }].Delete().Run(Conn);

            if (Result.deleted == 0)
            {
                return false;
            }

            return true;
        }
    }
}
