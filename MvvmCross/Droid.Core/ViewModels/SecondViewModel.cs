using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace Droid.Core.ViewModels
{
    public class SecondViewModel : MvxViewModel
    {
        public string ButtonText => "Click me";

        public ICommand MyCommand
        {
            get
            {
                return new MvxCommand(() =>
              {
                  List.Add(new Person { Name = DateTime.Now.ToString("F") });

                  ;
              });
            }
        }

        public ObservableCollection<Person> List { get; set; }

        public SecondViewModel()
        {
            List = new ObservableCollection<Person>();
        }
    }

    public class Person
    {
        public string Name { get; set; }
    }
}