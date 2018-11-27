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
    public class ChangeStageViewModel:DependencyObject
    {
        
        public Stage SelectedStage
        {
            get { return (Stage)GetValue(SelectedStageProperty); }
            set { SetValue(SelectedStageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedStage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedStageProperty =
            DependencyProperty.Register("SelectedStage", typeof(Stage), typeof(ChangeStageViewModel), new PropertyMetadata(null));


        public string PlantCount
        {
            get { return (string)GetValue(PlantCountProperty); }
            set { SetValue(PlantCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlantCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlantCountProperty =
            DependencyProperty.Register("PlantCount", typeof(string), typeof(ChangeStageViewModel), new PropertyMetadata(null));




        public ObservableCollection<Stage> StageCollection
        {
            get { return (ObservableCollection<Stage>)GetValue(StageCollectionProperty); }
            set { SetValue(StageCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StageCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StageCollectionProperty =
            DependencyProperty.Register("StageCollection", typeof(ObservableCollection<Stage>), typeof(ChangeStageViewModel), new PropertyMetadata(null));



        public Plant SelectedPlant
        {
            get { return (Plant)GetValue(SelectedPlantProperty); }
            set { SetValue(SelectedPlantProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedPlant.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedPlantProperty =
            DependencyProperty.Register("SelectedPlant", typeof(Plant), typeof(ChangeStageViewModel), new PropertyMetadata(null));



        public BaseCommand OKCommand
        {
            get { return (BaseCommand)GetValue(OKCommandProperty); }
            set { SetValue(OKCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OKCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OKCommandProperty =
            DependencyProperty.Register("OKCommand", typeof(BaseCommand), typeof(ChangeStageViewModel), new PropertyMetadata(null));


        static public event Action<bool> OnClose;

        public ChangeStageViewModel(Plant selectedPlant)
        {
            InitializeCommads();
            SelectedPlant = selectedPlant;
           // Collection.Plants.Where(o => o.Plant_id == selectedPlant.Plant_id).FirstOrDefault().Count = Collection.Plants.Where(o => o.Plant_id == selectedPlant.Plant_id).FirstOrDefault().Count - PlantCount;
            PlantCount = selectedPlant.Count.ToString();
            StageCollection = Collection.Stages;
        }

        private void InitializeCommads()
        { 
            OKCommand = new BaseCommand(OKMethod);
        }

        public void OKMethod()
        {
            
            int count;
            try
            {
                count = int.Parse(PlantCount);
            }
            catch (Exception e)
            {
                MessageBox.Show("Проверьте правильность введенного числа");
                return;
            }
            if (count <= 0 || count > SelectedPlant.Count)
            {
                MessageBox.Show("Введенное число является недействительным");
                return;
            }
            int index_of_existing_plant = -1;
            if (Collection.Plants.Where(o => o.Cult_id == SelectedPlant.Cult_id && o.House_id == SelectedPlant.House_id && o.Stage_id == SelectedStage).FirstOrDefault() != null)
            {
                index_of_existing_plant = Collection.Plants.Where(o => o.Cult_id == SelectedPlant.Cult_id && o.House_id == SelectedPlant.House_id && o.Stage_id == SelectedStage).FirstOrDefault().Plant_id;
            }
            Plant old_Plant = SelectedPlant;
            if (count <= SelectedPlant.Count)
            {
                if (index_of_existing_plant != -1)
                {
                    Collection.Plants.Where(o => o.Plant_id == old_Plant.Plant_id).FirstOrDefault().Count -= count;
                    Collection.Plants.Where(o => o.Plant_id == index_of_existing_plant).FirstOrDefault().Count += count;
                    string InsString = "update plants set count = @count where p_id = @p_id returning *";
                    using (NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
                    {
                        con.Open();
                        NpgsqlCommand command = new NpgsqlCommand(InsString, con);
                        command.Parameters.AddWithValue("@p_id", index_of_existing_plant);
                        command.Parameters.AddWithValue("@count", Collection.Plants.Where(o => o.Plant_id == index_of_existing_plant).FirstOrDefault().Count);
                        command.ExecuteNonQuery();
                    }
                    InsString = "update plants set count = @count where p_id = @p_id returning *";
                    using (NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
                    {
                        con.Open();
                        NpgsqlCommand command = new NpgsqlCommand(InsString, con);
                        command.Parameters.AddWithValue("@p_id", SelectedPlant.Plant_id);
                        command.Parameters.AddWithValue("@count", Collection.Plants.Where(o => o.Plant_id == old_Plant.Plant_id).FirstOrDefault().Count);
                        command.ExecuteNonQuery();
                    }
                    OnClose(true);
                }
                else
                {
                    int p_id;
                    Collection.Plants.Where(o => o.Plant_id == old_Plant.Plant_id).FirstOrDefault().Count -= count;
                    string InsString = "insert into plants values (default, @house_id, @stage_id, @cult_id, @count) returning *;";
                    using (NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
                    {
                        con.Open();
                        NpgsqlCommand command = new NpgsqlCommand(InsString, con);
                        command.Parameters.AddWithValue("@house_id", SelectedPlant.House_id);
                        command.Parameters.AddWithValue("@stage_id", SelectedStage.ID);
                        command.Parameters.AddWithValue("@cult_id", SelectedPlant.Cult_id);
                        command.Parameters.AddWithValue("@count", count);
                       p_id= (int)command.ExecuteScalar();
                    }
                    Plant new_Plant = new Plant
                    {
                        Cards_of_plant = SelectedPlant.Cards_of_plant,
                        Count = count,
                        Cult_id = SelectedPlant.Cult_id,
                        House_id = SelectedPlant.House_id,
                        Sort = SelectedPlant.Sort,
                        Type = SelectedPlant.Type,
                        Stage_id = SelectedStage,
                        Plant_id = p_id
                    };
                    Collection.Plants.Add(new_Plant);
                    new_Plant = null;
                    InsString = "update plants set count = @count where p_id = @p_id returning *";
                    using (NpgsqlConnection con = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
                    {
                        con.Open();
                        NpgsqlCommand command = new NpgsqlCommand(InsString, con);
                        command.Parameters.AddWithValue("@p_id", SelectedPlant.Plant_id);
                        command.Parameters.AddWithValue("@count", old_Plant.Count-count);
                        command.ExecuteNonQuery();
                    }
                    OnClose(true);
                }
            }
            OnClose(true);
        }

    }
}
