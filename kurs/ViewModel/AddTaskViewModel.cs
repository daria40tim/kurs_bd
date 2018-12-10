using kurs.Model;
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
    public class AddTaskViewModel:DependencyObject
    {
        static public event Action<bool> OnClose;
        public AddTaskViewModel()
        {
            Collection.FillData();
            WorkersCollection = new ObservableCollection<Worker>();
            InitializeCommands();
            WorkersCollection = Collection.Workers;   
        }
        private void InitializeCommands()
        {
            OKCommand = new BaseCommand(AddTaskMethod);
        }

        private void AddTaskMethod()
        {
            string InsString = "insert into tasks values (default, @task_text, @worker_id, now()) returning *;";
            using (NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                NpgsqlCommand command = new NpgsqlCommand(InsString, con);
                command.Parameters.AddWithValue("@task_text", Text);
                command.Parameters.AddWithValue("@worker_id", SelectedWorker.Worker_id);
                command.ExecuteNonQuery();
            }
            OnClose(true);
        }

        public ObservableCollection<Worker> WorkersCollection
        {
            get { return (ObservableCollection<Worker>)GetValue(WorkersCollectionProperty); }
            set { SetValue(WorkersCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WorkersCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WorkersCollectionProperty =
            DependencyProperty.Register("WorkersCollection", typeof(ObservableCollection<Worker>), typeof(AddTaskViewModel), new PropertyMetadata(null));




        public Worker SelectedWorker
        {
            get { return (Worker)GetValue(SelectedWorkerProperty); }
            set { SetValue(SelectedWorkerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedWorker.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedWorkerProperty =
            DependencyProperty.Register("SelectedWorker", typeof(Worker), typeof(AddTaskViewModel), new PropertyMetadata(null));


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(AddTaskViewModel), new PropertyMetadata(null));



        public BaseCommand OKCommand
        {
            get { return (BaseCommand)GetValue(OKCommandProperty); }
            set { SetValue(OKCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OKCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OKCommandProperty =
            DependencyProperty.Register("OKCommand", typeof(BaseCommand), typeof(AddTaskViewModel), new PropertyMetadata(null));


    }
}
