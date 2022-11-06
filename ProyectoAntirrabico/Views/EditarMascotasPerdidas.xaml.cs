using ProyectoAntirrabico.Model;
using ProyectoAntirrabico.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAntirrabico.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditarMascotasPerdidas : ContentPage
    {
        public EditarMascotasPerdidas(MMascotasPerdidas perdidas)
        {
            InitializeComponent();
            BindingContext = new VMEditarMP(Navigation, perdidas);
        }
    }
}