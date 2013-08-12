using System.Globalization;
using WpfClient.Model;
using WpfClient.Model.Entities;

namespace WpfClient.ViewModel
{
    public class ParameterVm
    {        
        public ParameterVm(Parameter parameter)
        {
            Value = parameter.Value.ToString(CultureInfo.InvariantCulture);
            Name = parameter.Name;
            State = parameter.State;
        }

        public ParameterVm()
        {
        }

        public string Value { get; set; }
        public string Name { get; set; }
        public StateEnum State { get; set; }
    }
}
