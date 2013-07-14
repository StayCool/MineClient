using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using DataRepository.Models;

namespace DataRepository.DataAccess
{
    public class MineDbInitializer : DropCreateDatabaseIfModelChanges<MineContext>
    {
        protected override void Seed(MineContext context)
        {
            if (context == null)
                context = new MineContext();

            var states = new List<DoorState>
            {
                new DoorState { State = "open" },
                new DoorState { State = "closed" }
            };
            states.ForEach(s => context.DoorStates.Add(s));
            context.SaveChanges();

            var types = new List<DoorType>
            {
                new DoorType { Type = "cutting" },
                new DoorType { Type = "switching" },
                new DoorType { Type = "atmospheric" },
                new DoorType { Type = "diffuser" },
            };
            types.ForEach(s => context.DoorTypes.Add(s));
            context.SaveChanges();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("uk-UA");
            var doors = new List<Door>
            {
                new Door { DoorStateId = 1, DoorTypeId = 1, Number = 1, 
                    Date = DateTime.Parse("01.10.2013 23:41", ci)},
                new Door { DoorStateId = 2, DoorTypeId = 1, Number = 1, 
                    Date = DateTime.Parse("01.10.2013 23:41", ci)},
                new Door { DoorStateId = 1, DoorTypeId = 1, Number = 2, 
                    Date = DateTime.Parse("01.10.2013 23:41", ci)},
                new Door { DoorStateId = 2, DoorTypeId = 1, Number = 2, 
                    Date = DateTime.Parse("01.10.2013 23:41", ci)},
            };
            doors.ForEach(s => context.Doors.Add(s));
            context.SaveChanges();
        }
    }
}
