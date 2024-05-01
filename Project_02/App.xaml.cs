using MonkeyCache.FileStore;
using Project_02.Assistants;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Project_02
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Barrel.ApplicationId = AppInfo.PackageName;

            MainPage = new AppShell();
            
        }

        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
            Themes.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            Themes.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Themes.SetTheme();
            });
        }
    }
}
