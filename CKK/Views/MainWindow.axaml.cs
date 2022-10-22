using Avalonia.ReactiveUI;
using CKK.ViewModels;

namespace CKK.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}