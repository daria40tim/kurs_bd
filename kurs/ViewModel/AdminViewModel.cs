using kurs.Model;
using kurs.View;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.ViewModel
{
   public  class AdminViewModel:DependencyObject
    {
        public AdminViewModel()
        {
            CropVM = new CropViewModel();
            ErrorVM = new ErrorGridViewModel();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            ValueFilterCommand = new BaseCommand(ValueFilter);
            DataFilterCommand = new BaseCommand(DataFilter);
            AddCultureCommand = new BaseCommand(AddCulture);
            AddTaskCommand = new BaseCommand(AddTask);
        }

        private void AddTask()
        {
            MessageBox.Show("10");
            AddTaskViewModel ChangeStageVM = new AddTaskViewModel();
            AddTaskView ChangeStageV = new AddTaskView();
            ChangeStageV.DataContext = ChangeStageVM;
            AddTaskViewModel.OnClose += (closeResult) =>
            {
                ChangeStageV.Close();
            };
            ChangeStageV.ShowDialog();
        }

        private void AddCulture()
        {
            AddCultureViewModel ChangeStageVM = new AddCultureViewModel();
            AddCultureView ChangeStageV = new AddCultureView();
            ChangeStageV.DataContext = ChangeStageVM;
            AddCultureViewModel.OnClose += (closeResult) =>
            {
                ChangeStageV.Close();
            };
            ChangeStageV.ShowDialog();
        }

        public BaseCommand AddTaskCommand
        {
            get { return (BaseCommand)GetValue(AddTaskCommandProperty); }
            set { SetValue(AddTaskCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddTaskCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddTaskCommandProperty =
            DependencyProperty.Register("AddTaskCommand", typeof(BaseCommand), typeof(AdminViewModel), new PropertyMetadata(null));


        public BaseCommand AddCultureCommand
        {
            get { return (BaseCommand)GetValue(AddCultureCommandProperty); }
            set { SetValue(AddCultureCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddCultureCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddCultureCommandProperty =
            DependencyProperty.Register("AddCultureCommand", typeof(BaseCommand), typeof(AdminViewModel), new PropertyMetadata(null));




        public void DataFilter()
        {
            CropVM.CropCollection = new ObservableCollection<Crop>();
            string queryString = "select crop_id, cult_id, value, data_of_crop from crops order by data_of_crop";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Crop new_Card = new Crop
                        {
                            Crop_id = (int)reader[0],
                            Cult = (Culture)Collection.Plants.Where(O => O.Cult_id == (int)reader[0]).FirstOrDefault(),
                            Value = float.Parse(reader[2].ToString()),
                            Date = DateTime.Parse(reader[3].ToString())
                        };
                        CropVM.CropCollection.Add(new_Card);
                        new_Card = null;
                    }
                }
                finally { reader.Close(); }
            }
        }

        public void ValueFilter()
        {
            MessageBox.Show("10");
            CropVM.CropCollection = new ObservableCollection<Crop>();
            string queryString = "select crop_id, cult_id, value, data_of_crop from crops order by value desc";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Crop new_Card = new Crop
                        {
                            Crop_id = (int)reader[0],
                            Cult = (Culture)Collection.Plants.Where(O => O.Cult_id == (int)reader[0]).FirstOrDefault(),
                            Value = float.Parse(reader[2].ToString()),
                            Date = DateTime.Parse(reader[3].ToString())
                        };
                        CropVM.CropCollection.Add(new_Card);
                        new_Card = null;
                    }
                }
                finally { reader.Close(); }
            }
        }

        public BaseCommand ValueFilterCommand
        {
            get { return (BaseCommand)GetValue(ValueFilterCommandProperty); }
            set { SetValue(ValueFilterCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueFilterCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueFilterCommandProperty =
            DependencyProperty.Register("ValueFilterCommand", typeof(BaseCommand), typeof(AdminViewModel), new PropertyMetadata(null));


        public BaseCommand DataFilterCommand
        {
            get { return (BaseCommand)GetValue(DataFilterCommandProperty); }
            set { SetValue(DataFilterCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataFilterCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataFilterCommandProperty =
            DependencyProperty.Register("DataFilterCommand", typeof(BaseCommand), typeof(AdminViewModel), new PropertyMetadata(null));





        public ErrorGridViewModel ErrorVM
        {
            get { return (ErrorGridViewModel)GetValue(ErrorVMProperty); }
            set { SetValue(ErrorVMProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorVM.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorVMProperty =
            DependencyProperty.Register("ErrorVM", typeof(ErrorGridViewModel), typeof(AdminViewModel), new PropertyMetadata(null));



        public CropViewModel CropVM
        {
            get { return (CropViewModel)GetValue(CropVMProperty); }
            set { SetValue(CropVMProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CropVM.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CropVMProperty =
            DependencyProperty.Register("CropVM", typeof(CropViewModel), typeof(AdminViewModel), new PropertyMetadata(null));


    }
}
