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
    public class VMListaMascotasPerdidas:BaseViewModel
    {

        #region VARIABLES
        int _index;

        ObservableCollection<MMascotasPerdidas> _ListaMP;

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
            set { SetValue(ref _ListaMP, value);
                OnPropertyChanged();
            }
        }
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

        public async Task Editar()
        {
            await DisplayAlert("Funciona", "Funciona","Ok");
        }

        //public void DibujarMascotaPerdida(MMascotasPerdidas mascota, int index, StackLayout CDerecha, StackLayout CIzquierda)
        //{
        //    var _ubicacion = Convert.ToBoolean(index % 2);

        //    var carril = _ubicacion ? CDerecha : CIzquierda;

        //    var FramePadre = new Frame
        //    {
        //        BackgroundColor = Color.White,
        //        HeightRequest = 330,
        //        CornerRadius = 10,
        //        Padding = 0,
        //        HasShadow = false,
        //        Margin = 10
        //    };

        //    var StackLayoutPadre = new StackLayout
        //    {

        //    };

        //    var FrameHijo = new Frame
        //    {
        //        Margin = new Thickness(10, 5, 10, 0),
        //        Padding = 0,
        //        HasShadow = false,
        //        CornerRadius = 10,
        //        HeightRequest = 125,
        //    };

        //    var FotoMascota = new Image
        //    {
        //        Source = mascota.LinkFoto,
        //        HeightRequest = 125,
        //        HorizontalOptions = LayoutOptions.Center
        //    };

        //    var LabelRaza = new Label
        //    {
        //        Text = mascota.Raza,
        //        TextColor = Color.Black,
        //        FontAttributes = FontAttributes.Bold,
        //        FontSize = 22,
        //        HorizontalOptions = LayoutOptions.Center,
        //        CharacterSpacing = 1
        //    };

        //    var LabelEdad = new Label
        //    {
        //        Text = mascota.Edad,
        //        TextColor = Color.Gray,
        //        FontSize = 18,
        //        HorizontalOptions = LayoutOptions.Center,
        //    };

        //    var LabelSexo = new Label
        //    {
        //        Text = mascota.Sexo,
        //        TextColor = Color.Gray,
        //        FontSize = 18,
        //        HorizontalOptions = LayoutOptions.Center,
        //        Margin = new Thickness(0, -5, 0, 0)
        //    };

        //    var LabelArea = new Label
        //    {
        //        Text = "Localizado en " + mascota.Area,
        //        TextColor = Color.Gray,
        //        FontSize = 15,
        //        HorizontalOptions = LayoutOptions.Center,
        //        Margin = new Thickness(0, -5, 0, 0)
        //    };

        //    var SLBotones = new StackLayout
        //    {
        //        Orientation = StackOrientation.Horizontal,
        //        Margin = new Thickness(10, 10, 10, 10),
        //        HorizontalOptions = LayoutOptions.Center
        //    };

        //    var FrameBotonEdit = new Frame
        //    {
        //        BackgroundColor = Color.FromHex("#0984e3"),
        //        HeightRequest = 30,
        //        Padding = 5,
        //        CornerRadius = 5,
        //        Margin = new Thickness(0, 0, 10, 0)
        //    };

        //    var BotonEdit = new Image
        //    {
        //        Source = "https://i.ibb.co/2g2Yztc/edit.png",
        //        VerticalOptions = LayoutOptions.Center,
        //        HorizontalOptions = LayoutOptions.Center
        //    };

        //    var FrameBotonDelete = new Frame
        //    {
        //        BackgroundColor = Color.FromHex("#d63031"),
        //        HeightRequest = 30,
        //        Padding = 5,
        //        CornerRadius = 5,
        //    };

        //    var BotonDelete = new Image
        //    {
        //        Source = "https://i.ibb.co/ph9JNn1/delete.png",
        //        VerticalOptions = LayoutOptions.Center,
        //        HorizontalOptions = LayoutOptions.Center
        //    };

        //    //AGRUPACION DE LOS ELEMENTOS
        //    FrameHijo.Content = FotoMascota;
        //    StackLayoutPadre.Children.Add(FrameHijo);
        //    StackLayoutPadre.Children.Add(LabelRaza);
        //    StackLayoutPadre.Children.Add(LabelEdad);
        //    StackLayoutPadre.Children.Add(LabelSexo);
        //    StackLayoutPadre.Children.Add(LabelArea);

        //    FrameBotonDelete.Content = BotonDelete;
        //    FrameBotonEdit.Content = BotonEdit;
        //    SLBotones.Children.Add(FrameBotonEdit);
        //    SLBotones.Children.Add(FrameBotonDelete);

        //    StackLayoutPadre.Children.Add(SLBotones);
        //    FramePadre.Content = StackLayoutPadre;

        //    carril.Children.Add(FramePadre);
        //}

        public async Task MostrarMascotasPerdidas()
        {
            var funcion = new DMascotasPerdidas();

            ListaMP = await funcion.MostrarMP();

            //var box = new BoxView
            //{
            //    HeightRequest = 50
            //};
            //CDerecha.Children.Add(box);

            //foreach(var mascota in ListaMP)
            //{
            //    DibujarMascotaPerdida(mascota, _index, CDerecha, CIzquierda);
            //    _index++;
            //}
        }
        #endregion
        #region COMANDOS
        public ICommand IrListaMAcommand => new Command(async () => await IrListaMascotasAdopcion());
        public ICommand IrFormMPcommand => new Command(async () => await IrFormMascotasPerdidas());
        public ICommand IrEditar => new Command(async () => await Editar());
        #endregion

    }
}
