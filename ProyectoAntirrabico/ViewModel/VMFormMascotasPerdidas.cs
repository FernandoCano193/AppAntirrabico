using System;
using System.Collections.Generic;
using System.Text;
//LIBRERIAS NECESARIAS
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.Media.Abstractions;
using ProyectoAntirrabico.Views;
using ProyectoAntirrabico.Model;
using ProyectoAntirrabico.Data;
using Plugin.Media;
using System.Diagnostics;

namespace ProyectoAntirrabico.ViewModel
{
    public class VMFormMascotasPerdidas:BaseViewModel
    {
        #region VARIABLES
        string _txtLinkFoto;
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
        string IDMascotaP;

        #endregion
        #region CONSTRUCTOR
        public VMFormMascotasPerdidas(INavigation navigation)
        {
            Navigation = navigation;
            ValidarConexionInternet();
        }
        #endregion

        #region OBJETOS
        public string txtLinkFoto
        {
            get { return _txtLinkFoto; }
            set { SetValue(ref _txtLinkFoto, value); }
        }
        public bool Carga
        {
            get { return _Carga; }
            set { SetValue(ref _Carga, value); }
        }

        public ImageSource FotoCelular
        {
            get { return _foto; }
            set
            {
                _foto = value;
                OnPropertyChanged();
            }
        }

        public string txtArea
        {
            get { return _txtArea; }
            set { SetValue(ref _txtArea, value); }
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


        public async void Cancelar()
        {
            Limpiar();
            await Navigation.PopAsync();

        }

        public async Task InsertarMP()
        {
            var funcion = new DMascotasPerdidas();
            var parametros = new MMascotasPerdidas();


            //Se asignan los valores de los objetos
            parametros.LinkFoto = "-";
            parametros.Area = SeleccionArea;
            parametros.Especie = txtEspecie;
            parametros.Sexo = SeleccionSexo;
            parametros.Estado = "Activo";
            parametros.Edad = txtEdad;
            parametros.Colores = txtColores;
            parametros.Raza = txtRaza;
            parametros.IdMascotaPerdida = "-";

            IDMascotaP = await funcion.InsertarMascotasPerdidas(parametros);


            //await Navigation.PopAsync();

        }

        public async Task AgregarMascota()
        {
            Carga = true;
            await InsertarMP();
            await SubirImagen();
            await EditarFoto();
            Carga = false;
            await DisplayAlert("Listo", "Se ha registrado una nueva mascota", "Ok");
            Limpiar();
        }

        public async Task EditarFoto()
        {
            var Funcion = new DMascotasPerdidas();
            var Parametros = new MMascotasPerdidas();

            Parametros.LinkFoto = RutaFoto;
            Parametros.IdMascotaPerdida = IDMascotaP;
            Parametros.Raza = txtRaza;
            Parametros.Sexo = txtSexo;
            Parametros.Estado = "Activo";
            Parametros.Edad = txtEdad;
            Parametros.Colores = txtColores;
            Parametros.Area = txtArea;
            Parametros.Especie = txtEspecie;

            await Funcion.EditarFoto(Parametros);
        }

        public async Task AgregarFoto()
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

        public async Task EliminarMascota(MMascotasPerdidas mascotasP)
        {
            var funcion = new DMascotasPerdidas();
            await funcion.EliminarFoto(mascotasP.IdMascotaPerdida+".jpg");
            await funcion.EliminarMascotasPerdidas(mascotasP);
            
        }

        public void Limpiar()
        {
            FotoCelular = "https://i.ibb.co/dKMhgbR/picture.png";
            txtLinkFoto = null;
            SeleccionArea = null;
            txtEspecie = null;
            SeleccionSexo = null;
            txtEdad = null;
            txtColores = null;
            txtRaza = null;
        }
        public async Task SubirImagen()
        {
            var funcion = new DMascotasPerdidas();
            RutaFoto = await funcion.SubirFotoStorage(file.GetStream(), IDMascotaP);

        }

        #endregion
        #region COMANDOS
        public ICommand AgregarMascotacommand => new Command(async () => await AgregarMascota());
        public ICommand AgregarFotocommand => new Command(async () => await AgregarFoto());
        public ICommand Cancelarcommand => new Command(Cancelar);
        #endregion
    }
}
