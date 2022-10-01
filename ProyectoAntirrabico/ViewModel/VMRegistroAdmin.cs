using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
//LIBRERIAS NECESARIAS
using Firebase.Auth;
using Plugin.Media.Abstractions;
using ProyectoAntirrabico.Views;
using ProyectoAntirrabico.Model;
using ProyectoAntirrabico.Data;

namespace ProyectoAntirrabico.ViewModel
{
    public class VMRegistroAdmin : BaseViewModel
    {
        #region VARIABLES
        string _Nombres;
        string _Apellidos;
        string _Area;
        string _Correo;
        string _Contraseña;
        string _LinkFoto;

        ImageSource _foto;

        MediaFile file;
        string RutaFoto;
        string IdAdministrador;

        #endregion
        #region CONSTRUCTOR
        public VMRegistroAdmin(INavigation navigation)
        {
            Navigation = navigation;
            ValidarConexionInternet();
        }
        #endregion
        #region OBJETOS

        public ImageSource FotoCelular
        {
            get { return _foto; }
            set
            {
                _foto = value;
                OnPropertyChanged();
            }
        }

        public string txtNombres
        {
            get { return _Nombres; }
            set { SetValue(ref _Nombres, value); }
        }

        public string txtApellidos
        {
            get { return _Apellidos; }
            set { SetValue(ref _Apellidos, value); }
        }

        public string txtLinkFoto
        {
            get { return _LinkFoto; }
            set { SetValue(ref _LinkFoto, value); }
        }

        public string txtArea
        {
            get { return _Area; }
            set { SetValue(ref _Area, value); }
        }

        public string SeleccionArea
        {
            get { return _Area; }
            set { SetProperty(ref _Area, value);
                txtArea = _Area;
            }
        }

        public string txtCorreo
        {
            get { return _Correo; }
            set { SetValue(ref _Correo, value); }
        }

        public string txtContraseña
        {
            get { return _Contraseña; }
            set { SetValue(ref _Contraseña, value); }
        }

        #endregion
        #region PROCESOS

        public async void CrearAutenticacion()
        {
            string ClaveWebAPI = "AIzaSyB5WJW7uB8VoWNJl10HLp_hRTr-ZedqmhE";

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ClaveWebAPI));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(txtCorreo.ToString(), txtContraseña.ToString());
                string gettoken = auth.FirebaseToken;

                await Navigation.PopAsync();
            }
            catch (Exception E)
            {
                await DisplayAlert("Alerta", E.Message, "Ok");
            }
        }

        public async Task InsertarAdmin()
        {
            var funcion = new DAdministradores();
            var parametros = new MAdmistradores();

            //Se asignan los valores de los objetos
            parametros.Apellidos = txtApellidos;
            parametros.Nombres = txtNombres;
            parametros.Area = SeleccionArea;
            parametros.LinkFoto = txtLinkFoto;
            parametros.Contraseña = txtContraseña;
            parametros.Correo = txtCorreo;

            await funcion.InsertarAdmin(parametros);
        }

        public async Task Registrar()
        {
            //Validaciones para el usuario y contraseña
            if (string.IsNullOrEmpty(this._Nombres) || (string.IsNullOrEmpty(this._Apellidos) || (string.IsNullOrEmpty(this._LinkFoto) 
                || (string.IsNullOrEmpty(this.SeleccionArea)))))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Debe de llenar todos los campos vacíos", "Aceptar"
                );
                return;
            }
            if (string.IsNullOrEmpty(this._Correo))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Necesita ingresar un correo", "Aceptar"
                );
                return;
            }
            if (string.IsNullOrEmpty(this._Contraseña))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Necesita ingresar una contraseña", "Aceptar"
                );
                return;
            }

            CrearAutenticacion();
            await InsertarAdmin();
            

            await DisplayAlert("Listo","Se ha insertado el nuevo admin","Ok");

            await Navigation.PopAsync();
        }
        public async void Cancelar()
        {
            txtApellidos = null;
            txtNombres = null;
            txtLinkFoto = null;
            SeleccionArea = null;
            txtCorreo = null;
            txtContraseña = null;
            Foto = null;

            await Navigation.PopAsync();
        }
        #endregion
        #region COMANDOS
        public ICommand Registrarcommand => new Command(async () => await Registrar());
        public ICommand Cancelarcommand => new Command(Cancelar);

        #endregion
    }
}
