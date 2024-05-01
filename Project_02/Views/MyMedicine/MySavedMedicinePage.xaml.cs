using Project_02.ViewModels.MyMedicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project_02.Views.MyMedicine
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MySavedMedicinePage : ContentPage
    {
        public MySavedMedicinePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var vm = (MyMedicineViewModel)BindingContext;
            if (vm.Medicine.Count == 0)
                await vm.RefreshCommand.ExecuteAsync();
        }
    }
}