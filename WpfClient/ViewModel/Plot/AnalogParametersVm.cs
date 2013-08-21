using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataRepository.DataAccess.GenericRepository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Ninject.Parameters;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using WpfClient.Model;
using WpfClient.ViewModel.General;


namespace WpfClient.ViewModel.Plot
{
    public class AnalogParametersVm : ViewModelBase
    {
        private int _fanObjectId;
        private PlotModel _plotModel;
        public PlotModel PlotModel
        {
            get { return _plotModel; }
            set { _plotModel = value; RaisePropertyChanged("PlotModel"); }
        }
        private RelayCommand _backArrowClickCommand;
        public ICommand BackArrowClick
        {
            get { return _backArrowClickCommand ?? (_backArrowClickCommand = new RelayCommand(BackArrowClickHandler)); }
        }

        private void BackArrowClickHandler()
        {
            IoC.Resolve<MainVm>().CurrentView = IoC.Resolve<GeneralVm>();
        }

        public AnalogParametersVm(int fanObjectId)
        {
            _fanObjectId = fanObjectId;
            PlotModel = new PlotModel();
            SetUpModel();
        }

        private readonly List<OxyColor> colors = new List<OxyColor> { OxyColors.Green, OxyColors.IndianRed, OxyColors.Coral, OxyColors.Chartreuse, OxyColors.Azure };

        private readonly List<MarkerType> markerTypes = new List<MarkerType> { MarkerType.Plus, MarkerType.Star, MarkerType.Diamond, MarkerType.Triangle, MarkerType.Cross };

        private void SetUpModel()
        {
            PlotModel.Title = "График аналоговых параметров вентиляторной установки №" + _fanObjectId.ToString();
            PlotModel.Subtitle = "Подробнее (CTRL + ЛКМ)";
            PlotModel.SubtitleColor = OxyColors.LightGreen;
            PlotModel.LegendTitle = "Легенда";
            PlotModel.LegendOrientation = LegendOrientation.Horizontal;
            PlotModel.LegendPlacement = LegendPlacement.Outside;
            PlotModel.LegendPosition = LegendPosition.TopRight;
            PlotModel.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            PlotModel.LegendBorder = OxyColors.Black;
            PlotModel.MouseDown += (s, e) =>
                {
                    try
                    {
                        if (e.IsControlDown)
                        {
                            DataPoint date = (DataPoint) e.HitTestResult.Item;
                            IoC.Resolve<MainVm>().CurrentView =
                                IoC.Resolve<OnPlotClickVm>(new ConstructorArgument("fanObjectId", _fanObjectId),
                                                           new ConstructorArgument("date",
                                                                                   DateTimeAxis.ToDateTime(date.X)),
                                                           new ConstructorArgument("prevView",
                                                                                   IoC.Resolve<MainVm>().CurrentView));
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }
                };
            var dateAxis = new DateTimeAxis(AxisPosition.Bottom, "Дата", "yyyy-MM-dd HH:mm:ss") { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 150 };
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis(AxisPosition.Left, 0) { MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Значение" };
            PlotModel.Axes.Add(valueAxis);

        }

        private void LoadData(IEnumerable<MeasurementVm> measurements)
        {
            if (!measurements.Any()) { return; }

            var lineSerie = new LineSeries
            {
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerStroke = colors[0],
                MarkerType = markerTypes[0],
                CanTrackerInterpolatePoints = false,
                Title = measurements.First().ParamName,
                Smooth = false,
            };
 
            foreach (var data in measurements)
            {
                lineSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(data.DateTime),data.Value));
            }
            
            PlotModel.Series.Add(lineSerie);
        }

        public void ShowSignal(int id, DateTime from = default(DateTime), DateTime to = default(DateTime))
        {

            if (id <= 0) return;

            if (to == default (DateTime)) to = DateTime.Now;

            if (from == default(DateTime)) from = to - new TimeSpan(24, 0, 0);

            using (var repoUnit = new RepoUnit())
            {
                var tmp = repoUnit.FanLog.Load().Where(f => f.FanNumber == _fanObjectId && f.Date > from && f.Date < to)
                    .Select(f => new { Date = f.Date, AnalogSignal = f.AnalogSignalLogs.FirstOrDefault(a => a.SignalTypeId == id) })
                    .Select(s => new MeasurementVm { DateTime =  s.Date, Value = s.AnalogSignal.SignalValue, ParamName = s.AnalogSignal.SignalType.Type});

                LoadData(tmp.ToList());
            }
        }
    }
}
