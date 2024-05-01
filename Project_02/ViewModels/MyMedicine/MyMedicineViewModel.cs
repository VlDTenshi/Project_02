using MvvmHelpers;
using MvvmHelpers.Commands;
using Project_02.Models;
using Project_02.Services;
using Project_02.Views.MyMedicine;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project_02.ViewModels.MyMedicine
{
    public class MyMedicineViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Medicine> Medicine {  get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Medicine> RemoveCommand { get; }
        public AsyncCommand<Medicine> SelectedCommand { get; }

        IMedicineService medicineService;

        public MyMedicineViewModel()
        {
            Title = "My Medicine";

            Medicine = new ObservableRangeCollection<Medicine>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Medicine>(Remove);
            SelectedCommand = new AsyncCommand<Medicine>(Selected);

            medicineService = DependencyService.Get<IMedicineService>();
        }

        async Task Add()
        {
            var route = $"{nameof(AddMyMedicinePage)}?Name=Aspirin";
            await Shell.Current.GoToAsync(route);
        }

        async Task Selected(Medicine medicine)
        {
            if(medicine == null)
                return;
            
            var route = $"{nameof(MyMedicineDetailsPage)}?MedicineId={medicine.Id}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Remove(Medicine medicine)
        {
            await medicineService.RemoveMedicine(medicine.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;
#if DEBUG
            await Task.Delay(500);
#endif
            Medicine.Clear();
            var medicines = await medicineService.GetMedicine();
            Medicine.AddRange(medicines);
            IsBusy = false;
            DependencyService.Get<IMessage>()?.MakeMessage("Refreshed");
        }
    }
}
