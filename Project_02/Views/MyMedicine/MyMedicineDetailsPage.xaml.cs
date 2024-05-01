using Project_02.Services;
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
	[QueryProperty(nameof(MedicineId), nameof(MedicineId))]
	public partial class MyMedicineDetailsPage : ContentPage
	{
		public string MedicineId { get; set; }
		IMedicineService medicineService;
		public MyMedicineDetailsPage ()
		{
			InitializeComponent ();
			medicineService = DependencyService.Get<IMedicineService> ();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			int.TryParse(MedicineId, out var result);

			BindingContext = await medicineService.GetMedicine(result);
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
			await Shell.Current.GoToAsync("..");
        }
    }
}