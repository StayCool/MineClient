using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataRepository.DataAccess.GenericRepository;

namespace DataRepository.Models
{
    public class Door : IEntityId 
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        public string DoorType { get; set; }

        [Column(TypeName = "nvarchar")]
        public string DoorState { get; set; }
    }
}
