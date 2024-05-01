using MvvmHelpers.Commands;
using Project_02.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project_02.ViewModels.MyMedicine
{
    [QueryProperty(nameof(Name), nameof(Name))]
    public class AddMyMedicineViewModel : ViewModelBase
    {
        string name, type;
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Type { get => type; set => SetProperty(ref type, value); }

        public AsyncCommand SaveCommand { get; }
        IMedicineService medicineService;
        public AddMyMedicineViewModel()
        {
            Title = "Add Medicine";
            SaveCommand = new AsyncCommand(Save);
            medicineService = DependencyService.Get<IMedicineService>();
        }

        async Task Save()
        {
            if(string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(type)) 
            {
                return;
            }

            await medicineService.AddMedicine(name, type);

            await Shell.Current.GoToAsync("..");
        }
    }
}
