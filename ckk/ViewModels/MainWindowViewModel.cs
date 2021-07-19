using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ckk.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        [Reactive]
        public int Hours { get; set; }
        
        [Reactive]
        public int Minutes { get; set; }
        
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
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
            StartTime = new TimeSpan(0,0,0,0);
            var defaultValue  = new TimeSpan(0,0,0,0);
            Cancle = new Subject<Unit>();
          
            StartCountingCommand=ReactiveCommand.CreateFromObservable<Unit, TimeSpan>(
                _ =>
                {
                    StartTime = new TimeSpan(0,Hours,Minutes,Seconds);
                    
                    return Observable.Interval(TimeSpan.FromMilliseconds(10))
                        .Select(x =>
                        {
                            return StartTime - TimeSpan.FromMilliseconds(x*10);
                        })
                        .TakeWhile(x => x >= TimeSpan.FromSeconds(0)).TakeUntil(CancelCommand);
                });
            this.CancelCommand = ReactiveCommand.Create(
                () =>
                {
                    StartTime = new TimeSpan(0,0,0,0);
                },
                this.StartCountingCommand.IsExecuting);
            StartCountingCommand
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(span => RemainingTime = span);

            StartCountingCommand
               .Where(z=> z == defaultValue)
               .Subscribe(x => Console.WriteLine(x));
        }

       
    }
}
