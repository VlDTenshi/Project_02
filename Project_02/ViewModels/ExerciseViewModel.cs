using MvvmHelpers;
using MvvmHelpers.Commands;
using Project_02.Models;
using Project_02.Views.MyExercise;
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
    public class ExerciseViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Exercise> Exercise { get; set; }
        public ObservableRangeCollection<Grouping<string, Exercise>> ExerciseGroups { get; set; }

        public AsyncCommand RefreshCommand { get; }

        public AsyncCommand<Exercise> FavoriteCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }

        public Command LoadMoreCommand { get; }
        public Command DelayLoadMoreCommand { get; }
        public Command ClearCommand { get; }

        public ExerciseViewModel()
        {
            Title = "Medicine Equipment";

            Exercise = new ObservableRangeCollection<Exercise>();
            ExerciseGroups = new ObservableRangeCollection<Grouping<string, Exercise>>();

            LoadMore();

            RefreshCommand = new AsyncCommand(Refresh);
            FavoriteCommand = new AsyncCommand<Exercise>(Favorite);
            SelectedCommand = new AsyncCommand<object>(Selected);
            LoadMoreCommand = new Command(LoadMore);
            ClearCommand = new Command(Clear);
            DelayLoadMoreCommand = new Command(DelayLoadMore);
        }

        async Task Favorite(Exercise exercise)
        {
            if (exercise == null)
                return;

            await Application.Current.MainPage.DisplayAlert("Favorite", exercise.Name, "OK");
        }

        Exercise previouslySelected;
        Exercise selectedExercise;
        public Exercise SelectedExercise
        {
            get => selectedExercise;
            set => SetProperty(ref selectedExercise, value);
        }

        async Task Selected(object args)
        {
            var exercise = args as Exercise;
            if (exercise == null)
                return;

            SelectedExercise = null;

            await AppShell.Current.GoToAsync(nameof(AddMyExercisePage));
        }

        async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);

            Exercise.Clear();
            LoadMore();

            IsBusy = false;
        }

        void LoadMore()
        {
            if (Exercise.Count >= 20)
                return;

            var image = "OIP.png";
            Exercise.Add(new Exercise { ShortDes = "For head", Name = "Aspirin", Image = image });
            Exercise.Add(new Exercise { ShortDes = "For arms", Name = "Aspirin_1", Image = image });
            Exercise.Add(new Exercise { ShortDes = "For arms", Name = "Analgin", Image = image });
            Exercise.Add(new Exercise { ShortDes = "For head", Name = "Analgin_1", Image = image });

            ExerciseGroups.Clear();

            ExerciseGroups.Add(new Grouping<string, Exercise>("Type2", Exercise.Where(c => c.ShortDes == "Type2")));
            ExerciseGroups.Add(new Grouping<string, Exercise>("Type1", Exercise.Where(c => c.ShortDes == "Type1")));
        }

        void DelayLoadMore()
        {
            if (Exercise.Count <= 10)
                return;

            LoadMore();
        }

        void Clear()
        {
            Exercise.Clear();
            ExerciseGroups.Clear();
        }
    }
}
