using MvvmHelpers;
using MvvmHelpers.Commands;
using Project_02.Models;
using Project_02.Services;
using Project_02.Views.MyExercise;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project_02.ViewModels.MyExercise
{
    public class MyExerciseViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Exercise> Exercise { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddCommand { get; }
        public AsyncCommand<Exercise> RemoveCommand { get; }
        public AsyncCommand<Exercise> SelectedCommand { get; }

        IExerciseService exerciseService;

        public MyExerciseViewModel()
        {
            Title = "My Exercise";

            Exercise = new ObservableRangeCollection<Exercise>();

            RefreshCommand = new AsyncCommand(Refresh);
            AddCommand = new AsyncCommand(Add);
            RemoveCommand = new AsyncCommand<Exercise>(Remove);
            SelectedCommand = new AsyncCommand<Exercise>(Selected);

            exerciseService = DependencyService.Get<IExerciseService>();
        }

        async Task Add()
        {
            var route = $"{nameof(AddMyExercisePage)}?Name=Plex";
            await Shell.Current.GoToAsync(route);
        }

        async Task Selected(Exercise exercise)
        {
            if (exercise == null)
                return;

            var route = $"{nameof(MyExerciseDetailsPage)}?ExerciseId={exercise.Id}";
            await Shell.Current.GoToAsync(route);
        }

        async Task Remove(Exercise exercise)
        {
            await exerciseService.RemoveExercise(exercise.Id);
            await Refresh();
        }

        async Task Refresh()
        {
            IsBusy = true;
#if DEBUG
            await Task.Delay(500);
#endif
            Exercise.Clear();
            var exercises = await exerciseService.GetExercise();
            Exercise.AddRange(exercises);
            IsBusy = false;
            DependencyService.Get<IMessage>()?.MakeMessage("Refreshed");
        }
    }
}
