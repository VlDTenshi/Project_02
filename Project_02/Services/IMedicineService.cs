using Project_02.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_02.Services
{
    public interface IMedicineService
    {
        Task AddMedicine(string name, string type);
        Task<IEnumerable<Medicine>> GetMedicine();
        Task<Medicine> GetMedicine(int id);
        Task RemoveMedicine(int id);
    }
}
