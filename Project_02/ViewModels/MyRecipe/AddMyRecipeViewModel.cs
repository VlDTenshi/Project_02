using MvvmHelpers.Commands;
using Project_02.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project_02.ViewModels.MyRecipe
{
    [QueryProperty(nameof(Name), nameof(Name))]
    public class AddMyRecipeViewModel : ViewModelBase
    {
        string name, type;
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Type { get => type; set => SetProperty(ref type, value); }

        public AsyncCommand SaveCommand { get; }
        IRecipeService recipeService;
        public AddMyRecipeViewModel()
        {
            Title = "Add Recipe";
            SaveCommand = new AsyncCommand(Save);
            recipeService = DependencyService.Get<IRecipeService>();
        }

        async Task Save()
        {
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(type))
            {
                return;
            }

            await recipeService.AddRecipe(name, type);

            await Shell.Current.GoToAsync("..");
        }
    }
}
