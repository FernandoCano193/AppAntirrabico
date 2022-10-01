using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoAntirrabico.Droid
{
    [Activity(Label = "Zoolo+cotas", Icon = "@mipmap/ic_launcher", Theme = "@style/SplashTheme", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize)]

    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //LINEA NECESARIA PARA SEGUIR MANTENIENDO TODOS LOS PERMISOS DE LA ACTIVIDAD INCIAL
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));


        }
    }
}