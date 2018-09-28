using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using School.Common.Models;
using School.Helpers;
using School.Services;
using School.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace School.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {


        #region Attributes

        private ApiServices apiService;

        private bool isRunning;

        private bool isEnabled;

        #endregion

        #region Properties

        public bool IsRemembered { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }

        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }

        #endregion

        #region Constructor


        public LoginViewModel()
        {


            this.apiService = new ApiServices();

            this.IsEnabled = true;
        }

        #endregion

        #region Methods



        #endregion

        #region Commands

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }

        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.EmailValidation,
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PasswordValidation,
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;
/**


            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;

            }

     **/      

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var token = await this.apiService.GetToken(url, this.Email, this.Password);

            if (token == null || string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.SomethingWrong, Languages.Accept);
                return;

            }

           
            this.IsRunning = false;
            this.IsEnabled = true;

            Settings.TokenType = token.TokenType;
            Settings.AccessToken = token.AccessToken;
            Settings.IsRemembered = this.IsRemembered;

            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlUsersController"].ToString();
            var response = await this.apiService.GetUser(url, prefix, $"{controller}/GetUser", this.Email, token.TokenType, token.AccessToken);
            if (response.IsSuccess)
            {
                var userASP = (MyUserASP)response.Result;
               MainViewModel.GetInstance().UserASP = userASP;
               Settings.UserASP = JsonConvert.SerializeObject(userASP);
             
            }

            MainViewModel.GetInstance().Register = new RegisterViewModel();
            Application.Current.MainPage = new RegisterPage();
    

        }

        #endregion

    }
}
