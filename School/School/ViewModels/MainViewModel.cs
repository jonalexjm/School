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
