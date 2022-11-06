using ProyectoAntirrabico.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoAntirrabico.ViewModel
{
    public class VMEditarMA:BaseViewModel
    {

        #region VARIABLES
        string _Texto;
        public MMascotasAdopocion RecibeParametros { get; set; }
        #endregion
        #region CONSTRUCTOR
        public VMEditarMA(INavigation navigation, MMascotasAdopocion TraeParametros)
        {
            Navigation = navigation;
            RecibeParametros = TraeParametros;
        }
        #endregion
        #region OBJETOS
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
        #endregion
        #region PROCESOS
        public async Task ProcesoAsyncrono()
        {

        }
        public void ProcesoSimple()
        {

        }
        #endregion
        #region COMANDOS
        public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}
