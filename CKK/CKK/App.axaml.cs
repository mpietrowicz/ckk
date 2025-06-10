using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CKK.Abstraction;
using CKK.Services;
using CKK.ViewModels;
using CKK.Views;
using Splat;
using System;

namespace CKK
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            ConfigureDataValidation();
            ConfigureSplatIoc();
        }

        private void ConfigureSplatIoc()
        {
           Locator.CurrentMutable.RegisterLazySingleton(() => new PcService(), typeof(IPcService));
        }

        private static void ConfigureDataValidation()
        {
            // Remove the default Avalonia data validation plugin to avoid duplicate validations
            BindingPlugins.DataValidators.RemoveAt(0);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel()
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = new MainViewModel()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}