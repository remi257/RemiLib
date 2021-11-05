using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemiLib.Wpf.ExampleApp.DragDrop
{
    public class DragDropVM
    {
        public DragDropVM()
        {
            ListViewCollection.Add(new DragDropModel(42, "LV Lorem"));
            ListViewCollection.Add(new DragDropModel(11, "LV ipsum"));
            ListViewCollection.Add(new DragDropModel(257, "LV dolor"));
            ListViewCollection.Add(new DragDropModel(0, "LV sit"));

            ItemsControlCollection.Add(new DragDropModel(42, "SP Lorem"));
            ItemsControlCollection.Add(new DragDropModel(11, "SP ipsum"));
            ItemsControlCollection.Add(new DragDropModel(257, "SP dolor"));
            ItemsControlCollection.Add(new DragDropModel(0, "SP sit"));

            DockPanelCollection.Add(new DragDropModel(42, "DP Lorem"));
            DockPanelCollection.Add(new DragDropModel(11, "DP ipsum"));
            DockPanelCollection.Add(new DragDropModel(257, "DP dolor"));
            DockPanelCollection.Add(new DragDropModel(0, "DP sit"));
        }

        public ObservableCollection<DragDropModel> ListViewCollection { get; } = new();
        public ObservableCollection<DragDropModel> ItemsControlCollection { get; } = new();
        public ObservableCollection<DragDropModel> DockPanelCollection { get; } = new();
    }
}
