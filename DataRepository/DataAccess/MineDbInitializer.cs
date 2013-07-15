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

            //Инициализация таблицы состояния ляд
            var states = new List<ObjectState>
            {
                new ObjectState { State = "открыт" },
                new ObjectState { State = "закрыт" }
            };
            states.ForEach(s => context.ObjectStates.Add(s));
            context.SaveChanges();

            //Инициализация таблицы типов ляд
            var types = new List<DoorType>
            {
                new DoorType { Type = "отсекающая 1" },
                new DoorType { Type = "отсекающая 2" },
                new DoorType { Type = "переключающая 1" },
                new DoorType { Type = "переключающая 2" },
                new DoorType { Type = "атмосферная" },
                new DoorType { Type = "дифузорная" },
                new DoorType { Type = "подводящего канал" },
                new DoorType { Type = "Направляющий вентилятора 1" },
                new DoorType { Type = "Напрявляющий вентилятора 2" }
            };
            types.ForEach(s => context.DoorTypes.Add(s));
            context.SaveChanges();

            //Инициализация таблицы ляд
            var doors = new List<Door>();
            for (int i = 1; i <= types.Count; i++) {
                doors.Add(new Door { DoorTypeId = i, DoorStateId = 1});
                doors.Add(new Door { DoorTypeId = i, DoorStateId = 2});
            }
            doors.ForEach(s => context.Doors.Add(s));
            context.SaveChanges();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("uk-UA");
        }
    }
}
