using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace ProyectoAntirrabico.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeAdmin : Xamarin.Forms.TabbedPage
    {
        public HomeAdmin()
        {
            InitializeComponent();

            //ESPECIFICACION DONDE SE INDICA QUE LA BARRA DE NAVEGACION SE ENCONTRARA EN LA PARTE BAJA
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

        }
    }
}