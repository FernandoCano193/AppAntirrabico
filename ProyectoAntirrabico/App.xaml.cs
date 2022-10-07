using ProyectoAntirrabico.Views;
using ProyectoAntirrabico.Views.Listas;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAntirrabico
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage (new FormRegistroAdmin());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
