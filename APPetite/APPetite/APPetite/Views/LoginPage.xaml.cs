﻿using APPetite.ViewModels;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPetite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OpenForgetPassPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"/{nameof(ForgetPassEmail)}");
        }

        private async void GoBackPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void LoginApp(object sender, EventArgs e)
        {
            string username = username_login.Text,
                password = password_login.Text;


            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await App.Current.MainPage.DisplayAlert("Empty Username or Password", "Please enter Username and Password", "OK");
            }
            else
            {
                var user = await FirebaseAccountHelper.GetUserByUsername(username);

                if (user != null)
                {
                    if (username == user.Username && (password == user.Password || password == user.BackupPass))
                    {
                        password_login.Text = "";
                        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CurrentAccount.txt");
                        File.WriteAllText(fileName, username);
                        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Incorrect Password", "The Password you entered is incorrect. Please try again.", "OK");
                    }
                }
                else
                {
                    user = await FirebaseAccountHelper.GetUserByEmail(username);

                    if (user != null)
                    {
                        if (username == user.Email && (password == user.Password || password == user.BackupPass))
                        {
                            password_login.Text = "";
                            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CurrentAccount.txt");
                            File.WriteAllText(fileName, username);
                            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Incorrect Password", "The Password you entered is incorrect. Please try again.", "OK");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Login Failed", "Your Email or Password is incorrect.\nPlease try again", "OK");
                    }
                }
            }
            
        }
    }
}