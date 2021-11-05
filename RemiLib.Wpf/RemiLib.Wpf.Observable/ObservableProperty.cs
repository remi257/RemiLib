using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RemiLib.Wpf.Observable
{
    public class ObservableProperty<T> : ObservableObject
    {
        public T Value { get => value; set => SetProperty(ref this.value, value); }
        private T value;

        public ObservableProperty(T value)
        {
            Value = value;
        }

        public ObservableProperty()
        {
        }
    }
}
