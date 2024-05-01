using MvvmHelpers;
using MvvmHelpers.Commands;
using Project_02.Models;
using Project_02.Views.MyMedicine;
using Project_02.Views.MyRecipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace Project_02.ViewModels
{
    public class RecipeViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Recipe> Recipe { get; set; }
        public ObservableRangeCollection<Grouping<string, Recipe>> RecipeGroups { get; set; }

        public AsyncCommand RefreshCommand { get; }

        public AsyncCommand<Recipe> FavoriteCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }

        public Command LoadMoreCommand { get; }
        public Command DelayLoadMoreCommand { get; }
        public Command ClearCommand { get; }

        public RecipeViewModel()
        {
            Title = "Recipe Equipment";

            Recipe = new ObservableRangeCollection<Recipe>();
            RecipeGroups = new ObservableRangeCollection<Grouping<string, Recipe>>();

            LoadMore();

            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Recipe>(Favorite);
            SelectedCommand = new AsyncCommand<object>(Selected);
            LoadMoreCommand = new Command(LoadMore);
            ClearCommand = new Command(Clear);
            DelayLoadMoreCommand = new Command(DelayLoadMore);
        }

        async Task Favorite(Recipe recipe)
        {
            if (recipe == null)
                return;

            await Application.Current.MainPage.DisplayAlert("Favorite", recipe.Name, "OK");
        }

        Recipe previouslySelected;
        Recipe selectedRecipe;
        public Recipe SelectedRecipe
        {
            get => selectedRecipe;
            set => SetProperty(ref selectedRecipe, value);
        }

        async Task Selected(object args)
        {
            var recipe = args as Recipe;
            if (recipe == null)
                return;

            SelectedRecipe = null;

            await AppShell.Current.GoToAsync(nameof(AddMyRecipePage));
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Recipe.Clear();
            LoadMore();

            IsBusy = false;
        }

        void LoadMore()
        {
            if (Recipe.Count >= 20)
                return;

            var image = "OIP.png";
            Recipe.Add(new Recipe { ShortDes = "Type1", Name = "Aspirin", Image = image });
            Recipe.Add(new Recipe { ShortDes = "Type1", Name = "Aspirin_1", Image = image });
            Recipe.Add(new Recipe { ShortDes = "Type2", Name = "Analgin", Image = image });
            Recipe.Add(new Recipe { ShortDes = "Type2", Name = "Analgin_1", Image = image });

            RecipeGroups.Clear();

            RecipeGroups.Add(new Grouping<string, Recipe>("Type2", Recipe.Where(c => c.ShortDes == "Type2")));
            RecipeGroups.Add(new Grouping<string, Recipe>("Type1", Recipe.Where(c => c.ShortDes == "Type1")));
        }

        void DelayLoadMore()
        {
            if (Recipe.Count <= 10)
                return;

            LoadMore();
        }

        void Clear()
        {
            Recipe.Clear();
            RecipeGroups.Clear();
        }
    }
}
