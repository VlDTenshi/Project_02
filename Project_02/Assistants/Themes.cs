using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Project_02.Assistants
{
    public static class Themes
    {
        public static void SetTheme()
        {
            switch(Settings.Theme)
            {
                //default
                case 0:
                    App.Current.UserAppTheme = OSAppTheme.Unspecified;
                    break;
                //light
                case 1:
                    App.Current.UserAppTheme = OSAppTheme.Light;
                    break;
                //dark
                case 2:
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
            }

            var nav = App.Current.MainPage as NavigationPage;

            var c = DependencyService.Get<IEnvir>();
            if(App.Current.RequestedTheme == OSAppTheme.Dark)
            {
                c?.SetStatusBarColor(Color.Black, false);
                if(nav != null)
                {
                    nav.BarBackgroundColor = Color.Black;
                    nav.BarTextColor = Color.White;
                }
            }
            else
            {
                c?.SetStatusBarColor(Color.White, true);
                if (nav != null)
                {
                    nav.BarBackgroundColor = Color.White;
                    nav.BarTextColor = Color.Black;
                }
            }
        }
    }
}
