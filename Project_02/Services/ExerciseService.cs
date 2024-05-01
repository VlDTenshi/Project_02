using Project_02.Models;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Project_02.Services
{
    public class ExerciseService : IExerciseService
    {
        SQLiteAsyncConnection _connection1;
        async Task Init()
        {
            if (_connection1 != null)

                return;

            //Get an absolute ppath to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "ExerciseData.db");

            _connection1 = new SQLiteAsyncConnection(databasePath);

            await _connection1.CreateTableAsync<Exercise>();
        }

        public async Task AddExercise(string name, string shortdes)
        {
            await Init();
            var image = "OIP.png";
            var exercise = new Exercise
            {
                Name = name,
                ShortDes = shortdes,
                Image = image
            };

            var id = await _connection1.InsertAsync(exercise);
        }

        public async Task RemoveExercise(int id)
        {
            await Init();

            await _connection1.DeleteAsync<Exercise>(id);
        }

        public async Task<IEnumerable<Exercise>> GetExercise()
        {
            await Init();

            var exercise = await _connection1.Table<Exercise>().ToListAsync();
            return exercise;
        }

        public async Task<Exercise> GetExercise(int id)
        {
            await Init();

            var exercise = await _connection1.Table<Exercise>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return exercise;
        }
    }
}
