using Project_02.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Project_02.Services
{
    public class RecipeService : IRecipeService
    {
        SQLiteAsyncConnection _connection2;
        async Task Init()
        {
            if (_connection2 != null)

                return;

            //Get an absolute ppath to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "RecipeData.db");

            _connection2 = new SQLiteAsyncConnection(databasePath);

            await _connection2.CreateTableAsync<Recipe>();
        }

        public async Task AddRecipe(string name, string shortdes)
        {
            await Init();
            var image = "OIP.png";
            var recipe = new Recipe
            {
                Name = name,
                ShortDes = shortdes,
                Image = image
            };

            var id = await _connection2.InsertAsync(recipe);
        }

        public async Task RemoveRecipe(int id)
        {
            await Init();

            await _connection2.DeleteAsync<Recipe>(id);
        }

        public async Task<IEnumerable<Recipe>> GetRecipe()
        {
            await Init();

            var recipe = await _connection2.Table<Recipe>().ToListAsync();
            return recipe;
        }

        public async Task<Recipe> GetRecipe(int id)
        {
            await Init();

            var recipe = await _connection2.Table<Recipe>()
                .FirstOrDefaultAsync(c => c.Id == id);

            return recipe;
        }
    }
}
