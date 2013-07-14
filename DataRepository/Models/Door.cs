using System;

namespace DataRepository.Models
{
    public class Door
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int DoorTypeId { get; set; }
        public int DoorStateId { get; set; }

        public DateTime Date { get; set; }

        public virtual DoorType Type { get; set; }
        public virtual DoorState State { get; set; }
    }
}
