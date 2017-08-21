using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using ReactiveUI;
using HangmanApp.Droid.ViewModel;

namespace HangmanApp.Droid.Activities
{
    [Activity(Label = "HangmanApp", MainLauncher = true)]
    public class Activity_MainScreen : ReactiveActivity // , IViewFor<ViewModel_MainScreen>
    {
        /*
#region Implementation of IViewFor<> Interface
        private ViewModel_MainScreen _model;
        public ViewModel_MainScreen ViewModel {
            get => _model ;
            set => this.RaiseAndSetIfChanged(ref _model, value); 
        }
        object IViewFor.ViewModel {
            get => _model;
            set =>  ViewModel = (ViewModel_MainScreen) value; 
        }
        #endregion
        */
        Button btnStartGame;


        private TextView textViewMessage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Layout_MainScreen);

            //Initializer();
        }
        /*
        private void Initializer()
        {
            btnStartGame = FindViewById<Button>(Resource.Id.btnStartGame);

            //btnStartGame.SetOnHoverListener(new ButtonHoverOverEvent(btnStartGame, this.ViewModel));


            textViewMessage = FindViewById<TextView>(Resource.Id.textViewMessage);


            this.Bind(this.ViewModel, x => x.Toast, x => x.textViewMessage.Text);
        }

        class ButtonHoverOverEvent : View.IOnHoverListener
        {
            private Button _btnptr;
            private ViewModel_MainScreen _vm;

            public ButtonHoverOverEvent(Button btn, ViewModel_MainScreen vm)
            {
                _btnptr = btn;
                _vm = vm;
            }

            public IntPtr Handle => throw new NotImplementedException();

            public bool OnHover(View v, MotionEvent e)
            {
                switch(e.Action)
                {
                    case MotionEventActions.HoverEnter:
                        _vm.Toast = _btnptr.Text;
                        break;
                    case MotionEventActions.HoverExit: break;
                }
                return false;
            }

            #region IDisposable Support
            private bool disposedValue = false; // To detect redundant calls

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects).
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                    // TODO: set large fields to null.

                    disposedValue = true;
                }
            }

            // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
            // ~ButtonHoverOverEvent() {
            //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            //   Dispose(false);
            // }

            // This code added to correctly implement the disposable pattern.
            public void Dispose()
            {
                // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
                Dispose(true);
                // TODO: uncomment the following line if the finalizer is overridden above.
                // GC.SuppressFinalize(this);
            }
            #endregion
        }

    */
    }
}