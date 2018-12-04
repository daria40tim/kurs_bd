using kurs.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.ViewModel
{
    public class MainWindowViewModel:DependencyObject
    {
        public MainWindowViewModel()
        {
            Collection.FillData();
            DataGridVM = new DataGridViewModel();
            TaskVM = new TaskViewModel();
            SelectedPlant = DataGridVM.SelectedPlant;
            CardsVM = new CardsViewModel(DataGridVM.SelectedPlant);
        }



        public Plant SelectedPlant
        {
            get { return (Plant)GetValue(SelectedPlantProperty); }
            set { SetValue(SelectedPlantProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedPlant.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPlantProperty =
            DependencyProperty.Register("SelectedPlant", typeof(Plant), typeof(MainWindowViewModel), new PropertyMetadata(null,OnSelectedPlantChanged));

        private static void OnSelectedPlantChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mainWindow = d as MainWindowViewModel;
            if (mainWindow == null) return;
            mainWindow.CardsVM = new CardsViewModel(mainWindow.SelectedPlant);
        }

        public CardsViewModel CardsVM
        {
            get { return (CardsViewModel)GetValue(CardsVMProperty); }
            set { SetValue(CardsVMProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CardsVM.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CardsVMProperty =
            DependencyProperty.Register("CardsVM", typeof(CardsViewModel), typeof(MainWindowViewModel), new PropertyMetadata(null));



        public TaskViewModel TaskVM
        {
            get { return (TaskViewModel)GetValue(TaskVMProperty); }
            set { SetValue(TaskVMProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskVM.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskVMProperty =
            DependencyProperty.Register("TaskVM", typeof(TaskViewModel), typeof(MainWindowViewModel), new PropertyMetadata(null));


        public DataGridViewModel DataGridVM
        {
            get { return (DataGridViewModel)GetValue(DataGridVMProperty); }
            set { SetValue(DataGridVMProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataGridVM.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataGridVMProperty =
            DependencyProperty.Register("DataGridVM", typeof(DataGridViewModel), typeof(MainWindowViewModel), new PropertyMetadata(null));



    }
}
