using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ProyectoAntirrabico.Views;
using ProyectoAntirrabico.Model;
using ProyectoAntirrabico.Data;
using System.Collections.ObjectModel;

namespace ProyectoAntirrabico.ViewModel
{
    public class VMListaMascotasAdopcion : BaseViewModel
    {

        #region VARIABLES
        string _Texto;

        //SE CREA UNA LISTA PARA PODER MOSTRAR LOS DATOS
        ObservableCollection<MMascotasAdopocion> _ListaMA;

        #endregion
        #region CONSTRUCTOR
        public VMListaMascotasAdopcion(INavigation navigation)
        {
            Navigation = navigation;
            MostrarMascotasAdopcion();
            ValidarConexionInternet();
        }
        #endregion
        #region OBJETOS
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
        public ObservableCollection<MMascotasAdopocion> ListaMA
        {
            get { return _ListaMA; }
            set { SetValue(ref _ListaMA, value);
                OnPropertyChanged();
            }
        }
        #endregion
        #region PROCESOS
        public async Task IrLogin()
        {
            await Navigation.PushAsync(new Login());
        }

        public async Task IrFormMascotasAdopcion()
        {
            await Navigation.PushAsync(new FormMascotasAdopcion());
        }

        public async Task MostrarMascotasAdopcion()
        {
            var funcion = new DMascotasAdopcion();
            ListaMA = await funcion.MostrarMA();
        }
        #endregion

        #region COMANDOS
        public ICommand IrLogincommand => new Command(async () => await IrLogin());
        public ICommand IrFormMAcommand => new Command(async () => await IrFormMascotasAdopcion());
        //public ICommand ProcesoSimpcommand => new Command(DibujarMascotaAdopcion);
        #endregion
    }
}
