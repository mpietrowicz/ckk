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
            ViewModel = new MainWindowViewModel();
            this.WhenActivated(disposableRegistration =>
            {
                this.BindCommand(ViewModel, model => model.StartCountingCommand, window => window.Run)
                    .DisposeWith(disposableRegistration);
                this.Bind(ViewModel, model => model.Hours, window => window.hours.Text)
                    .DisposeWith(disposableRegistration);
                this.Bind(ViewModel, model => model.Minutes, window => window.minutes.Text)
                    .DisposeWith(disposableRegistration);
                this.Bind(ViewModel, model => model.Seconds, window => window.seconds.Text)
                    .DisposeWith(disposableRegistration);
                this.Bind(ViewModel, model => model.RemainingTime, window => window.DisplayTime.Content)
                    .DisposeWith(disposableRegistration);
                this.BindCommand(ViewModel, model => model.CancelCommand, window => window.Cancel)
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}