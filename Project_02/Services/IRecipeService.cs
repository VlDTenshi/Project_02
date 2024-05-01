using Project_02.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project_02.Services
{
    public interface IRecipeService
    {
        Task AddRecipe(string name, string type);
        Task<IEnumerable<Recipe>> GetRecipe();
        Task<Recipe> GetRecipe(int id);
        Task RemoveRecipe(int id);
    }
}
