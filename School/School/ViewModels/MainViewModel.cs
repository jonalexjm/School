using School.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.ViewModels
{
    public class MainViewModel
    {

        #region attributes

        #endregion

        #region Properties

        public RegisterViewModel Register { get; set; }

        public UserViewModel Users { get; set; }

        public LoginViewModel Login { get; set; }

        #endregion


        public MyUserASP UserASP { get; set; } // para poder acceder al usuario todo el tiempo



        public string UserFullName
        {
            get
            {
                if (UserASP != null && UserASP.Claims != null && UserASP.Claims.Count > 1)
                {
                    return $"{UserASP.Claims[0].ClaimValue} {UserASP.Claims[1].ClaimValue}";
                }

                return null;
            }
        }




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

        #region Singleton

        public MainViewModel()
        {

            instance = this;
            

        }


        #endregion

        #region Methods

        #endregion





    }
}
