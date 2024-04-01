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
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();

            // Создаем приветственное сообщение
            var welcomeLabel = new Label
            {
                Text = "Добро пожаловать!",
                FontFamily = Device.RuntimePlatform == Device.iOS ? "HelveticaNeue-Light" : null,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) + 5,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Создаем мерцающую кнопку с текстом и стрелкой
            var arrowButton = new Button
            {
                Text = "Начать",
                BackgroundColor = Color.FromHex("#3CB9A7"), // Бирюзовый цвет кнопки
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            // Анимация мерцания кнопки
            var animation = new Animation(callback: d =>
            {
                double offset = Math.Sin(d * Math.PI) * 0.1;
                arrowButton.Scale = 1 + offset;
            }, start: 0, end: 1);

            // Добавляем периодическое выполнение анимации с меньшей частотой
            Device.StartTimer(TimeSpan.FromSeconds(0.8), () =>
            {
                arrowButton.Animate("animation", animation, length: 1000, easing: Easing.SinInOut);
                return true;
            });

            // Обработчик нажатия на кнопку
            arrowButton.Clicked += async (sender, args) =>
            {
                await Shell.Current.GoToAsync("//NavigatePage");
            };

            // Создаем макет для размещения элементов
            var layout = new StackLayout
            {
                Children = { welcomeLabel, arrowButton },
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            // Устанавливаем макет в качестве содержимого страницы
            Content = layout;
        }
    }
}