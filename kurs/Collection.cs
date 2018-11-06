using kurs.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs
{
    public static class Collection
    {
        public static int house = 0;
        public static ObservableCollection<Plant> Plants { get; set; }
        public static ObservableCollection<Model.Task> Tasks { get; set; }
        public static ObservableCollection<Worker> Workers { get; set; }
        public static ObservableCollection<Card> Cards { get; set; }

        public static void ReadPlants(int house)
        {
            string queryString = "select p_id, s_title, t_title, st_title, count,cult_id, house_id from plants natural join stages natural join cultures natural join sorts natural join types where cultures.house_id = @house_id order by p_id ";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@house_id", house);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Plant new_plant = new Plant
                        {
                            Plant_id = (int)reader[0],
                            Sort = reader[1].ToString(),
                            Type = reader[2].ToString(),
                            Stage = reader[3].ToString(),
                            Count = (int)reader[4],
                            Cult_id = (int)reader[5],
                            House_id = (int)reader[6],
                            Cards_of_plant = new List<Card>()
                        };
                        Plants.Add(new_plant);
                        new_plant = null;
                    }
                }
                finally { reader.Close(); }
            }
        }
        public static void ReadTasks()
        {
            string queryString = "select task_id, task_text, day_of_app from tasks";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Model.Task new_Task = new Model.Task
                        {
                            Task_id = (int)reader[0],
                            Text = reader[1].ToString(),
                            End_date = DateTime.Now,
                            App_date = (DateTime)reader[2]
                        };
                        Tasks.Add(new_Task);
                        new_Task = null;
                    }
                }
                finally { reader.Close(); }
            }
        }
        public static void ReadCards(int house)
        {
            string queryString = "select card_id, card_title, st_title, cult_id, optimal, tolerance, limit_dev from cards natural join stages natural join plants where house_id=@house_id";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@house_id", house);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Card new_Card = new Card
                        {
                            Card_id = (int)reader[0],
                            Title = reader[1].ToString(),
                            Stage = reader[2].ToString(),
                            Cult_id = (int)reader[3],
                            Optimal = (float)reader[4],
                            Tolerance = (float)reader[5],
                            Limit_deviation = (float)reader[6]
                        };
                        if (Plants.First() != null)
                        {
                            Plants.Where(p => (p.Cult_id == new_Card.Cult_id) && (p.Stage == new_Card.Stage)).First().Cards_of_plant.Add(new_Card);
                        }
                        Cards.Add(new_Card);
                            new_Card = null;
                    }
                }
                finally { reader.Close(); }
            }
        }
        public static void ReadWorkers()
        {
            string queryString = "select worker_id, fio, login, password, position, house_id from workers";
            using (NpgsqlConnection connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(queryString, connection);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        int house = 0;
                        if (reader[5].ToString() != "")
                        {
                           house = int.Parse(reader[5].ToString());
                        }
                            Worker new_Worker = new Worker
                        {
                            Worker_id = (int)reader[0],
                            FIO = reader[1].ToString(),
                            Login = reader[2].ToString(),
                            Password = reader[3].ToString(),
                            Position = reader[4].ToString(),
                            House_id = house
                    };
                        Workers.Add(new_Worker);
                        new_Worker = null;
                    }
                }
                finally { reader.Close(); }
            }
        }
        public static void FillData()
        {
            Plants = new ObservableCollection<Plant>();
            Tasks = new ObservableCollection<Model.Task>();
            Workers = new ObservableCollection<Worker>();
            Cards = new ObservableCollection<Card>();
            ReadPlants(house);
            ReadCards(house);
            ReadTasks();
            ReadWorkers();
            
        }
    }
}