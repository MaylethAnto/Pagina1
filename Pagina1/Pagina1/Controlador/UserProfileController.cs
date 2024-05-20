using Pagina1.Modelo;
using SQLite;
using System.Threading.Tasks;

namespace Pagina1.Controlador
{
    public class UserProfileController
    {
        private readonly SQLiteAsyncConnection _database;

        public UserProfileController(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserProfile>().Wait();
        }

        public Task<UserProfile> AuthenticateUserAsync(string email, string password)
        {
            return _database.Table<UserProfile>()
                            .Where(u => u.CorreoElectronico == email && u.Contrasena == password)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveUserProfileAsync(UserProfile user)
        {
            return _database.InsertAsync(user);
        }
    }
}
