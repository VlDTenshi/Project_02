using Project_02.Services;
using Project_02.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Project_02
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new StartPage());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
