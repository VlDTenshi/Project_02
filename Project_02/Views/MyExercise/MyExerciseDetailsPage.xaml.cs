using Project_02.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project_02.Views.MyExercise
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ExerciseId), nameof(ExerciseId))]
    public partial class MyExerciseDetailsPage : ContentPage
    {
        public string ExerciseId { get; set; }
        IExerciseService exerciseService;
        public MyExerciseDetailsPage()
        {
            InitializeComponent();
            exerciseService = DependencyService.Get<IExerciseService>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(ExerciseId, out var result);

            BindingContext = await exerciseService.GetExercise(result);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}