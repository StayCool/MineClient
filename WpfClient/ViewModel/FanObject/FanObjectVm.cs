using System;
using System.Windows.Threading;
using GalaSoft.MvvmLight;
using Ninject.Parameters;
using WpfClient.Model;
using WpfClient.Services;

namespace WpfClient.ViewModel.FanObject
{
    class FanObjectVm : ViewModelBase
    {
        private DatabaseService _databaseService;
        private readonly int _fanObjectId;

        public TubeSystemVm TubeSystemVm { get; set; }
        public ThermometerVm ThermometerVm { get; set; }
        public IndicatorVm IndicatorVm { get; set; }

        public FanObjectVm(int fanObjectId, DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _fanObjectId = fanObjectId;

            TubeSystemVm = IoC.Resolve<TubeSystemVm>(new ConstructorArgument("fanObjectId", fanObjectId));
            AsyncProvider.StartTimer(2000, Update);
        }


        public void Update()
        {
            var fanObject = _databaseService.GetFanObject(_fanObjectId);

            TubeSystemVm.Update(fanObject);
            //IndicatorVm.Update();
        }


    }
}
