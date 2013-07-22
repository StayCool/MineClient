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
        public IDbSet<FanLog> FanLog { get; set; }
        public IDbSet<DoorsLog> DoorsLog { get; set; }
        
        public IDbSet<DoorType> DoorsType { get; set; }
        public IDbSet<AnalogSignalType> AnalogSignalType { get; set; }

        public IDbSet<DoorState> DoorState { get; set; }

        public MineContext()
            : base(GetConnectionName())
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected static string GetConnectionName() {
            return @"Data Source=(localdb)\v11.0;Database=MineDb1;Trusted_Connection=True;";
        }
    }
}
