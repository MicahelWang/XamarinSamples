using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace Droid.Core.ViewModels
{
    public class FirstViewModel:MvxViewModel
    {
        private string _hello = "Hello MvvmCross";

        public string Hello
        {
            get
            {
                return _hello;
            }

            set
            {
                _hello = value;
                RaisePropertyChanged(() => Hello);
            }
        }

        public string ButtonText => "Click me";


        public ICommand MyCommand
        {
            get { return new MvxCommand(() => ShowViewModel<SecondViewModel>(), () => true); }
        }
    }
}