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

            //Init DoorType table
            var doorType = new List<DoorType>
            {
                new DoorType {Type = "Ляда 1 отсекающая"},
                new DoorType {Type = "Ляда 1 переключающая"},
                new DoorType {Type = "Ляда 2 отсекающая"},
                new DoorType {Type = "Ляда 2 переключающая"},
                new DoorType {Type = "Ляда атмосферная"},
                new DoorType {Type = "Ляда дифузорная"},
                new DoorType {Type = "Ляда подводящего канала"},
                new DoorType {Type = "Направляющий аппарата вентилятора 1"},
                new DoorType {Type = "Направляющий аппарата вентилятора 2"}
            };
            doorType.ForEach(d => context.DoorsType.Add(d));

            //Init DoorState table
            var doorState = new List<DoorState>
            {
                new DoorState {State = "Неопределен"},
                new DoorState {State = "Закрыт"},
                new DoorState {State = "Открыт"},
                new DoorState {State = "Неопределен"}
            };
            doorState.ForEach(d => context.DoorState.Add(d));

            //Init FanState table
            var fanState = new List<FanState>
            {
                new FanState {State = "Выключен"},
                new FanState {State = "Включен"},
            };
            fanState.ForEach(d => context.FanState.Add(d));

            //Init AnalogSignalType table
            var analogSignalType = new List<AnalogSignal>
            {
                new AnalogSignal {Type = "Аналоговый сигнал 1"},
                new AnalogSignal {Type = "Аналоговый сигнал 2"},
                new AnalogSignal {Type = "Аналоговый сигнал 3"}
            };
            analogSignalType.ForEach(s => context.AnalogSignals.Add(s));
            
            context.SaveChanges();
        }
    }
}
