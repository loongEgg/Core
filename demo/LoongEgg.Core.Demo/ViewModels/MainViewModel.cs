using System;
using System.Threading.Tasks;
using System.Timers;

namespace LoongEgg.Core.Demo
{
    public class MainViewModel : BindableObject
    {
        public uint PhoneNum
        {
            get { return _PhoneNum; }
            set { SetProperty(ref _PhoneNum, value); }
        }
        private uint _PhoneNum;

        public bool PhoneNumValid => PhoneNum.ToString().Length > 2;

        public DelegateCommand DelegateCallCommand { get; }
        public RelayCommand RelayCallCommand { get; }

        public MainViewModel()
        {
            DelegateCallCommand = new DelegateCommand(DelegateCall, CanDelegateCall);
            RelayCallCommand = new RelayCommand(async () => { await RelayCallAsync(); }, () => PhoneNumValid && IsRelayCalling == false); ;
        }

        private bool CanDelegateCall() => PhoneNumValid && IsDelegateCalling == false;

        public bool IsDelegateCalling
        {
            get { return _IsDelegateCalling; }
            set { SetProperty(ref _IsDelegateCalling, value); }
        }
        private bool _IsDelegateCalling;

        public bool IsRelayCalling
        {
            get { return _IsRelayCalling; }
            set { SetProperty(ref _IsRelayCalling, value); }
        }
        private bool _IsRelayCalling;

        private void DelegateCall()
        {
            IsDelegateCalling = true;
            var timer = new Timer
            {
                Interval = 5000,
                Enabled = true
            };
            timer.Elapsed += (s, e) => IsDelegateCalling = false;
        }
        private async Task RelayCallAsync()
        {
            IsRelayCalling = true;
            await Task.Delay(5000);
            IsRelayCalling = false;
        }
    }
}
