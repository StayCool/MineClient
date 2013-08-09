using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Mc.CustomControls.Model;
using WpfClient.Model;
using System.Linq;
using WpfClient.Services;
using Entity = WpfClient.Model.Entities;

namespace WpfClient.ViewModel.FanObject
{
    class TubeSystemVm : INotifyPropertyChanged
    {
        private string _fanMode;
        private FanService _fanService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<DoorStateEnum> DoorsState { get; set; }
 
        #region Property
        private bool _rotationV1;
        public bool RotationV1
        {
            get { return _rotationV1; }
            set { _rotationV1 = value; OnPropertyChanged("RotationV1"); }
        }
        private bool _rotationV2;
        public bool RotationV2
        {
            get { return _rotationV2; }
            set { _rotationV2 = value; OnPropertyChanged("RotationV2"); }
        }

        private DoorStateEnum _lo1;
        public DoorStateEnum Lo1
        {
            get { return _lo1; }
            set { _lo1 = value; OnPropertyChanged("Lo1"); }
        }
        private DoorStateEnum _lo2;
        public DoorStateEnum Lo2
        {
            get { return _lo2; }
            set { _lo2 = value; OnPropertyChanged("Lo2"); }
        }
        private DoorStateEnum _lp1;
        public DoorStateEnum Lp1
        {
            get { return _lp1; }
            set { _lp1 = value; OnPropertyChanged("Lp1"); }
        }
        private DoorStateEnum _lp2;
        public DoorStateEnum Lp2
        {
            get { return _lp2; }
            set { _lp2 = value; OnPropertyChanged("Lp2"); }
        }
        private DoorStateEnum _na1;
        public DoorStateEnum Na1
        {
            get { return _na1; }
            set { _na1 = value; OnPropertyChanged("Na1"); }
        }
        private DoorStateEnum _na2;
        public DoorStateEnum Na2
        {
            get { return _na2; }
            set { _na2 = value; OnPropertyChanged("Na2"); }
        }
        private DoorStateEnum _la;
        public DoorStateEnum La
        {
            get { return _la; }
            set { _la = value; OnPropertyChanged("La"); }
        }
        private DoorStateEnum _lpk;
        public DoorStateEnum Lpk
        {
            get { return _lpk; }
            set { _lpk = value; OnPropertyChanged("Lpk"); }
        }
        private DoorStateEnum _ld;
        public DoorStateEnum Ld
        {
            get { return _ld; }
            set { _ld = value; OnPropertyChanged("Ld"); }
        }

        private string _v1onDown;
        public string V1onDown
        {
            get { return _v1onDown; }
            set { _v1onDown = value; OnPropertyChanged("V1onDown"); }
        }
        private string _v1onTop;
        public string V1onTop
        {
            get { return _v1onTop; }
            set { _v1onTop = value; OnPropertyChanged("V1onTop"); }
        }
        private string _v1onLeft;
        public string V1onLeft
        {
            get { return _v1onLeft; }
            set { _v1onLeft = value; OnPropertyChanged("V1onLeft"); }
        }
        private string _v1onReight;
        public string V1onReight
        {
            get { return _v1onReight; }
            set { _v1onReight = value; OnPropertyChanged("V1onReight"); }
        }

        private string _v2onDown;
        public string V2onDown
        {
            get { return _v2onDown; }
            set { _v2onDown = value; OnPropertyChanged("V2onDown"); }
        }
        private string _v2onTop;
        public string V2onTop
        {
            get { return _v2onTop; }
            set { _v2onTop = value; OnPropertyChanged("V2onTop"); }
        }
        private string _v2onLeft;
        public string V2onLeft
        {
            get { return _v2onLeft; }
            set { _v2onLeft = value; OnPropertyChanged("V2onLeft"); }
        }
        private string _v2onReight;
        public string V2onReight
        {
            get { return _v2onReight; }
            set { _v2onReight = value; OnPropertyChanged("V2onReight"); }
        }

        private string _workReight;
        public string WorkReight
        {
            get { return _workReight; }
            set { _workReight = value; OnPropertyChanged("WorkReight"); }
        }
        private string _workLeft;
        public string WorkLeft
        {
            get { return _workLeft; }
            set { _workLeft = value; OnPropertyChanged("WorkLeft"); }
        }

        
        private string _normaTop;
        public string NormaTop
        {
            get { return _normaTop; }
            set { _normaTop = value; OnPropertyChanged("NormaTop"); }
        }
        private string _normaLeft;
        public string NormaLeft
        {
            get { return _normaLeft; }
            set { _normaLeft = value; OnPropertyChanged("NormaLeft"); }
        }

