using MySqlConnector;
namespace Core.Interfaces
{
    public interface IDatabaseService
    {

        /// <summary>
        /// Returns a MySQL connection.
        /// </summary>
        /// <returns>An instance of  <see cref="MySqlConnection"/>.</returns>
        MySqlConnection GetConnection();
    }
}
