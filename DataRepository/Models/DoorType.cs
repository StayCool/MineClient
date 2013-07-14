using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataRepository.Models
{
    public class DoorType
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(25)]
        public string Type { get; set; }

        public virtual ICollection<Door> Doors { get; set; }
    }
}
