using Pagina1.Modelo;
using SQLite;
using System.Threading.Tasks;

namespace Pagina1.Controlador
{
    public class PetProfileController
    {
        public SQLiteAsyncConnection Connection { get; set; }

        public PetProfileController(string path)
        {
            Connection = new SQLiteAsyncConnection(path);
            Connection.CreateTableAsync<PetProfile>().Wait();
        }

        public Task<PetProfile> GetPetProfileAsync()
        {
            return Connection.Table<PetProfile>().FirstOrDefaultAsync();
        }

        public Task SavePetProfileAsync(PetProfile petProfile)
        {
            if (petProfile.Id == 0)
            {
                return Connection.InsertAsync(petProfile);
            }
            else
            {
                return Connection.UpdateAsync(petProfile);
            }
        }

        public Task DeletePetProfileAsync(PetProfile petProfile)
        {
            return Connection.DeleteAsync(petProfile);
        }
    }
}