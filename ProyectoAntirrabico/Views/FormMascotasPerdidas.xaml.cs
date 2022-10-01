using Plugin.Media.Abstractions;
using Plugin.Media;
using ProyectoAntirrabico.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Diagnostics;
using ProyectoAntirrabico.Data;

namespace ProyectoAntirrabico.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormMascotasPerdidas : ContentPage
    {
        public FormMascotasPerdidas()
        {
            InitializeComponent();
            BindingContext = new VMFormMascotasPerdidas(Navigation);
        }
    }
}