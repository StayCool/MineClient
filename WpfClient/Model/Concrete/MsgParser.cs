using System;
using System.Collections.Generic;
using WpfClient.Model.Abstract;
using WpfClient.Services;

namespace WpfClient.Model.Concrete
{
    public class MsgParser : IMsgParser
    {
        private const int _expextedValueCount = 44;

        public Dictionary<string, int> Parse(string message)
        {
            string parsedStr = message.Trim();
            var keyValueStr = message.Split(new char[] { ' ', '=' });

            if (keyValueStr.Length < _expextedValueCount)
                throw new Exception("Insufficient number of parameters accepted from remote fan");

            var paramTable = new Dictionary<string, int>(_expextedValueCount/2);
            for (int i = 0; i < keyValueStr.Length; i += 2)
            {
                paramTable[keyValueStr[i]] = int.Parse(keyValueStr[i + 1]);
            }

            return paramTable;
        }
    }
}
