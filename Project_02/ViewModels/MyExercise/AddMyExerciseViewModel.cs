using MvvmHelpers.Commands;
using Project_02.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Project_02.ViewModels.MyExercise
{
    [QueryProperty(nameof(Name), nameof(Name))]
    public class AddMyExerciseViewModel : ViewModelBase
    {
        string name, shortdes;
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string ShortDes { get => shortdes; set => SetProperty(ref shortdes, value); }

        public AsyncCommand SaveCommand { get; }
        IExerciseService exerciseService;
        public AddMyExerciseViewModel()
        {
            Title = "Add Exercise";
            SaveCommand = new AsyncCommand(Save);
            exerciseService = DependencyService.Get<IExerciseService>();
        }

        async Task Save()
        {
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(shortdes))
            {
                return;
            }

            await exerciseService.AddExercise(name, shortdes);

            await Shell.Current.GoToAsync("..");
        }
    }
}
