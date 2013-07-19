using DataRepository.DataAccess.GenericRepository;

namespace DataRepository.Models
{
    public class Door : IEntityId 
    {
        public int Id { get; set; }
        
        public int DoorTypeId { get; set; }
        public int DoorStateId { get; set; }

        public virtual DoorType Type { get; set; }
        public virtual ObjectState State { get; set; }
    }
}
