using SQLite;
using System.Threading.Tasks;
using TermTracker.Models;

namespace TermTracker.Data
{
    public static class SQLiteAsyncConnectionExtensions
    {
        public static async Task EnsureCreatedAsync(this SQLiteAsyncConnection connection)
        {
            // Create tables if they do not exist
            await connection.CreateTableAsync<Term>();
            await connection.CreateTableAsync<Course>();
            await connection.CreateTableAsync<Assessment>();
            await connection.CreateTableAsync<Note>();
        }
    }
}
