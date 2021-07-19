using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ckk.ViewModels;
using MahApps.Metro.Controls;
using ReactiveUI;

namespace ckk
{
    public class WindowCustomBase<TViewModel> : MetroWindow, IViewFor<TViewModel> where TViewModel : class
    {
        /// <summary>
        /// The view model dependency property.
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                "ViewModel",
                typeof(TViewModel),
                typeof(ReactiveWindow<TViewModel>),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets the binding root view model.
        /// </summary>
        public TViewModel? BindingRoot => ViewModel;

        /// <inheritdoc/>
        public TViewModel ViewModel
        {
            get => (TViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        /// <inheritdoc/>
        object? IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (TViewModel?)value;
        }
    }
}
