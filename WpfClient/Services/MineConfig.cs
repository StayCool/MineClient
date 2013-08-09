using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Model.Abstract;

namespace WpfClient.Services
{
    class MineConfig : IConfig
    {
        private int? _fansCount;

        public int FansCount {
            get
            {
                if (_fansCount == null)
                {
                    _fansCount = _fansCount = int.Parse(ConfigurationManager.AppSettings["FanObjectCount"]);
                }
                return _fansCount.Value;
            }
            set
            {
                _fansCount = null;
                ConfigurationManager.AppSettings["FanObjectCount"] = value.ToString();
            }
        }
    }
}
