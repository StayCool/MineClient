using System.Collections.Generic;
using WpfClient.Services;

namespace WpfClient.Model.Abstract
{
    public interface IMsgParser
    {
        Dictionary<DoorEnum, int> Parse(string message);
    }
}
