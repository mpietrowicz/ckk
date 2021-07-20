using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ckk.ViewModels;
using ReactiveUI;

namespace ckk
{
    public class ViewModelBase<Treactive> : ReactiveObject where Treactive : ViewModelBase<Treactive>
    {
        public bool Initjalizing { get; set; } = true;

        public ViewModelBase()
        {
            Changed.Where(anytypeChecker).Subscribe();
        }
        private bool anytypeChecker(IReactivePropertyChangedEventArgs<IReactiveObject> reactivePropertyChangedEventArgs)
        {
            if (!Initjalizing)
            {
                var containsArgument = reactivePropertyChangedEventArgs.Sender.GetType().GetProperties()
                    .First(x => x.Name == reactivePropertyChangedEventArgs.PropertyName)?.GetCustomAttributes().ToList().Select(x=> x.GetType());
                if (containsArgument.Contains(typeof(DataMemberAttribute)))
                {
                    StateManager<Treactive>.Instance.Set(reactivePropertyChangedEventArgs.Sender as Treactive);
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
