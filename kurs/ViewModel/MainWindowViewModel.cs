using kurs.Model;
using kurs.View;
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
    public class MainWindowViewModel : DependencyObject
    { 
        public MainWindowViewModel()
        {

            Collection.FillData();
            Temperature = 16;
            Air_humidity = 45;
            Soil_Moisture = 81;
            Lightning = 80;
            DataGridVM = new DataGridViewModel();
            TaskVM = new TaskViewModel();
            SelectedPlant = DataGridVM.SelectedPlant;
            CardsVM = new CardsViewModel(DataGridVM.SelectedPlant);
            InicializeCommands();
        }

        private void InicializeCommands()
        {
            CheckCommand = new BaseCommand(Check);
            ChangeStageCommand = new BaseCommand(ChangeStage);
        }

        private void ChangeStage()
        {
            ChangeStageViewModel ChangeStageVM = new ChangeStageViewModel(this.SelectedPlant);
            ChangeStageView ChangeStageV = new ChangeStageView();
            ChangeStageV.DataContext = ChangeStageVM;
            ChangeStageViewModel.OnClose += (closeResult) =>
            {
                ChangeStageV.Close();
                DataGridVM = new DataGridViewModel();
            };
            ChangeStageV.ShowDialog();
        }

        private void Check()
        {
            GetCropViewModel ChangeStageVM = new GetCropViewModel();
            GetCropView ChangeStageV = new GetCropView();
            ChangeStageV.DataContext = ChangeStageVM;
            GetCropViewModel.OnClose += (closeResult) =>
            {
                ChangeStageV.Close();
            };
            ChangeStageV.ShowDialog();
        }

        public Plant SelectedPlant
        {
            get { return (Plant)GetValue(SelectedPlantProperty); }
            set { SetValue(SelectedPlantProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedPlant.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPlantProperty =
            DependencyProperty.Register("SelectedPlant", typeof(Plant), typeof(MainWindowViewModel), new PropertyMetadata(null, OnSelectedPlantChanged));

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



        public BaseCommand ChangeStageCommand
        {
            get { return (BaseCommand)GetValue(ChangeStageCommandProperty); }
            set { SetValue(ChangeStageCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeStageCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChangeStageCommandProperty =
            DependencyProperty.Register("ChangeStageCommand", typeof(BaseCommand), typeof(MainWindowViewModel), new PropertyMetadata(null));



        public double Temperature
        {
            get { return (double)GetValue(TemperatureProperty); }
            set { SetValue(TemperatureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Temperature.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemperatureProperty =
            DependencyProperty.Register("Temperature", typeof(double), typeof(MainWindowViewModel), new PropertyMetadata(OnTemperatureChanged));

        private static void OnTemperatureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            foreach (var item in Collection.Plants)
            {
                if (item.Cards_of_plant.Where(o => o.Title == "Температура воздуха").FirstOrDefault() != null)
                {
                    float optimum = item.Cards_of_plant.Where(o => o.Title == "Температура воздуха").First().Optimal;
                    float tolerance = item.Cards_of_plant.Where(o => o.Title == "Температура воздуха").First().Tolerance;
                    float limit = item.Cards_of_plant.Where(o => o.Title == "Температура воздуха").First().Limit_deviation;
                    float max = optimum + limit;
                    float min = optimum - limit;
                    float min_range = optimum - tolerance;
                    float max_range = optimum + tolerance;
                    if (int.Parse(d.GetValue(TemperatureProperty).ToString()) <= min || int.Parse(d.GetValue(TemperatureProperty).ToString()) >= max)
                    {
                        string message = "Температура воздуха " + d.GetValue(TemperatureProperty).ToString() + " опасна для растений";
                        string InsString = "insert into errors values (default, @message_str, now(), @card_id)";
                        using (NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
                        {
                            con.Open();
                            NpgsqlCommand command = new NpgsqlCommand(InsString, con);
                            command.Parameters.AddWithValue("@message_str", message);
                            command.Parameters.AddWithValue("@card_id", item.Cards_of_plant.Where(o => o.Title == "Температура воздуха").First().Card_id);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public int Air_humidity
        {
            get { return (int)GetValue(Air_humidityProperty); }
            set { SetValue(Air_humidityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Air_humidity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Air_humidityProperty =
            DependencyProperty.Register("Air_humidity", typeof(int), typeof(MainWindowViewModel), new PropertyMetadata(OnAirHumidityChanged));

        private static void OnAirHumidityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            foreach (var item in Collection.Plants)
            {
                if (item.Cards_of_plant.Where(o => o.Title == "Влажность воздуха").FirstOrDefault() != null)
                {
                    float optimum = item.Cards_of_plant.Where(o => o.Title == "Влажность воздуха").First().Optimal;
                    float tolerance = item.Cards_of_plant.Where(o => o.Title == "Влажность воздуха").First().Tolerance;
                    float limit = item.Cards_of_plant.Where(o => o.Title == "Влажность воздуха").First().Limit_deviation;
                    float max = optimum + limit;
                    float min = optimum - limit;
                    float min_range = optimum - tolerance;
                    float max_range = optimum + tolerance;
                    if (int.Parse(d.GetValue(Air_humidityProperty).ToString()) <= min || int.Parse(d.GetValue(Air_humidityProperty).ToString()) >= max)
                    {
                        string message = "Влажность воздуха " + d.GetValue(Air_humidityProperty).ToString() + " опасна для растений";
                        string InsString = "insert into errors values (default, @message_str, now(), @card_id)";
                        using (NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
                        {
                            con.Open();
                            NpgsqlCommand command = new NpgsqlCommand(InsString, con);
                            command.Parameters.AddWithValue("@message_str", message);
                            command.Parameters.AddWithValue("@card_id", item.Cards_of_plant.Where(o => o.Title == "Влажность воздуха").First().Card_id);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public int Soil_Moisture
        {
            get { return (int)GetValue(Soil_MoistureProperty); }
            set { SetValue(Soil_MoistureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Soil_Moisture.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Soil_MoistureProperty =
            DependencyProperty.Register("Soil_Moisture", typeof(int), typeof(MainWindowViewModel), new PropertyMetadata(OnSoilMoistureChanged));

        private static void OnSoilMoistureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            foreach (var item in Collection.Plants)
            {
                if (item.Cards_of_plant.Where(o => o.Title == "Влажность почвы").FirstOrDefault() != null)
                {
                    float optimum = item.Cards_of_plant.Where(o => o.Title == "Влажность почвы").First().Optimal;
                    float tolerance = item.Cards_of_plant.Where(o => o.Title == "Влажность почвы").First().Tolerance;
                    float limit = item.Cards_of_plant.Where(o => o.Title == "Влажность почвы").First().Limit_deviation;
                    float max = optimum + limit;
                    float min = optimum - limit;
                    float min_range = optimum - tolerance;
                    float max_range = optimum + tolerance;
                    if (int.Parse(d.GetValue(Soil_MoistureProperty).ToString()) <= min || int.Parse(d.GetValue(Soil_MoistureProperty).ToString()) >= max)
                    {
                        string message = "Влажность почвы " + d.GetValue(Soil_MoistureProperty).ToString() + " опасна для растений";
                        string InsString = "insert into errors values (default, @message_str, now(), @card_id)";
                        using (NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
                        {
                            con.Open();
                            NpgsqlCommand command = new NpgsqlCommand(InsString, con);
                            command.Parameters.AddWithValue("@message_str", message);
                            command.Parameters.AddWithValue("@card_id", item.Cards_of_plant.Where(o => o.Title == "Влажность почвы").First().Card_id);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public int Lightning
        {
            get { return (int)GetValue(LightningProperty); }
            set { SetValue(LightningProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Lightning.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LightningProperty =
            DependencyProperty.Register("Lightning", typeof(int), typeof(MainWindowViewModel), new PropertyMetadata(OnLightningChanged));

        private static void OnLightningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            foreach (var item in Collection.Plants)
            {
                if (item.Cards_of_plant.Where(o => o.Title == "Освещенность").FirstOrDefault() != null)
                {
                    
                    float optimum = item.Cards_of_plant.Where(o => o.Title == "Освещенность").First().Optimal;
                    float tolerance = item.Cards_of_plant.Where(o => o.Title == "Освещенность").First().Tolerance;
                    float limit = item.Cards_of_plant.Where(o => o.Title == "Освещенность").First().Limit_deviation;
                    float max = optimum + limit;
                    float min = optimum - limit;
                    float min_range = optimum - tolerance;
                    float max_range = optimum + tolerance;
                    if (int.Parse(d.GetValue(LightningProperty).ToString()) <= min || int.Parse(d.GetValue(LightningProperty).ToString()) >= max)
                    {
                        string message = "Уровень освещения " + d.GetValue(LightningProperty).ToString() + " опасен для растений";
                        string InsString = "insert into errors values (default, @message_str, now(), @card_id)";
                        using (NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
                        {
                            con.Open();
                            NpgsqlCommand command = new NpgsqlCommand(InsString, con);
                            command.Parameters.AddWithValue("@message_str", message);
                            command.Parameters.AddWithValue("@card_id", item.Cards_of_plant.Where(o => o.Title == "Освещенность").First().Card_id);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public BaseCommand CheckCommand
        {
            get { return (BaseCommand)GetValue(CheckCommandProperty); }
            set { SetValue(CheckCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CheckCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckCommandProperty =
            DependencyProperty.Register("CheckCommand", typeof(BaseCommand), typeof(MainWindowViewModel), new PropertyMetadata(null));


    }

}
