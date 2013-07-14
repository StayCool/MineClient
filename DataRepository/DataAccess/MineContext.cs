using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using DataRepository.Models;

namespace DataRepository.DataAccess
{
    public class MineContext : DbContext
    {
        public IDbSet<Door> Doors { get; set; }
        public IDbSet<DoorState> DoorStates { get; set; }
        public IDbSet<DoorType> DoorTypes { get; set; }

        public MineContext()
            : base(GetConnectionName())
        {
            Configuration.LazyLoadingEnabled = false;
        }

        protected static string GetConnectionName() {
            return @"Data Source=(localdb)\v11.0;Database=MineDb;Trusted_Connection=True;";
        }
    }
}
