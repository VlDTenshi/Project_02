using MvvmHelpers;
using MvvmHelpers.Commands;
using Project_02.Models;
using Project_02.Views.MyMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace Project_02.ViewModels
{
    public class MedicineEquipmentViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Medicine> Medicine { get; set; }
        public ObservableRangeCollection<Grouping<string, Medicine>> MedicineGroups { get; set; }

        public AsyncCommand RefreshCommand { get; }

        public AsyncCommand<Medicine> FavoriteCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }

        public Command LoadMoreCommand { get; }
        public Command DelayLoadMoreCommand { get; }
        public Command ClearCommand { get; }

        public MedicineEquipmentViewModel()
        {
            Title = "Medicine Equipment";

            Medicine = new ObservableRangeCollection<Medicine>();
            MedicineGroups = new ObservableRangeCollection<Grouping<string, Medicine>>();

            LoadMore();

            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Medicine>(Favorite);
            SelectedCommand = new AsyncCommand<object>(Selected);
            LoadMoreCommand = new Command(LoadMore);
            ClearCommand = new Command(Clear);
            DelayLoadMoreCommand = new Command(DelayLoadMore);
        }

        async Task Favorite(Medicine medicine)
        {
            if (medicine == null)
                return;

            await Application.Current.MainPage.DisplayAlert("Favorite", medicine.Name, "OK");
        }

        Medicine previouslySelected;
        Medicine selectedMedicine;
        public Medicine SelectedMedicine
        {
            get => selectedMedicine;
            set => SetProperty(ref selectedMedicine, value);
        }

        async Task Selected(object args)
        {
            var medicine = args as Medicine;
            if(medicine == null) 
                return;

            SelectedMedicine = null;

            await AppShell.Current.GoToAsync(nameof(AddMyMedicinePage));
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Medicine.Clear();
            LoadMore();

            IsBusy = false;
        }

        void LoadMore()
        {
            if (Medicine.Count >= 20)
                return;

            var image = "OIP.png";
            Medicine.Add(new Medicine { Type = "Type1", Name = "Aspirin", Image = image });
            Medicine.Add(new Medicine { Type = "Type1", Name = "Aspirin_1", Image = image });
            Medicine.Add(new Medicine { Type = "Type2", Name = "Analgin", Image = image });
            Medicine.Add(new Medicine { Type = "Type2", Name = "Analgin_1", Image = image });

            MedicineGroups.Clear();

            MedicineGroups.Add(new Grouping<string, Medicine>("Type2", Medicine.Where(c => c.Type == "Type2")));
            MedicineGroups.Add(new Grouping<string, Medicine>("Type1", Medicine.Where(c => c.Type == "Type1")));
        }

        void DelayLoadMore()
        {
            if(Medicine.Count <= 10)
                return;

            LoadMore();
        }

        void Clear()
        {
            Medicine.Clear();
            MedicineGroups.Clear();
        }
    }
}
