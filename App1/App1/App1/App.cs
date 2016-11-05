
using System;
using Xamarin.Forms;

namespace App1
{
    public class App : Application
    {

        public static string ImageName { get; set; }
        public App()
        {
            MainPage = new Page1();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
