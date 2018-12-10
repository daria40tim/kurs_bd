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
    public class ErrorGridViewModel:DependencyObject
    {
        public ErrorGridViewModel()
        {
            Collection.FillData();
            ErrorCollection = new ObservableCollection<Error>();
            string queryString = "select text, card_id, data_of_err from errors";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Error new_Card = new Error
                        {
                            Text = reader[0].ToString(),
                            Card_id = int.Parse(reader[1].ToString()),
                            Data = DateTime.Parse(reader[2].ToString())
                        };
                        ErrorCollection.Add(new_Card);
                        new_Card = null;
                    }
                }
                finally { reader.Close(); }

            }
        }
        public ObservableCollection<Error> ErrorCollection
        {
            get { return (ObservableCollection<Error>)GetValue(ErrorCollectionProperty); }
            set { SetValue(ErrorCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorCollectionProperty =
            DependencyProperty.Register("ErrorCollection", typeof(ObservableCollection<Error>), typeof(ErrorGridViewModel), new PropertyMetadata(null));

    }

}
