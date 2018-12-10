using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kurs.ViewModel
{
    public class AddCultureViewModel:DependencyObject
    {
        static public event Action<bool> OnClose;
        public AddCultureViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            OKCommand = new BaseCommand(AddNewCulture);
        }

        private void AddNewCulture()
        {
            int s_number = 0; int t_number = 0;
            string InsString = "insert into sorts values (default, @new_sort) returning *;";
            using (NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                NpgsqlCommand command = new NpgsqlCommand(InsString, con);
                command.Parameters.AddWithValue("@new_sort", CultureSort);
                s_number= int.Parse(command.ExecuteScalar().ToString());
            }
            InsString = "insert into types values (default, @new_type) returning *;";
            using (NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                NpgsqlCommand command = new NpgsqlCommand(InsString, con);
                command.Parameters.AddWithValue("@new_type", CultureType);
                t_number = int.Parse(command.ExecuteScalar().ToString());
            }
            InsString = "insert into cultures values (default, @new_sort, @new_type) returning *;";
            using (NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                NpgsqlCommand command = new NpgsqlCommand(InsString, con);
                command.Parameters.AddWithValue("@new_type", t_number);
                command.Parameters.AddWithValue("@new_sort", s_number);
                command.ExecuteNonQuery();
            }
            OnClose(true);
        }

        public string CultureSort
        {
            get { return (string)GetValue(CultureSortProperty); }
            set { SetValue(CultureSortProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CultureSort.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CultureSortProperty =
            DependencyProperty.Register("CultureSort", typeof(string), typeof(AddCultureViewModel), new PropertyMetadata(null));


        public string CultureType
        {
            get { return (string)GetValue(CultureTypeProperty); }
            set { SetValue(CultureTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CultureType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CultureTypeProperty =
            DependencyProperty.Register("CultureType", typeof(string), typeof(AddCultureViewModel), new PropertyMetadata(null));



        public BaseCommand OKCommand
        {
            get { return (BaseCommand)GetValue(OKCommandProperty); }
            set { SetValue(OKCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OKCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OKCommandProperty =
            DependencyProperty.Register("OKCommand", typeof(BaseCommand), typeof(AddCultureViewModel), new PropertyMetadata(null));




    }
}
