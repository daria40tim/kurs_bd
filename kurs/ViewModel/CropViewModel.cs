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
    public class CropViewModel:DependencyObject
    {
        public CropViewModel()
        {
            Collection.FillData();
            CropCollection = new ObservableCollection<Crop>();
            string queryString = "select crop_id, cult_id, value, data_of_crop from crops";
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
                            Cult = (Culture)Collection.Plants.Where(O=>O.Cult_id==(int)reader[0]).FirstOrDefault(),
                            Value = float.Parse(reader[2].ToString()),
                            Date = DateTime.Parse(reader[3].ToString())
                        };
                        CropCollection.Add(new_Card);
                        new_Card = null;
                    }
                }
                finally { reader.Close(); }
            }
        }

        public ObservableCollection<Crop> CropCollection
        {
            get { return (ObservableCollection<Crop>)GetValue(CropCollectionProperty); }
            set { SetValue(CropCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CropCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CropCollectionProperty =
            DependencyProperty.Register("CropCollection", typeof(ObservableCollection<Crop>), typeof(CropViewModel), new PropertyMetadata(null));


    }
}
