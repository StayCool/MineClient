using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfClient.Model.Abstract
{
    public interface IMsgParser
    {
        Dictionary<string, int> Parse(string message);
    }
}
