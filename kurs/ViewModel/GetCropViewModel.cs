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
    class GetCropViewModel:DependencyObject
    {
        static public event Action<bool> OnClose;
        public GetCropViewModel()
        {
            CultCollection = new ObservableCollection<Culture>();
            InitializeCommands();
            foreach (var item in Collection.Plants.Where(o=>o.Stage_id.Title=="Плодоношение").Distinct())
            {
                Culture _new_Cult = new Culture
                {
                    Cult_id = item.Cult_id,
                    Sort = item.Sort,
                    Type = item.Type
                };
                CultCollection.Add(_new_Cult);
            }
        }

        private void InitializeCommands()
        {
            OKCommand = new BaseCommand(GetCropMethod);
        }

        public void GetCropMethod()
        {
            string InsString = "insert into crops values (default, @cult_id, @value, now()) returning *;";
            using (NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                con.Open();
                NpgsqlCommand command = new NpgsqlCommand(InsString, con);
                command.Parameters.AddWithValue("@cult_id", SelectedCult.Cult_id);
                command.Parameters.AddWithValue("@value", CropCount);
                command.ExecuteNonQuery();
            }
            OnClose(true);
        }

        public BaseCommand OKCommand
        {
            get { return (BaseCommand)GetValue(OKCommandProperty); }
            set { SetValue(OKCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OKCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OKCommandProperty =
            DependencyProperty.Register("OKCommand", typeof(BaseCommand), typeof(GetCropViewModel), new PropertyMetadata(null));


        public ObservableCollection<Culture> CultCollection
        {
            get { return (ObservableCollection<Culture>)GetValue(CultCollectionProperty); }
            set { SetValue(CultCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CultCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CultCollectionProperty =
            DependencyProperty.Register("CultCollection", typeof(ObservableCollection<Culture>), typeof(GetCropViewModel), new PropertyMetadata(null));



        public Culture SelectedCult
        {
            get { return (Culture)GetValue(SelectedCultProperty); }
            set { SetValue(SelectedCultProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedCult.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedCultProperty =
            DependencyProperty.Register("SelectedCult", typeof(Culture), typeof(GetCropViewModel), new PropertyMetadata(null));


        public float CropCount
        {
            get { return (float)GetValue(CropCountProperty); }
            set { SetValue(CropCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CropCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CropCountProperty =
            DependencyProperty.Register("CropCount", typeof(float), typeof(GetCropViewModel), new PropertyMetadata(null));




    }
}
