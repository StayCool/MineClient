using System;
using System.Collections.Generic;
using WpfClient.Model.Abstract;
using WpfClient.Services;

namespace WpfClient.Model.Concrete
{
    public class MsgParser : IMsgParser
    {
        private const int _expextedValueCount = 44;

        public Dictionary<DoorEnum, int> Parse(string message)
        {
            string parsedStr = message.Trim();
            var keyValueStr = message.Split(new char[] { ' ', '=' });

            if (keyValueStr.Length != _expextedValueCount)
                throw new Exception("Insufficient number of parameters accepted from remote fan");


            var paramTable = new Dictionary<DoorEnum, int>(_expextedValueCount)
            {
                { (DoorEnum)Enum.Parse(typeof(DoorEnum), keyValueStr[0]), int.Parse(keyValueStr[1])}
            };

            for (int i = 2; i < keyValueStr.Length; i += 2)
            {
                paramTable.Add((DoorEnum)Enum.Parse(typeof(DoorEnum), keyValueStr[i]), int.Parse(keyValueStr[i + 1]));
            }

            return paramTable;
        }
    }
}
