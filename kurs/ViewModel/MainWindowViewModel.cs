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



        public string Temperature
        {
            get { return (string)GetValue(TemperatureProperty); }
            set { SetValue(TemperatureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Temperature.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemperatureProperty =
            DependencyProperty.Register("Temperature", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(null));



        public string Air_Humidity
        {
            get { return (string)GetValue(Air_HumidityProperty); }
            set { SetValue(Air_HumidityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Air_Humidity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Air_HumidityProperty =
            DependencyProperty.Register("Air_Humidity", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(null));



        public string Soil_Moisture
        {
            get { return (string)GetValue(Soil_MoistureProperty); }
            set { SetValue(Soil_MoistureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Soil_Moisture.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Soil_MoistureProperty =
            DependencyProperty.Register("Soil_Moisture", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(null));


        public string Lightning
        {
            get { return (string)GetValue(LightningProperty); }
            set { SetValue(LightningProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Lightning.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightningProperty =
            DependencyProperty.Register("Lightning", typeof(string), typeof(MainWindowViewModel), new PropertyMetadata(null));





    }
}
