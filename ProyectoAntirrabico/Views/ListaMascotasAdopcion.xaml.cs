using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoAntirrabico.ViewModel;

namespace ProyectoAntirrabico.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaMascotasAdopcion : ContentPage
    {

        public ListaMascotasAdopcion()
        {
            InitializeComponent();
            BindingContext = new VMListaMascotasAdopcion(Navigation);
        }

    }
}