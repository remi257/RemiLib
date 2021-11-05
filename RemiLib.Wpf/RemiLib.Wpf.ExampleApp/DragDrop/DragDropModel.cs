using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemiLib.Wpf.Observable;

namespace RemiLib.Wpf.ExampleApp.DragDrop
{
    public class DragDropModel
    {
        public DragDropModel(int number, string text)
        {
            Number.Value = number;
            Text.Value = text;
        }

        public ObservableProperty<int> Number { get; set; } = new();
        public ObservableProperty<string> Text { get; set; } = new();
    }
}
