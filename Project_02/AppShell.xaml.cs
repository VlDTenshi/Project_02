using Project_02.Views;
using Project_02.Views.MyExercise;
using Project_02.Views.MyMedicine;
using Project_02.Views.MyRecipe;
using Xamarin.Forms;

namespace Project_02
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AddMyMedicinePage),
                typeof(AddMyMedicinePage));
            Routing.RegisterRoute(nameof(MyMedicineDetailsPage),
                typeof (MyMedicineDetailsPage));
            Routing.RegisterRoute(nameof(RegistrationPage),
                typeof (RegistrationPage));
            Routing.RegisterRoute(nameof(AddMyExercisePage), 
                typeof (AddMyExercisePage));
            Routing.RegisterRoute(nameof(MyExerciseDetailsPage),
                typeof (MyExerciseDetailsPage));
            Routing.RegisterRoute(nameof(AddMyRecipePage),
                typeof (AddMyRecipePage));
            Routing.RegisterRoute(nameof(MyRecipeDetailsPage),
                typeof (MyRecipeDetailsPage));

        } 
    }
}
