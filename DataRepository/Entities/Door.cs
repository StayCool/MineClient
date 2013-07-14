using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataRepository.Entities
{
    class Door
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public DoorType Type { get; set; }

        public DoorState State { get; set; }
    }
}
