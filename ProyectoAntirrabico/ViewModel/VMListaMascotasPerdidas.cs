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
using Xamarin.Forms.Internals;
using System.Runtime.InteropServices.ComTypes;

namespace ProyectoAntirrabico.ViewModel
{
    public class VMListaMascotasPerdidas:BaseViewModel
    {

        #region VARIABLES
        int _index;
        string IdMP;

        ObservableCollection<MMascotasPerdidas> _ListaMP;
        //List<MMascotasPerdidas> _ListaMP;
        CollectionView _Lista;

        #endregion
        #region CONSTRUCTOR
        public VMListaMascotasPerdidas(INavigation navigation)
        {
            Navigation = navigation;
            MostrarMascotasPerdidas();
            ValidarConexionInternet();
        }
        #endregion
        #region OBJETOS
        public ObservableCollection<MMascotasPerdidas> ListaMP
        {
            get { return _ListaMP; }
            set
            {
                SetValue(ref _ListaMP, value);
                OnPropertyChanged();
            }
        }
        //public List<MMascotasPerdidas> ListaMP
        //{
        //    get { return _ListaMP; }
        //    set { SetValue(ref _ListaMP, value);
        //        OnPropertyChanged();
        //    }
        //}
        #endregion
        #region PROCESOS
        public async Task IrListaMascotasAdopcion()
        {
            await Navigation.PushAsync(new ListaMascotasAdopcion());
        }

        public async Task IrFormMascotasPerdidas()
        {
            await Navigation.PushAsync(new FormMascotasPerdidas());
        }

        public async Task Editar(MMascotasPerdidas perdidas)
        {
            await Navigation.PushAsync(new EditarMascotasPerdidas(perdidas));
        }

        public async Task MostrarMascotasPerdidas()
        {
            var funcion = new DMascotasPerdidas();

            ListaMP = await funcion.MostrarMP();
        }
        public async Task IrLogin()
        {
            await Navigation.PushAsync(new Login());
        }

        public async Task btnPerdido()
        {
            await DisplayAlert("Ok", "Ok", "Ok");
        }
        #endregion
        #region COMANDOS
        public ICommand IrListaMAcommand => new Command(async () => await IrListaMascotasAdopcion());
        public ICommand IrLogincommand => new Command(async () => await IrLogin());
        public ICommand IrFormMPcommand => new Command(async () => await IrFormMascotasPerdidas());
        public ICommand IrEditarCommand => new Command<MMascotasPerdidas>(async (p) => await Editar(p));
        public ICommand btnPerdidocommand => new Command(async () => await btnPerdido());
        #endregion

    }
}
