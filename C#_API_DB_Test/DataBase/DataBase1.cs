using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace C__API_DB_Tests.DataBase
{
    public class DataBase1
    {
        private MySqlConnection _connection;
        private string _connectionString = "Server=localhost;Database=classicmodels;User Id=root;Password=123123;";


        // Constructor: Initialize connection to the database
        public DataBase1()
        {
            try
            {
                _connection = new MySqlConnection(_connectionString);
                _connection.Open();
                Console.WriteLine("Connected to the database.");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Database connection failed.");
                Console.WriteLine(ex.Message);
            }
        }

        // Fetch data from a table
        public DataTable GetData(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (MySqlCommand command = new MySqlCommand(query, _connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error fetching data.");
                Console.WriteLine(ex.Message);
            }
            return dataTable;
        }

        // Insert, Update, or Delete data
        public int UpdateData(string query)
        {
            int rowsAffected = 0;
            try
            {
                using (MySqlCommand command = new MySqlCommand(query, _connection))
                {
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error updating data.");
                Console.WriteLine(ex.Message);
            }
            return rowsAffected;
        }

        // Close the connection
        public void CloseConnection()
        {
            try
            {
                if (_connection != null && _connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                    Console.WriteLine("Database connection closed.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error closing connection.");
                Console.WriteLine(ex.Message);
            }
        }

    }
}
