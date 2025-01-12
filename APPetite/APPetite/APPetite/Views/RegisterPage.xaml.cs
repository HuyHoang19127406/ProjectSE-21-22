﻿using APPetite.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPetite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void Sign_up_clicked(object sender, EventArgs e)
        {
            string username = username_register.Text,
                email = email_register.Text,
                password = password_register.Text,
                confirmPass = password_again.Text;

            if (!String.Equals(password, confirmPass))
            {
                await App.Current.MainPage.DisplayAlert("Password mismatch", "Please make sure the password confirmation match", "OK");
            }
            else if (password.Length < 8)          // and something more, like special char, etc.
            {
                await App.Current.MainPage.DisplayAlert("Invalid Password", "", "OK");
            }
            else if (!Email.IsValidEmail(email_register.Text))
            {
                await App.Current.MainPage.DisplayAlert("Invalid Email Address", "Please enter correct Email Address.", "OK");
            }
            else
            {
                var userCheck = await FirebaseAccountHelper.GetUserByUsername(username);
                if (userCheck != null)
                {
                    await App.Current.MainPage.DisplayAlert("Username already in use", "Username already exists. Please try with another one.", "OK");
                }
                else
                {
                    userCheck = await FirebaseAccountHelper.GetUserByEmail(email);
                    if (userCheck != null)
                    {
                        await App.Current.MainPage.DisplayAlert("Email already in use", "An account with Email " + email + " already exists.", "OK");
                    }
                    else
                    {
                        var user = await FirebaseAccountHelper.AddUser(username, email, password);
                        if (user)
                        {
                            await App.Current.MainPage.DisplayAlert("Sign Up Success", "", "OK");
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Error", "SignUp Fail", "OK");
                        }
                    }
                }
            }
        }

        private void checkToEnable()
        {
            signUpButton.IsEnabled = false;
            if (
                !string.IsNullOrEmpty(username_register.Text) && !string.IsNullOrEmpty(email_register.Text) &&
                !string.IsNullOrEmpty(password_register.Text) && !string.IsNullOrEmpty(password_again.Text)
                )
            {
                signUpButton.IsEnabled = true;
            }
        }

        void entryTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            checkToEnable();
        }

        private async void GoBackPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}