using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Model.Abstract;

namespace WpfClient.Services
{
    class MineConfig : IConfig
    {
        private int _fansCount;
        private int _maxTemperature;
        private int _maxIndicatorValue;
        private int _parameterWarning;
        private int _parameterDanger;

        public int FansCount {
            get
            {
                if (_fansCount == 0)
                {
                    _fansCount = int.Parse(ConfigurationManager.AppSettings["FanObjectCount"]);
                }
                return _fansCount;
            }
            set
            {
                _fansCount = 0;
                ConfigurationManager.AppSettings["FanObjectCount"] = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public int MaxTemperature
        {
            get
            {
                if (_maxTemperature == 0)
                {
                    _maxTemperature = int.Parse(ConfigurationManager.AppSettings["MaxTemperature"]);
                }
                return _maxTemperature;
            }
            set
            {
                _maxTemperature = 0;
                ConfigurationManager.AppSettings["MaxTemperature"] = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public int MaxIndicatorValue {
            get
            {
                if (_maxIndicatorValue == 0)
                {
                    _maxIndicatorValue = int.Parse(ConfigurationManager.AppSettings["MaxIndicatorValue"]);
                }
                return _maxIndicatorValue;
            }
            set
            {
                _maxIndicatorValue = 0;
                ConfigurationManager.AppSettings["MaxTemperature"] = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public int ParameterWarning
        {
            get
            {
                if (_parameterWarning == 0)
                {
                    _parameterWarning = int.Parse(ConfigurationManager.AppSettings["ParameterWarning"]);
                }
                return _parameterWarning;
            }
            set
            {
                _parameterWarning = 0;
                ConfigurationManager.AppSettings["ParameterWarning"] = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public int ParameterDanger 
        {
            get 
            {
                if (_parameterDanger == 0) 
                {
                    _parameterDanger = int.Parse(ConfigurationManager.AppSettings["ParameterDanger"]);
                }
                return _parameterDanger;
            }
            set 
            {
                _parameterDanger = 0;
                ConfigurationManager.AppSettings["ParameterDanger"] = value.ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}
