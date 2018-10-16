using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.ViewModel
{
    public class TaskViewModel:DependencyObject
    {
        public TaskViewModel()
        {
            Collection.FillData();
            TaskCollection = Collection.Tasks;
        }


        public ObservableCollection<Model.Task> TaskCollection
        {
            get { return (ObservableCollection<Model.Task>)GetValue(TaskCollectionProperty); }
            set { SetValue(TaskCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskCollectionProperty =
            DependencyProperty.Register("TaskCollection", typeof(ObservableCollection<Model.Task>), typeof(TaskViewModel), new PropertyMetadata(null));


    }
}
