using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using DataRepository.DataAccess.GenericRepository;

namespace DataRepository.Models
{
    public class DoorsLog : IEntityId
    {
        public int Id { get; set; }

        [ForeignKey("Door0")]
        public int? Door0_Id { get; set; }
        public virtual Door Door0 { get; set; }

        [ForeignKey("Door1")]
        public int? Door1_Id { get; set; }
        public virtual Door Door1 { get; set; }

        [ForeignKey("Door2")]
        public int? Door2_Id { get; set; }
        public virtual Door Door2 { get; set; }

        [ForeignKey("Door3")]
        public int? Door3_Id { get; set; }
        public virtual Door Door3 { get; set; }

        [ForeignKey("Door4")]
        public int? Door4_Id { get; set; }
        public virtual Door Door4 { get; set; }

        [ForeignKey("Door5")]
        public int? Door5_Id { get; set; }
        public virtual Door Door5 { get; set; }

        [ForeignKey("Door6")]
        public int? Door6_Id { get; set; }
        public virtual Door Door6 { get; set; }

        [ForeignKey("Door7")]
        public int? Door7_Id { get; set; }
        public virtual Door Door7 { get; set; }

        [ForeignKey("Door8")]
        public int? Door8_Id { get; set; }
        public virtual Door Door8 { get; set; }


    }
}
