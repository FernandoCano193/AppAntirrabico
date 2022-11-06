using ProyectoAntirrabico.Data;
using ProyectoAntirrabico.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoAntirrabico.ViewModel
{
    public class VMEditarMP :BaseViewModel
    {
        #region VARIABLES

        public MMascotasPerdidas RecibeMascotas { get; set; }
        #endregion
        #region CONSTRUCTOR
        public VMEditarMP(INavigation navigation, MMascotasPerdidas TraeMascotas)
        {
            Navigation = navigation;
            RecibeMascotas = TraeMascotas;
        }
        #endregion
        #region OBJETOS
        
        #endregion
        #region PROCESOS
        public async Task Editar()
        {
        }

        public async Task Cancelar()
        {
            await Navigation.PopAsync();
        }
        public void ProcesoSimple()
        {

        }
        #endregion
        #region COMANDOS
        public ICommand editarcommand => new Command(async () => await Editar());
        public ICommand Cancelarcommand => new Command(async () => await Cancelar());
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}
