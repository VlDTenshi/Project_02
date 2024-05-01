using Project_02.Views;
using System.Threading.Tasks;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Project_02.ViewModels
{
    public class NextPageViewModel: ViewModelBase
    {
        public ICommand GoTo { get; private set; }
        public ICommand GoToo { get; private set; }
        public ICommand GoTooo { get; private set; }
        public ICommand GoToooo { get; private set; }

        public NextPageViewModel()
        {
            GoTo = new Command(ExecuteGoTo);
            GoToo = new Command(ExecuteGoToo);
            GoTooo = new Command(ExecuteGoTooo);
            GoToooo = new Command(ExecuteGoToooo);
        }
        public static class Routes
        {
            public static string HospitalsPage => "//HospitalsPage";
            public static string MedicinesPage => "//MedicinesPage";
            public static string ExercisesPage => "//ExercisesPage";
        }

        private async void ExecuteGoTo()
        {
            await Shell.Current.GoToAsync(Routes.HospitalsPage);
        }

        private async void ExecuteGoToo()
        {
            await Shell.Current.GoToAsync(Routes.MedicinesPage);
        }

        private async void ExecuteGoTooo()
        {
            await Shell.Current.GoToAsync(Routes.ExercisesPage);
        }
        //private async void ExecuteGoTo()
        //{
        //    await Shell.Current.GoToAsync($"//{nameof(HospitalsPage)}");
        //}

        //private async void ExecuteGoToo()
        //{
        //    await Shell.Current.GoToAsync($"//{nameof(MedicinesPage)}");
        //}

        //private async void ExecuteGoTooo()
        //{
        //    await Shell.Current.GoToAsync($"//{nameof(ExercisesPage)}");
        //}

        private async void ExecuteGoToooo()
        {
            var result = await Application.Current.MainPage.DisplayActionSheet("Выберите способ вызова экстренной службы", "Отмена", null, "Позвонить на 112", "Выбрать из контактов");

            switch (result)
            {
                case "Позвонить на 112":
                    await CallEmergencyService();
                    break;
                case "Выбрать из контактов":
                    await ChooseContactFromAddressBook();
                    break;
                default:
                    // Если выбор отменен или что-то пошло не так
                    break;
            }
        }
        private async Task CallEmergencyService()
        {
            try
            {
                // Позвонить на номер экстренной службы 112
                PhoneDialer.Open("112");
            }
            catch (FeatureNotSupportedException ex)
            {
                // Функция недоступна на данном устройстве
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Позвонить на экстренную службу невозможно", "OK");
            }
            catch (Exception ex)
            {
                // Общая ошибка
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Что-то пошло не так", "OK");
            }
        }

        private async Task ChooseContactFromAddressBook()
        {
            try
            {
                // Открыть телефонную книгу для выбора контакта
                var contact = await Contacts.PickContactAsync();

                // Вы можете использовать выбранный контакт здесь
                if (contact != null)
                {
                    // Здесь может быть ваша логика обработки выбранного контакта
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибки, если не удалось выбрать контакт
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Выбрать контакт не удалось", "OK");
            }
        }
    }
}
