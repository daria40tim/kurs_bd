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
    public class MainWindowVM:DependencyObject
    {
        public MainWindowVM()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            Set = new DataSet();
            string queryString = "select * from stages;";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {

                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                connection.Open();
                adapter.Fill(Set);
                Table = Set.Tables[0];
                connection.Close();
            }
    }

        public DataTable Table
        {
            get { return (DataTable)GetValue(TableProperty); }
            set { SetValue(TableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Table.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TableProperty =
            DependencyProperty.Register("Table", typeof(DataTable), typeof(MainWindowVM), new PropertyMetadata(null));



        public DataSet Set
        {
            get { return (DataSet)GetValue(SetProperty); }
            set { SetValue(SetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Set.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SetProperty =
            DependencyProperty.Register("Set", typeof(DataSet), typeof(MainWindowVM), new PropertyMetadata(null));


    }
}
