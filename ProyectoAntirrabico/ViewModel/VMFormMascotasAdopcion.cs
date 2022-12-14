using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
//LIBRERIAS NECESARIAS
using Plugin.Media.Abstractions;
using ProyectoAntirrabico.Views;
using ProyectoAntirrabico.Model;
using ProyectoAntirrabico.Data;
using Plugin.Media;
using System.Diagnostics;

namespace ProyectoAntirrabico.ViewModel
{
    public class VMFormMascotasAdopcion : BaseViewModel
    {
        #region VARIABLES
        string _txtLinkFoto;
        string _txtNombre;
        string _txtArea;
        string _txtEspecie;
        string _txtSexo;
        string _txtEdad;
        string _txtColores;
        string _txtRaza;
        bool _Carga;

        ImageSource _foto = "https://i.ibb.co/dKMhgbR/picture.png";

        MediaFile file;
        string RutaFoto;
        string IDMascotaA;
        #endregion

        #region CONSTRUCTOR
        public VMFormMascotasAdopcion(INavigation navigation)
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

        public bool Carga
        {
            get { return _Carga; }
            set { SetValue(ref _Carga, value); }
        }

        public string txtLinkFoto
        {
            get { return _txtLinkFoto; }
            set { SetValue(ref _txtLinkFoto, value); }
        }

        public string txtArea
        {
            get { return _txtArea; }
            set { SetValue(ref _txtArea, value); }
        }
        public string txtNombre
        {
            get { return _txtNombre; }
            set { SetValue(ref _txtNombre, value); }
        }

        public string SeleccionArea
        {
            get { return _txtArea; }
            set
            {
                SetProperty(ref _txtArea, value);
                txtArea = _txtArea;
            }
        }
        public string txtEspecie
        {
            get { return _txtEspecie; }
            set { SetValue(ref _txtEspecie, value); }
        }

        public string txtSexo
        {
            get { return _txtSexo; }
            set { SetValue(ref _txtSexo, value); }
        }

        public string SeleccionSexo
        {
            get { return _txtSexo; }
            set
            {
                SetProperty(ref _txtSexo, value);
                txtSexo = _txtSexo;
            }
        }

        public string txtEdad
        {
            get { return _txtEdad; }
            set { SetValue(ref _txtEdad, value); }
        }

        public string txtColores
        {
            get { return _txtColores; }
            set { SetValue(ref _txtColores, value); }
        }

        public string txtRaza
        {
            get { return _txtRaza; }
            set { SetValue(ref _txtRaza, value); }
        }

        #endregion

        #region PROCESOS

        public async Task Insertar()
        {
            var funcion = new DMascotasAdopcion();
            var parametros = new MMascotasAdopocion();

            parametros.LinkFoto = "-";
            parametros.Nombre = txtNombre;
            parametros.Area = SeleccionArea;
            parametros.Especie = txtEspecie;
            parametros.Sexo = SeleccionSexo;
            parametros.Edad = txtEdad;
            parametros.Colores = txtColores;
            parametros.Raza = txtRaza;
            parametros.IdMascotaAdopcion = "-";

            IDMascotaA = await funcion.InsertarMA(parametros);
            
        }

        public async Task EliminarMascota(MMascotasAdopocion mascotas)
        {
            var funcion = new DMascotasAdopcion();
            await funcion.EliminarFoto(mascotas.IdMascotaAdopcion+".jpg");
            await funcion.EliminarMascotasPerdidas(mascotas);
        }

        public async Task SubirImagen()
        {
            var funcion = new DMascotasAdopcion();
            RutaFoto = await funcion.SubirFotoStorage(file.GetStream(), IDMascotaA);
        }

        public async Task EditarFoto()
        {
            var funcion = new DMascotasAdopcion();
            var parametros = new MMascotasAdopocion();

            parametros.LinkFoto = RutaFoto;
            parametros.IdMascotaAdopcion = IDMascotaA;
            parametros.Raza = txtRaza;
            parametros.Area = txtArea;
            parametros.Edad = txtEdad;
            parametros.Colores = txtColores;
            parametros.Sexo = txtSexo;
            parametros.Especie = txtEspecie;
            parametros.Nombre = txtNombre;

            await funcion.EditarFoto(parametros);
        }

        public async Task AgregarMascota()
        {
            Carga = true;
            await Insertar();
            await SubirImagen();
            await EditarFoto();
            Carga = false;

            await DisplayAlert("Listo", "Se ha registrado una nueva mascota", "Ok");
            Limpiar();
        }


        public async Task Cancelar()
        {
            Limpiar();
            await Navigation.PopAsync();
        }

        public void Limpiar()
        {
            FotoCelular = "https://i.ibb.co/dKMhgbR/picture.png";
            txtLinkFoto = null;
            SeleccionArea = null;
            txtEspecie = null;
            SeleccionSexo = null;
            txtNombre = null;
            txtEdad = null;
            txtColores = null;
            txtRaza = null;
        }


        private async Task AgregarFoto()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                    return;
                FotoCelular = ImageSource.FromStream(() =>
                {
                    var RutaFoto = file.GetStream();
                    return RutaFoto;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion
        #region COMANDOS
        public ICommand Insertarcommand => new Command(async () => await AgregarMascota());
        public ICommand AgregarFotocommand => new Command(async () => await AgregarFoto());
        public ICommand Cancelarcommand => new Command(async () => await Cancelar());
        #endregion
    }
}