        private string _normaLeftReversReight;
        public string NormaLeftReversReight
        {
            get { return _normaLeftReversReight; }
            set { _normaLeftReversReight = value; OnPropertyChanged("NormaLeftReversReight"); }
        }
        private string _normaReightReversLeft;
        public string NormaReightReversLeft
        {
            get { return _normaReightReversLeft; }
            set { _normaReightReversLeft = value; OnPropertyChanged("NormaReightReversLeft"); }
        }

        private string _reversLeft;
        public string ReversLeft
        {
            get { return _reversLeft; }
            set { _reversLeft = value; OnPropertyChanged("ReversLeft"); }
        }

        private string _reversDown;
        public string ReversDown
        {
            get { return _reversDown; }
            set { _reversDown = value; OnPropertyChanged("ReversDown"); }
        }

        #endregion

        #region PrivateMethods
        private void ClearTubes()
        {
            ReversDown = "Stop";
            ReversLeft = "Stop";
            NormaReightReversLeft = "Stop";
            NormaLeftReversReight = "Stop";
            NormaLeft = "Stop";
            NormaTop = "Stop";
            WorkLeft = "Stop";
            WorkReight = "Stop";
            V1onDown = "Stop";
            V1onLeft = "Stop";
            V1onReight = "Stop";
            V1onTop = "Stop";
            V2onDown = "Stop";
            V2onLeft = "Stop";
            V2onReight = "Stop";
            V2onTop = "Stop";
        }
        private void V1Work()
        {
            V1onDown = "Down";
            V1onLeft = "Left";
            V1onReight = "Reight";
            V1onTop = "Top";
        }
        private void V2Work()
        {
            V2onDown = "Down";
            V2onLeft = "Left";
            V2onReight = "Reight";
            V2onTop = "Top";
        }
        private void Norma()
        {
            NormaLeft = "Left";
            NormaTop = "Top";
            NormaLeftReversReight = "Left";
            NormaReightReversLeft = "Reight";
            WorkLeft = "Left";
            WorkReight = "Reight";
        }
        private void Revers()
        {
            ReversDown = "Down";
            ReversLeft = "Left";
            NormaLeftReversReight = "Reight";
            NormaReightReversLeft = "Left";
            WorkLeft = "Left";
            WorkReight = "Reight";
        }
        #endregion PrivateMethods


        public string FanMode
        {
            get { return _fanMode; }
            set
            {
                _fanMode = value;
                OnPropertyChanged("FanMode");
            }
        }

        public TubeSystemVm(FanService fanService)
        {
            ClearTubes();
            _fanService = fanService;
            DoorsState = new ObservableCollection<DoorStateEnum>();
            for (int i = 0; i < 15; i++)
                DoorsState.Add(DoorStateEnum.Open);
        }

        public void Update(Entity.FanObject fanObject)
        {
            SetFanMode(fanObject);
            SetWorkingFan(fanObject.WorkingFanNumber);
            SetDoorsState(fanObject.Doors);
        }     

        public void SetWorkingFan(int workingFan)
        {
            RotationV1 = workingFan == 1;
            RotationV2 = workingFan == 2;
            setArrows(workingFan);
        }

        private void setArrows(int workingFan) 
        {
            ClearTubes();
            switch (FanMode) 
            {
                case "Реверс": Revers(); break;
                case "Норма": Norma(); break;
            }
            if (workingFan == 1) V1Work();
            if (workingFan == 2) V2Work();
        }

        public void SetFanMode(Entity.FanObject fanObject)
        {
            FanMode = _fanService.GetFanMode(fanObject.WorkingFanNumber, fanObject.Doors);
        }

        public void SetDoorsState(List<Entity.Door> doors)
        {
            foreach (var type in Enum.GetValues(typeof (DoorType)))
            {
                var stateId = doors.First(d => d.TypeId == (int) type).StateId;
                DoorsState[(int) type] = Enum.IsDefined(typeof(DoorStateEnum), stateId) ? (DoorStateEnum)stateId : DoorStateEnum.Undefined ;
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
