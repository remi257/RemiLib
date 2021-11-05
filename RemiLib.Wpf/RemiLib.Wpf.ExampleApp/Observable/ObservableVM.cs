using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemiLib.Wpf.Observable;

namespace RemiLib.Wpf.ExampleApp.Observable
{
    public class ObservableVM
    {
        public ObservableProperty<int> SliderValue { get; set; } = new(50);
        public ObservableProperty<string> StringValue { get; set; } = new("abc");
    }
}
