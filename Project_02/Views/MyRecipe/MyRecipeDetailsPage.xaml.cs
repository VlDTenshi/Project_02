using Project_02.Models;
using Project_02.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project_02.Views.MyRecipe
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(RecipeId), nameof(RecipeId))]
    public partial class MyRecipeDetailsPage : ContentPage
	{
        public string RecipeId { get; set; }
        IRecipeService recipeService;
        public MyRecipeDetailsPage()
        {
            InitializeComponent();
            recipeService = DependencyService.Get<IRecipeService>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int.TryParse(RecipeId, out var result);

            BindingContext = await recipeService.GetRecipe(result);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}