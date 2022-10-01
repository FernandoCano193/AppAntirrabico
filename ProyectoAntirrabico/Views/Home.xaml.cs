using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace ProyectoAntirrabico.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : Xamarin.Forms.TabbedPage
    {
        public Home()
        {
            InitializeComponent();

            //ESPECIFICACION DONDE SE INDICA QUE LA BARRA DE NAVEGACION SE ENCONTRARA EN LA PARTE BAJA
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}