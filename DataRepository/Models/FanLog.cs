using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataRepository.Models
{
    public class FanLog
    {
        public int Id { get; set; }

        public int FanNumber { get; set; }
        public int Fan1StateId { get; set; }
        public int Fan2StateId { get; set; }

        public int DoorsLogId { get; set; }
        public int AnalogSignalsId { get; set; }
        public DateTime Date { get; set; }

        public virtual DoorsLog DoorsLog { get; set; }
        public virtual AnalogSignal AnalogSignals { get; set; }
    }
}
