using Plugin.Media;
using Plugin.Media.Abstractions;
using ProyectoAntirrabico.Data;
using ProyectoAntirrabico.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoAntirrabico.ViewModel
{
    public class VMEditarMP :BaseViewModel
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
        bool btnCargarFoto;
        bool Decision;
        ImageSource _foto = "https://i.ibb.co/dKMhgbR/picture.png";

        MediaFile file;
        string RutaFoto;
        string IDMascotaP;
        public MMascotasPerdidas RecibeMascotas { get; set; }
        #endregion
        #region CONSTRUCTOR
        public VMEditarMP(INavigation navigation, MMascotasPerdidas TraeMascotas)
        {
            Navigation = navigation;
            RecibeMascotas = TraeMascotas;
            txtColores = RecibeMascotas.Colores;
            IDMascotaP = RecibeMascotas.IdMascotaPerdida;
            txtEdad = RecibeMascotas.Edad;
            txtEspecie = RecibeMascotas.Especie;
            txtRaza = RecibeMascotas.Raza;
            SeleccionArea = RecibeMascotas.Area;
            SeleccionSexo = RecibeMascotas.Sexo;
            FotoCelular = RecibeMascotas.LinkFoto;
            txtLinkFoto = RecibeMascotas.LinkFoto;
            
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
        public async Task Editar()
        {

            var Funcion = new DMascotasPerdidas();
            var Parametros = new MMascotasPerdidas();

            if (string.IsNullOrEmpty(this._txtColores) || string.IsNullOrEmpty(this._txtEdad) || string.IsNullOrEmpty(this._txtRaza) || string.IsNullOrEmpty(this._txtEspecie))
            {
                await Application.Current.MainPage.DisplayAlert(
                "Error", "Debe de llenar todos los campos vacíos", "Aceptar");
                return;
            }
            else
            {
                Decision = await DisplayAlert("EDITAR", "¿Esta seguro en editar esta mascota?", "Si", "Cancelar");

                if (Decision)
                {
                    Carga = true;

                    if (btnCargarFoto == false)
                    {

                        Parametros.LinkFoto = txtLinkFoto;
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
                    else
                    {
                        await SubirImagen();
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
                    Carga = false;
                    await DisplayAlert("Información", "Se ha actualizado la mascota", "Ok");
                }
            }
           
        }
        public async Task AgregarFoto()
        {
            btnCargarFoto = true;
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

        public async Task Eliminar()
        {
            Decision =await DisplayAlert("ELIMINAR","¿Esta seguro de eliminar esta mascota?","Si","Cancelar");
            if(Decision)
            {
                Carga = true;
                var funcion = new VMFormMascotasPerdidas(Navigation);
                await funcion.EliminarMascota(RecibeMascotas);
                Carga = false;
                await DisplayAlert("Listo", "Se ha eliminado esta mascota", "Ok");
                await Navigation.PopAsync();
            }
            
        }
        public async Task SubirImagen()
        {

            var funcion = new DMascotasPerdidas();
            await funcion.EliminarFoto(IDMascotaP+".jpg");
            RutaFoto = await funcion.SubirFotoStorage(file.GetStream(), IDMascotaP);

        }

        public async Task Regresar()
        {
            await Navigation.PopAsync();
        }

        #endregion

        #region COMANDOS
        public ICommand editarcommand => new Command(async () => await Editar());
        public ICommand AgregarFotocommand => new Command(async () => await AgregarFoto());
        public ICommand Eliminarcommand => new Command(async () => await Eliminar());
        public ICommand RegresarCommand => new Command(async () => await Regresar());
        #endregion
    }
}
