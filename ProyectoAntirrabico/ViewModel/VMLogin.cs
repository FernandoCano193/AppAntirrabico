using Firebase.Auth;
using Newtonsoft.Json;
using ProyectoAntirrabico.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoAntirrabico.ViewModel
{
    public class VMLogin:BaseViewModel
    {
        #region VARIABLES
        string _txtCorreo;
        string _txtContraseña;
        #endregion

        #region CONSTRUCTOR
        public VMLogin(INavigation navigation)
        {
            Navigation = navigation;
            ValidarConexionInternet();
        }
        #endregion

        #region OBJETOS
        public string txtCorreo
        {
            get { return _txtCorreo; }
            set { SetValue(ref _txtCorreo, value); }
        }

        public string txtContraseña
        {
            get { return _txtContraseña; }
            set { SetValue(ref _txtContraseña, value); }
        }

        #endregion

        #region PROCESOS
        public async Task IrRegistrorAdmin()
        {
            txtCorreo = null;
            txtContraseña = null;
            await Navigation.PushAsync(new FormRegistroAdmin());
        }

        public async void LoginValidacion()
        {
            //Validaciones para el usuario y contraseña

            if (string.IsNullOrEmpty(this._txtCorreo))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Necesita ingresar el correo", "Aceptar"
                );
                return;
            }
            if (string.IsNullOrEmpty(this._txtContraseña))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Necesita ingresar la contraseña", "Aceptar"
                );
                return;
            }

            string ClaveWebAPI = "AIzaSyB5WJW7uB8VoWNJl10HLp_hRTr-ZedqmhE";

            try
            {
                //Se incia el proceso de validacion para las credenciales

                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(ClaveWebAPI));
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(txtCorreo.ToString(), txtContraseña.ToString());
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefresToken", serializedcontnet);

                //Navegacion a la view FormMascotasAdopcion
                await Navigation.PushAsync(new HomeAdmin());

                //Se limpian los entrys del correo y contraseña
                txtContraseña = null;
                txtCorreo = null;

            }
            catch (Exception E) //En caso de no estar registrado o ingresar un correo o contraseña no valida
            {
                await DisplayAlert("Error", "Correo o contraseña son incorrectas, verificar", "Aceptar");
            }
            
        }

        public void ProcesoSimple()
        {

        }
        #endregion

        #region COMANDOS
        //Comando utilizado para ejecutar el proceso IrRegistroAdmin
        public ICommand IrRegistrocommand => new Command(async () => await IrRegistrorAdmin());

        //Comando utilizado para ejecutar el proceso LoginValidacion
        public ICommand Logincommand => new Command(LoginValidacion);
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}
