using Project_02.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project_02
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigatePage : ContentPage
    {
        public NavigatePage()
        {
            InitializeComponent();

            var mainLayout = new StackLayout
            {
                Spacing = 10
            };

            var menuButton1 = CreateMenuButton("Contacts", OnMenuButton1Clicked);
            mainLayout.Children.Add(menuButton1);

            var menuButton2 = CreateMenuButton("Exercises", OnMenuButton2Clicked);
            mainLayout.Children.Add(menuButton2);

            var menuButton3 = CreateMenuButton("Medicines", OnMenuButton3Clicked);
            mainLayout.Children.Add(menuButton3);

            //var menuButton4 = CreateMenuButton("Название страницы 4", OnMenuButton4Clicked);
            //mainLayout.Children.Add(menuButton4);

            Content = mainLayout;
        }
        private Button CreateMenuButton(string text, EventHandler handler)
        {
            var button = new Button
            {
                Text = text,
                BackgroundColor = Color.Beige,
                CornerRadius = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(20, 0) // Отступы между кнопками
            };
            button.Clicked += handler;
            return button;
        }
        private async void OnMenuButton1Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
        }

        private async void OnMenuButton2Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(ExercisesPage)}");
        }

        private async void OnMenuButton3Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(ItemsPage)}");
        }

        //private async void OnMenuButton4Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Page4());
        //}
    }
}