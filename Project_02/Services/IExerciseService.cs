using Project_02.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project_02.Services
{
    public interface IExerciseService
    {
        Task AddExercise(string name, string shortdes);
        Task<IEnumerable<Exercise>> GetExercise();
        Task<Exercise> GetExercise(int id);
        Task RemoveExercise(int id);
    }
}
