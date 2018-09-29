using GalaSoft.MvvmLight.Command;
using School.Common.Models;
using School.Helpers;
using School.Services;
using School.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace School.ViewModels
{
    public class UserViewModel : BaseViewModel
    {


        #region Attributes

        private ApiServices apiService;

        private bool isEnabled;
        #endregion



        #region Properties
        public string Prueba { get; set; }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }

        #endregion






        #region Singleton

        public static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        #endregion



        #region Constructor

        public UserViewModel()
        {

            this.Prueba = "por que no quieres cargar mi amor";
            this.IsEnabled = true;
           


        }
        #endregion

        #region Commands

        public ICommand TestButtonCommand
        {
            get
            {
                return new RelayCommand(prueba);
            }
        }

        private async void prueba()
        {
            await Application.Current.MainPage.DisplayAlert("como hacerlo", "debe hacerce", Languages.Accept);
        }


        public ICommand AddUserTestCommand
        {
            get
            {
                return new RelayCommand(AddUserTest);
            }
        }

        private async void AddUserTest()
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await App.Navigator.PushAsync(new RegisterPage());
        }


        #endregion


        #region methods







        #endregion
    }
}