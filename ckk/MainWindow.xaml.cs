using System;
using System.Reactive.Disposables;
using ckk.ViewModels;
using MahApps.Metro.Controls;
using ReactiveUI;

namespace ckk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowCustomBase<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = StateManager<MainWindowViewModel>.Instance.Get(new MainWindowViewModel());
            this.WhenActivated(disposableRegistration =>
            {
                this.Bind(ViewModel, model => model.ClosePc, window => window.ClosePc.IsChecked)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel, model => model.RebootPc, window => window.RebootPc.IsChecked)
                    .DisposeWith(disposableRegistration);


                this.BindCommand(ViewModel, model => model.StartCountingCommand, window => window.Run)
                    .DisposeWith(disposableRegistration);
                this.Bind(ViewModel, model => model.Hours, window => window.hours.Text)
                    .DisposeWith(disposableRegistration);
                this.Bind(ViewModel, model => model.Minutes, window => window.minutes.Text)
                    .DisposeWith(disposableRegistration);
                this.Bind(ViewModel, model => model.Seconds, window => window.seconds.Text)
                    .DisposeWith(disposableRegistration);
                this.Bind(ViewModel, model => model.RemainingTime.Hours, window => window.hoursDisplay.Content)
                    .DisposeWith(disposableRegistration);
                this.Bind(ViewModel, model => model.RemainingTime.Minutes, window => window.minutesDisplay.Content)
                    .DisposeWith(disposableRegistration);
                this.Bind(ViewModel, model => model.RemainingTime.Seconds, window => window.secondsDisplay.Content)
                    .DisposeWith(disposableRegistration);
                this.Bind(ViewModel, model => model.RemainingTime.Milliseconds, window => window.milisecondsDisplay.Content )
                    .DisposeWith(disposableRegistration);
                this.BindCommand(ViewModel, model => model.CancelCommand, window => window.Cancel)
                    .DisposeWith(disposableRegistration);
                this.Bind(ViewModel, model => model.DisplayTextGoButton, window => window.Run.Content)
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}