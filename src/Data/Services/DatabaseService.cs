using MySqlConnector;
using Core.Interfaces;

namespace Data.Services
{

    public class DatabaseService : IDatabaseService
    {

        private MySqlConnectionStringBuilder mySqlBuilder = new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            Port = 3306,
            UserID = "root",
            Password = "Delcroktam6",
            Database = "capstoneproject",
        };

        //protected: bruikbaar in parent en child class. Private alleen in huidige (parent) class.
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(mySqlBuilder.ConnectionString);

        }

    }

}
