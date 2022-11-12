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
    public partial class ListaMascotasPerdidas : ContentPage
    {
        string ID;
        public ListaMascotasPerdidas()
        {
            InitializeComponent();
            BindingContext = new VMListaMascotasPerdidas(Navigation);
        }
    }
}