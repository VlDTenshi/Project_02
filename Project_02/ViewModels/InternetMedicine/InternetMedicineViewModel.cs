using MvvmHelpers;
using MvvmHelpers.Commands;
using Project_02.Models;
using Project_02.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project_02.ViewModels.InternetMedicine
{
    public class InternetMedicineViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Medicine> Medicine {  get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Medicine> RemoveCommand { get; }

        public InternetMedicineViewModel() 
        {
            Title = "Internet Medicine";

            Medicine = new ObservableRangeCollection<Medicine>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Medicine>(Remove);
        }

        async Task Add()
        {
            var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name of medicine");
            var type = await App.Current.MainPage.DisplayPromptAsync("Type", "Type of medicine");
            await InternetMedicineService.AddMedicine(name, type);
            await Refresh();
        }

        async Task Remove(Medicine medicine)
        {
            await InternetMedicineService.RemoveMedicine(medicine.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            Medicine.Clear();
            var medicines = await InternetMedicineService.GetMedicine();
            Medicine.AddRange(medicines);
            IsBusy = false;
        }

    }
}
