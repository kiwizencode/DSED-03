using ReactiveUI;

using System;
using System.Collections.Generic;
using System.Reactive.Linq;

using Guess5App.Shared.Helper;
using Guess5App.Shared.Model;
using Guess5App.Shared.DataAccessObject;
using System.Reactive.Concurrency;
using System.Reactive;

namespace Guess5App.Droid.ViewModel
{
    public class ViewModel_Game : ReactiveObject
    {
        public bool Run_Flag { get; set; } = true;


        private int _score;
        public string Score { get => _score.ToString(); set { } }

        private static int MAX_TICK { get; set; } = 20;
        public void TimerTick()
        {
            _timer = (_timer == 0) ? MAX_TICK : _timer - 1;
            this.RaisePropertyChanged("Timer");
        }

        private int _timer = 0;
        public string Timer
        {
            /*  How to add zero-padding to a string
                https://stackoverflow.com/questions/3122677/add-zero-padding-to-a-string */
            get => _timer.ToString().PadLeft(2, '0');
            set
            {
                if (int.TryParse(value, out int i)) this.RaiseAndSetIfChanged(ref _timer, i);
            }
        }

        private readonly ReactiveCommand _tickCommand;
        public ReactiveCommand TickCommand => _tickCommand ;

        public ViewModel_Game()
        {

            //var mainSequence = Observable.Interval(TimeSpan.FromSeconds(1));
            //var run_is_true = from i in mainSequence
            //                  where Run_Flag == true
            //                  select i;

            //run_is_true.Subscribe(x => {
            //    //ThreadPool.QueueUserWorkItem(_ =>
            //    //{
            //    //RunOnUiThread(() => ViewModel.TimerTick());
            //    TimerTick();
            //    //});
            //});


            //_tickCommand = ReactiveCommand.Create(() => TimerTick(), outputScheduler: RxApp.MainThreadScheduler );
            //_tickCommand.e
        }

 
    }
}