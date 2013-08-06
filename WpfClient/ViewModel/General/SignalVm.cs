namespace WpfClient.ViewModel.General
{
    public class SignalVm
    {
        public enum ValueState
        {
            Ok, Bad, Dangerous
        }

        public string Value { get; set; }
        public ValueState SignalState { get; set; }
    }
}
