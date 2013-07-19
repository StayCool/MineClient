using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DataRepository.DataAccess.GenericRepository;

namespace DataRepository.Models
{
    public class DoorsLog : IEntityId
    {
        public int Id { get; set; }

        public int? DoorId1 { get; set; }
        public int? DoorId2 { get; set; }
        public int? DoorId3 { get; set; }
        public int? DoorId4 { get; set; }
        public int? DoorId5 { get; set; }
        public int? DoorId6 { get; set; }
        public int? DoorId7 { get; set; }
        public int DoorId8 { get; set; }
        public int DoorId9 { get; set; }
    }
}
