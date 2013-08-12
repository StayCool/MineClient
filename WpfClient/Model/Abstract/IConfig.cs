namespace WpfClient.Model.Abstract
{
    public interface IConfig
    {
        int FansCount { get; set; }

        int MaxTemperature { get; set; }

        int MaxIndicatorValue { get; set; }

        int ParameterWarning { get; set; }

        int ParameterDanger { get; set; }
    }
}
