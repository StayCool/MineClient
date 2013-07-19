using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataRepository.DataAccess.GenericRepository;

namespace DataRepository.Models
{
    public class ObjectState : IEntityId
    {
        public int Id { get; set; }
     
        [Column(TypeName = "varchar")]
        [StringLength(20)]
        public string State { get; set; }
    }
}
