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
        private double _fansCount;
        private double _maxTemperature;
        private double _maxIndicatorValue;
        private double _parameterWarning;
        private double _parameterDanger;
        private double _maxPillowValue;

        public double FansCount 
        {
            get { return (_fansCount = getParameter("FanObjectCount", _fansCount)); }
            set { setParameter("FanObjectCount", ref _fansCount, value); }
        }

        public double MaxTemperature
        {
            get { return (_maxTemperature = getParameter("MaxTemperature", _maxTemperature)); }
            set { setParameter("MaxTemperature", ref _maxTemperature, value); }
        }

        public double MaxPillowValue
        {
            get { return (_maxPillowValue = getParameter("MaxPillowValue", _maxPillowValue)); } 
            set { setParameter("MaxPillowValue", ref _maxPillowValue, value); }
        }

        public double MaxIndicatorValue 
        {
            get { return (_maxIndicatorValue = getParameter("MaxIndicatorValue", _maxIndicatorValue)); }
            set { setParameter("MaxIndicatorValue", ref _maxIndicatorValue, value); }
        }

        public double ParameterWarning
        {
            get { return (_parameterWarning = getParameter("ParameterWarning", _parameterWarning)); }
            set { setParameter("ParameterWarning", ref _parameterWarning, value); }
        }

        public double ParameterDanger 
        {
            get { return (_parameterDanger = getParameter("ParameterDanger", _parameterDanger)); }
            set { setParameter("ParameterDanger", ref _parameterDanger, value); }
        }

        private double getParameter(string name, double value)
        {
            if (Math.Abs(value) < 1e-10)
            {
                value = double.Parse(ConfigurationManager.AppSettings[name]);
            }
            return value;
        }

        private void setParameter(string name, ref double oldValue, double newValue)
        {
            oldValue = 0;
            ConfigurationManager.AppSettings[name] = newValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}
