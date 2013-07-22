using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataRepository.DataAccess.GenericRepository;

namespace DataRepository.Models
{
    public class FanLog : IEntityId
    {
        public int Id { get; set; }

        public int FanNumber { get; set; }
        public int Fan1StateId { get; set; }
        public int Fan2StateId { get; set; }

        public int DoorsLogId { get; set; }
        public int AnalogSignalLogId { get; set; }
        public DateTime Date { get; set; }

        public FanState Fan1State { get; set; }
        public FanState Fans2State { get; set; }
        public virtual ICollection<DoorsLog> DoorsLogs { get; set; }
        public virtual ICollection<AnalogSignalLog> AnalogSignalLogs { get; set; }
    }
}
