using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using Splat;
using CKK.Abstraction;

namespace CKK.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(StartCommand))]
        private int? _hours = 0;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(StartCommand))]
        private int? _minutes = 0;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(StartCommand))]
        private int? _seconds = 0;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(StartCommand))]
        [NotifyCanExecuteChangedFor(nameof(StopCommand))]
        private bool _isRunning = false;
        
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(StartCommand))]
        [NotifyCanExecuteChangedFor(nameof(StopCommand))]
        private bool _isStoped = false;
        
        
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(StartCommand))]
        private string? _selectedAction = "Shutdown";

        public ObservableCollection<string> AvailableActions { get; set; } = new ObservableCollection<string>()
        {
            "Shutdown",
            "Restart",
            "Sleep",
            "Hibernate"
        };

        private IPcService? _pcService;

        private IPcService PcService => _pcService ??= Locator.Current.GetService<IPcService>() ?? throw new InvalidOperationException("PC Service is not registered.");


        private DispatcherTimer? _timer = null;

        public MainViewModel()
        {
        
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            var currentTime = new TimeSpan(Hours ?? 0, Minutes ?? 0, Seconds ?? 0);
            currentTime = currentTime.Subtract(TimeSpan.FromSeconds(1));
            Hours = currentTime.Hours;
            Minutes = currentTime.Minutes;
            Seconds = currentTime.Seconds;

            if (currentTime.TotalSeconds <= 0)
            {
                _timer.Stop();
                IsRunning = false;
            }
        }

        private bool CanStart()
        {
            if (IsRunning)
            {
                return false;
            }
            var timespan = new TimeSpan(Hours ?? 0, Minutes ?? 0, Seconds ?? 0);
            if (timespan.TotalSeconds > 0)
                return true;
            else
                return false;

        }
        
      
        
        private bool CanStop()
        {
            if (IsRunning)
            {
                return !CanStart();
            }
            else
            {
                return false;
            }

        }
        

        [RelayCommand(CanExecute = nameof(CanStop))]
        private void Stop()
        {
            IsStoped = true;
            IsRunning = false;
        }
        

        [RelayCommand(CanExecute = nameof(CanStart))]
        private async Task Start()
        {
            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1),
                IsEnabled = false,

            };
            _timer.Tick += OnTimerTick;
            IsStoped = false;
            IsRunning = true;
            _timer.Start();
            
            
            var initTime = new TimeSpan(Hours ?? 0, Minutes ?? 0, Seconds ?? 0);
            do
            {
                await Task.Delay(100);

            } while (IsRunning);

            Hours = initTime.Hours;
            Minutes = initTime.Minutes;
            Seconds = initTime.Seconds;
            _timer.IsEnabled = false;
            _timer.Stop();
            _timer = null;

            if(SelectedAction == "Shutdown")
            {
                await PcService.ShutdownPc();
            }
            else if (SelectedAction == "Restart")
            {
                await PcService.RestartPc();
            }
            else if (SelectedAction == "Sleep")
            {
                await PcService.SleepPc();
            }
            else if (SelectedAction == "Hibernate")
            {
                await PcService.HibernatePc();
            }

            await Task.CompletedTask;
        }
    }
}
