using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SalesAndInventory.Classes
{
    class Configuration
    {
        private static string server = "localhost";
        private static string port = "3306";
        private static string username = "root";
        private static string password = "";
        private static string database = "newerastore";

        private static string getConnectionString()
        {
            return string.Format("Server={0}; Port={1}; Username={2}; Password={3}; Database={4};",server,port,username,password,database);
        }

        public static MySqlConnection getConnection()
        {
            using(MySqlConnection connection = new MySqlConnection(getConnectionString())){
                try
                {
                    connection.Open();
                    connection.Close();
                    return connection;
                }catch(MySqlException mySqlException){
                    System.Windows.Forms.MessageBox.Show(mySqlException.Message);
                    return null;
                }

            }
        }
    }
}
