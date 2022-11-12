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
    public class VMEditarMA:BaseViewModel
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
        bool btnCargaFoto;
        bool Decision;

        ImageSource _foto = "https://i.ibb.co/dKMhgbR/picture.png";

        MediaFile file;
        string RutaFoto;
        string IDMascotaA;

        public MMascotasAdopocion RecibeParametros { get; set; }
        #endregion
        #region CONSTRUCTOR
        public VMEditarMA(INavigation navigation, MMascotasAdopocion TraeParametros)
        {
            Navigation = navigation;
            RecibeParametros = TraeParametros;

            FotoCelular = RecibeParametros.LinkFoto;
            txtLinkFoto = RecibeParametros.LinkFoto;
            SeleccionArea = RecibeParametros.Area;
            txtRaza = RecibeParametros.Raza;
            txtEdad = RecibeParametros.Edad;
            txtNombre = RecibeParametros.Nombre;
            txtEspecie = RecibeParametros.Especie;
            txtColores = RecibeParametros.Colores;
            IDMascotaA = RecibeParametros.IdMascotaAdopcion;
            SeleccionSexo = RecibeParametros.Sexo;
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
        public async Task Regresar()
        {
            await Navigation.PopAsync();
        }
        public async Task Eliminar()
        {
            Decision = await DisplayAlert("ELIMINAR", "¿Esta seguro de eliminar esta mascota?", "Si", "Cancelar");
            if (Decision)
            {
                Carga = true;
                var funcion = new VMFormMascotasAdopcion(Navigation);
                await funcion.EliminarMascota(RecibeParametros);
                Carga = false;
                await DisplayAlert("Listo", "Se ha eliminado esta mascota", "Ok");
                await Navigation.PopAsync();
            }
        }

        public async Task Editar()
        {
            var funcion = new DMascotasAdopcion();
            var parametros = new MMascotasAdopocion();

            Decision = await DisplayAlert("EDITAR", "¿Esta seguro en editar esta mascota?", "Si", "Cancelar");
            if(Decision)
            {
                Carga = true;

                if(btnCargaFoto==false)
                {
                    parametros.Area = SeleccionArea;
                    parametros.Colores = txtColores;
                    parametros.Sexo = SeleccionSexo;
                    parametros.LinkFoto = txtLinkFoto;
                    parametros.IdMascotaAdopcion = IDMascotaA;
                    parametros.Nombre = txtNombre;
                    parametros.Especie = txtEspecie;
                    parametros.Edad = txtEdad;
                    parametros.Raza = txtRaza;

                    await funcion.EditarFoto(parametros);
                }
                else
                {
                    await SubirImagen();
                    parametros.Area = SeleccionArea;
                    parametros.Colores = txtColores;
                    parametros.Sexo = SeleccionSexo;
                    parametros.LinkFoto = RutaFoto;
                    parametros.IdMascotaAdopcion = IDMascotaA;
                    parametros.Nombre = txtNombre;
                    parametros.Especie = txtEspecie;
                    parametros.Edad = txtEdad;
                    parametros.Raza = txtRaza;
                    await funcion.EditarFoto(parametros);
                }
                Carga = false;
                await DisplayAlert("Información", "Se ha actualizado la mascota", "Ok");
            }
        }

        public async Task AgregarFoto()
        {
            btnCargaFoto = true;
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

        public async Task SubirImagen()
        {

            var funcion = new DMascotasAdopcion();
            await funcion.EliminarFoto(IDMascotaA + ".jpg");
            RutaFoto = await funcion.SubirFotoStorage(file.GetStream(), IDMascotaA);

        }


        #endregion

        #region COMANDOS
        public ICommand RegresarCommand => new Command(async () => await Regresar());
        public ICommand Eliminarcommand => new Command(async () => await Eliminar());
        public ICommand AgregarFotocommand => new Command(async () => await AgregarFoto());
        public ICommand Editarcommand => new Command(async () => await Editar());
        #endregion
    }
}
