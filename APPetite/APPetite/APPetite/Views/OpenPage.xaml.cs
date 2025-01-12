﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APPetite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenPage : ContentPage
    {
        public OpenPage()
        {
            InitializeComponent();
        }

        private async void OpenLoginPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(OpenPage)}/{nameof(LoginPage)}");
        }

        private async void OpenRegisterPage(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(OpenPage)}/{nameof(RegisterPage)}");
        }
    }
}