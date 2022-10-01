using Plugin.Media;
using Plugin.Media.Abstractions;
using ProyectoAntirrabico.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAntirrabico.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormMascotasAdopcion : ContentPage
    {
        public FormMascotasAdopcion()
        {
            InitializeComponent();
            BindingContext = new VMFormMascotasAdopcion(Navigation);
        }

        MediaFile file;

        
    }
}