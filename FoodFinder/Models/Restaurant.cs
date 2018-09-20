using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FoodFinder;

namespace FoodFinder.Models
{
    public class Restaurant
    {
        private string _name;
        private int _id;

        public Restaurant (string name, int id = 0)
        {
            _name = name;
            _id = id;
        }

        public override bool Equals(System.Object otherRestaurant)
        {
            if (!(otherRestaurant is Restaurant))
            {
                return false;
            }
            else
            {
                Restaurant newRestaurant = (Restaurant) otherRestaurant;
                bool areIdsEqual = (this.GetRestaurantId() == newRestaurant.GetRestaurantId());
                bool areNamesEqual = (this.GetRestaurantName() == newRestaurant.GetRestaurantName());
                return (areIdsEqual && areNamesEqual);
            }
        }

        public override int GetHashCode()
        {
            return this.GetRestaurantId().GetHashCode();
        }

        public string GetRestaurantName()
        {
            return _name;
        }
        public int GetRestaurantId()
        {
            return _id;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO resteraunt name value @restaurantName;";

            MySqlParameter restaurantName = new MySqlParameter();
            restaurantName.ParameterName = "@restaurantName";
            restaurantName.Value = this._name;
            cmd.Parameters.Add(restaurantName);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM restaurant;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
