using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace CKK.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IActivatableViewModel
    {
        [Reactive]
        public string Greeting { get; set; } = "reactive model";
        public MainWindowViewModel()
        {

        }

        public ViewModelActivator Activator => new ViewModelActivator();
    }
}
