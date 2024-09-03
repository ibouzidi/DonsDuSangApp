using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace DonsDuSangApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IEncryptionService _encryptionService;

        public LoginViewModel(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
            LoginCommand = new Command(OnLogin);
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }
        public string Title { get; internal set; }

        private async void OnLogin()
        {
            // Retrieve encrypted password from local storage
            string storedPassword = await SecureStorage.GetAsync("doctor_password");

            // Decrypt the stored password
            string decryptedPassword = _encryptionService.Decrypt(storedPassword);

            // Check if the entered password matches the stored one
            if (Password == decryptedPassword)
            {
                // Login success
                await Application.Current.MainPage.DisplayAlert("Success", "Login successful!", "OK");
            }
            else
            {
                // Login failed
                await Application.Current.MainPage.DisplayAlert("Error", "Incorrect password.", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
