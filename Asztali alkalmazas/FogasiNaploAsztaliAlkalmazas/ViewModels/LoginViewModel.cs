﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FogasiNaploAsztaliAlkalmazas.Models;
using FogasiNaploAsztaliAlkalmazas.Repositories;
using FogasiNaploAsztaliAlkalmazas.Views;

namespace FogasiNaploAsztaliAlkalmazas.ViewModels
{
    public class LoginViewModel : ObservableObject
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); LoginCommand.NotifyCanExecuteChanged(); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); LoginCommand.NotifyCanExecuteChanged(); }
        }
        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }
        private LoginRepository repo;
        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            repo = new LoginRepository();
            LoginCommand = new RelayCommand(() => Login());
        }

        //private bool CanLogin()
        //{
        //    return !string.IsNullOrWhiteSpace(_username) && !string.IsNullOrWhiteSpace(_password);
        //}
        private void Login()
        {
            ErrorMessage = repo.Authenticate(_username, _password);
            if (ErrorMessage == Application.Current.Resources["loginSuccess"].ToString())
            {

                var mw = new FogasokView();

                Application.Current.Windows[0].Close();
                mw.Show();

            }
        }
    }
}
