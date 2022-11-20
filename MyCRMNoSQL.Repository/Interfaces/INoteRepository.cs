﻿using MyCRMNoSQL.Repository;
using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository.Interfaces
{
    public interface INoteRepository : ICRUDRepository<Note>, IBRepository<Note>
    {
        List<Note> GetAllByUser(string id);
    }
}
