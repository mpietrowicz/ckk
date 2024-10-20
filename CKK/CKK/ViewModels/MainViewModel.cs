using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

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
        private bool _isRunning = false;

        private DispatcherTimer _timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1),
            IsEnabled = false,

        };

        public MainViewModel()
        {
            _timer.Tick += OnTimerTick;
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

        [RelayCommand(CanExecute = nameof(CanStart))]
        private async Task Start()
        {
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

            await Task.CompletedTask;
        }
    }
}
