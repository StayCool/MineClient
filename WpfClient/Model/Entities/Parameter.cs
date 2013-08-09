namespace WpfClient.Model.Entities
{
    public class Parameter
    {
        public enum ValueState
        {
            Ok, Bad, Dangerous, None
        }

        public string Value { get; set; }
        public string Property { get; set; }

        public ValueState ParameterState { get; set; }
    }
}
