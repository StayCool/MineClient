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

            //Инициализация таблицы ляд
            var doors = new List<Door>
            {
                new Door { DoorType = "Ляда 1 отсекающая", DoorState = "открыта"},
                new Door { DoorType = "Ляда 1 отсекающая", DoorState = "закрыта"},
                new Door { DoorType = "Ляда 1 отсекающая", DoorState = "не определено"},

                new Door { DoorType = "Ляда 1 переключающая", DoorState = "открыта"},
                new Door { DoorType = "Ляда 1 переключающая", DoorState = "закрыта"},
                new Door { DoorType = "Ляда 1 переключающая", DoorState = "не определено"},
               
                new Door { DoorType = "Ляда 2 отсекающая", DoorState = "открыта"},
                new Door { DoorType = "Ляда 2 отсекающая", DoorState = "закрыта"},
                new Door { DoorType = "Ляда 2 отсекающая", DoorState = "не определено"},

                new Door { DoorType = "Ляда 2 переключающая", DoorState = "открыта"},
                new Door { DoorType = "Ляда 2 переключающая", DoorState = "закрыта"},
                new Door { DoorType = "Ляда 2 переключающая", DoorState = "не определено"},

                new Door { DoorType = "Ляда атмосферная", DoorState = "открыта" },
                new Door { DoorType = "Ляда атмосферная", DoorState = "закрыта" },
                new Door { DoorType = "Ляда атмосферная", DoorState = "не определено" },

                new Door { DoorType = "Ляда дифузорная", DoorState = "открыта" },
                new Door { DoorType = "Ляда дифузорная", DoorState = "закрыта" },
                new Door { DoorType = "Ляда дифузорная", DoorState = "не определено" },

                new Door { DoorType = "Ляда подводящего канал", DoorState = "открыта" },
                new Door { DoorType = "Ляда подводящего канал", DoorState = "закрыта" },
                new Door { DoorType = "Ляда подводящего канал", DoorState = "не определено" },

                new Door { DoorType = "Направляющий аппарата вентилятора 1", DoorState = "открыта" },
                new Door { DoorType = "Направляющий аппарата вентилятора 1", DoorState = "закрыта" },

                new Door { DoorType = "Направляющий аппарата вентилятора 2", DoorState = "открыта" },
                new Door { DoorType = "Направляющий аппарата вентилятора 2", DoorState = "закрыта" },
            };
            doors.ForEach(s => context.Doors.Add(s));
            context.SaveChanges();
        }
    }
}
