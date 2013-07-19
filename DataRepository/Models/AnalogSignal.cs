using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataRepository.DataAccess.GenericRepository;

namespace DataRepository.Models
{
    public class AnalogSignal : IEntityId
    {
        public int Id { get; set; }

        public int Signal1 { get; set; }
        public int Signal2 { get; set; }
        public int Signal3 { get; set; }
    }
}
