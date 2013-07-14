using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using DataRepository.Entities;

namespace DataRepository.DataAccess
{
    class MineContext : DbContext
    {
        public DbSet<Door> Doors { get; set; }
    }
}
