using System;
using WpfClient.Model;
using WpfClient.Model.Concrete;
using WpfClient.Model.Entities;

namespace WpfClient.Services
{
    class SystemStateService
    {
        public static StateEnum GetParameterState(int value)
        {
            var warningValue = Config.Instance.ParameterWarning;
            var dangerValue = Config.Instance.ParameterDanger;

            if (value < warningValue) return StateEnum.Ok;

            return value >= dangerValue ? StateEnum.Dangerous : StateEnum.Warning;
        }

        public static StateEnum GetFanState(int workingFan)
        {
            return workingFan == 0 ? StateEnum.Dangerous : StateEnum.Ok;
        }

        
    }
}
