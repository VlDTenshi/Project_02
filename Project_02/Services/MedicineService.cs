using Project_02.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Project_02.Services
{
    public class MedicineService : IMedicineService
    {
        SQLiteAsyncConnection _connection;
        async Task Init()
        {
            if(_connection != null)
            
                return;

            //Get an absolute ppath to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Data.db");

            _connection = new SQLiteAsyncConnection(databasePath);

            await _connection.CreateTableAsync<Medicine>();
        }

        public async Task AddMedicine(string name, string type)
        {
            await Init();
            var image = "OIP.png";
            var medicine = new Medicine
            {
                Name = name,
                Type = type,
                Image = image
            };

            var id = await _connection.InsertAsync(medicine);
        }

        public async Task RemoveMedicine(int id)
        {
            await Init();

            await _connection.DeleteAsync<Medicine>(id);
        }

        public async Task<IEnumerable<Medicine>> GetMedicine()
        {
            await Init();

            var medicine = await _connection.Table<Medicine>().ToListAsync();
            return medicine;
        }

        public async Task<Medicine> GetMedicine(int id)
        {
            await Init();

            var medicine = await _connection.Table<Medicine>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return medicine;
        }
    }
}
