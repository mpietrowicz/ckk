using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ckk.ViewModels
{
    [Serializable]
    public class MainWindowViewModel : ViewModelBase<MainWindowViewModel>
    {

        [Reactive]
        public string DisplayTextGoButton { get; set; }
        [DataMember]
        [Reactive]
        public int Hours { get; set; }
        [DataMember]
        [Reactive]
        public bool ClosePc { get; set; }
        [DataMember]
        [Reactive]
        public bool RebootPc { get; set; }
        [DataMember]
        [Reactive]
        public int Minutes { get; set; }
        [DataMember]
        [Reactive]
        public int Seconds { get; set; }

        [Reactive]
        public TimeSpan StartTime { get; set; }

        [Reactive]
        public TimeSpan RemainingTime { get; set; }

        [Reactive]
        public Subject<Unit> Cancle { get; set; }


        public ReactiveCommand<Unit, TimeSpan> StartCountingCommand { get; set; }

        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public MainWindowViewModel()
        {
            ClosePc = true;
            RebootPc = false;
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
            StartTime = new TimeSpan(0, 0, 0, 0);
            var defaultValue = new TimeSpan(0, 0, 0, 0);
            Cancle = new Subject<Unit>();

            DisplayTextGoButton = "OK";

           

            StartCountingCommand = ReactiveCommand.CreateFromObservable<Unit, TimeSpan>(
                _ =>
                {

                    StartTime = new TimeSpan(0, Hours, Minutes, Seconds);

                    return Observable.Interval(TimeSpan.FromMilliseconds(50))
                        .Select(x =>
                        {
                            return StartTime - TimeSpan.FromMilliseconds(x * 50);
                        })
                        .TakeWhile(x => x >= TimeSpan.FromSeconds(0)).TakeUntil(CancelCommand);
                });
            this.CancelCommand = ReactiveCommand.Create(
                () =>
                {
                    StartTime = new TimeSpan(0, 0, 0, 0);
                    RemainingTime = new TimeSpan(0, 0, 0, 0);

                },
                this.StartCountingCommand.IsExecuting);
            StartCountingCommand
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(span => RemainingTime = span);

            StartCountingCommand
               .Where(z => z == defaultValue)
               .TakeUntil(CancelCommand)
               .Subscribe(x =>
               {

                   if (RebootPc)
                   {
#if !DEBUG
                       System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0"); 
#endif
                       MessageBox.Show("Reboot");
                   }
                   if (ClosePc)
                   {
#if !DEBUG
                       System.Diagnostics.Process.Start("shutdown.exe", "-s -t 0"); 
#endif
                       MessageBox.Show("Close");
                   }

               });
        }


    }
}
