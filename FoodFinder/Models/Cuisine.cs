using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using FoodFinder;

namespace FoodFinder.Models
{
    public class Cuisine
    {
        private string _name;
        private int _id;

        public Cuisine(string name, int id =0)
        {
            _name = name;
            _id = id;
        }
        public override bool Equals(System.Object otherCuisine)
        {
            if (!(otherCuisine is Cuisine))
            {
                return false;
            }
            else
            {
                Cuisine newCuisine = (Cuisine) otherCuisine;
                bool areIdsEqual = (this.GetCuisineId() == newCuisine.GetCuisineId());
                bool areNamesEqual = (this.GetCuisineName() == newCuisine.GetCuisineName());
                return (areIdsEqual && areNamesEqual);
            }
        }

        public override int GetHashCode()
        {
            return this.GetCuisineId().GetHashCode();
        }

        public string GetCuisineName()
        {
            return _name;
        }

        public int GetCuisineId()
        {
            return _id;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO cuisine name value @name;";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Cuisine> GetAll()
        {
            List<Cuisine> allCuisine = new List<Cuisine> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var command = conn.CreateCommand() as MySqlCommand;
            command.CommandText = @"SELECT * FROM cuisine;";

            var rdr = command.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Cuisine newCuisine = new Cuisine(name,id);
                allCuisine.Add(newCuisine);
            }

            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }

            return allCuisine;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM cuisine;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static Cuisine Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM cuisine WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int cuisineId = 0;
            string cusisineName = "";
            while (rdr.Read())
            {
                cuisineId = rdr.GetInt32(0);
                cusisineName = rdr.GetString(1);
            }
            Cuisine newCuisine = new Cuisine(cuisineId, cusisineName);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newCuisine;
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM cuisine WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = _id;
            cmd.Parameters.Add(thisId);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM cuisine;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public List<Cuisine> GetCuisine()
        {
            List<Cuisine> allCuisine = new List<Cuisine>;
            MySqlConnection conn = DB.Connection();
            conn.Open;

            MySqlCommand cmd = CreateCommand() as MySqlCommand;
            var cmd = @"SELECT * FROM cuisine;";

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int CuisineId = rdr.GetInt32(0);
                string CuisineName = rdr.GetString(1);
                Cuisine newCuisine = new Cuisine(CuisineName, CuisineId);
                allCategories.Add(newCuisine);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCuisine;
        }
        public static Cuisine Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM cuisine WHERE id = @searchId;";
        }
    }
}
