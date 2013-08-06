using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Services;

namespace MineClient.Rules
{
    public class Config
    {
        private static readonly IConfig _config = IoC.Resolve<IConfig>();

        public static IConfig Instance
        {
            get { return _config; }
        }
    }
}
