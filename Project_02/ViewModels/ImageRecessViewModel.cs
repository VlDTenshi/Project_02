using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Command = Xamarin.Forms.Command;

namespace Project_02.ViewModels
{
    public class ImageRecessViewModel : ViewModelBase
    {
        public UriImageSource Image {  get; set; } = 
            new UriImageSource 
            {
                Uri = new Uri("https://ristmikud.tallinn.ee/last/cam103.jpg?=1640269695"),
                CachingEnabled = true,
                CacheValidity = TimeSpan.FromMinutes(1)
            };
        public Command RefreshCommand { get; }

        public ImageRecessViewModel()
        {
            RefreshCommand = new Command(() =>
            {
                Image = new UriImageSource
                {
                    Uri = new Uri("https://ristmikud.tallinn.ee/last/cam103.jpg?=1640269695"),
                    CachingEnabled = true,
                    CacheValidity = TimeSpan.FromMinutes(1)
                };
                OnPropertyChanged(nameof(Image));
            });
            RefreshLongCommand = new AsyncCommand(async () =>
            {
                IsBusy = true;
                await Task.Delay(5000);
                IsBusy = false;
            });
        }

        public AsyncCommand RefreshLongCommand { get; }
    }
}
