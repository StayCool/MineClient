namespace WpfClient.Model.Abstract
{
    public interface IConfig
    {
        double FansCount { get; set; }

        double MaxTemperature { get; set; }

        double MaxPillowValue { get; set; }

        double MaxIndicatorValue { get; set; }

        double ParameterWarning { get; set; }

        double ParameterDanger { get; set; }
    }
}
