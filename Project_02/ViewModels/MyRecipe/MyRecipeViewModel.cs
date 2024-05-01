using MvvmHelpers;
using MvvmHelpers.Commands;
using Project_02.Models;
using Project_02.Services;
using Project_02.Views.MyMedicine;
using Project_02.Views.MyRecipe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project_02.ViewModels.MyRecipe
{
    public class MyRecipeViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Recipe> Recipe { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Recipe> RemoveCommand { get; }
        public AsyncCommand<Recipe> SelectedCommand { get; }

        IRecipeService recipeService;

        public MyRecipeViewModel()
        {
            Title = "My Medicine";

            Recipe = new ObservableRangeCollection<Recipe>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Recipe>(Remove);
            SelectedCommand = new AsyncCommand<Recipe>(Selected);

            recipeService = DependencyService.Get<IRecipeService>();
        }

        async Task Add()
        {
            var route = $"{nameof(AddMyRecipePage)}?Name=Aspirin";
            await Shell.Current.GoToAsync(route);
        }

        async Task Selected(Recipe recipe)
        {
            if (recipe == null)
                return;

            var route = $"{nameof(MyRecipeDetailsPage)}?RecipeId={recipe.Id}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Remove(Recipe recipe)
        {
            await recipeService.RemoveRecipe(recipe.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;
#if DEBUG
            await Task.Delay(500);
#endif
            Recipe.Clear();
            var recipes = await recipeService.GetRecipe();
            Recipe.AddRange(recipes);
            IsBusy = false;
            DependencyService.Get<IMessage>()?.MakeMessage("Refreshed");
        }
    }
}
