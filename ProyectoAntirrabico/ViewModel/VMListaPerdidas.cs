using ProyectoAntirrabico.Views;
using ProyectoAntirrabico.Model;
using ProyectoAntirrabico.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoAntirrabico.ViewModel
{
    public class VMListaPerdidas:BaseViewModel
    {
        #region VARIABLES
        string _Texto;
        int _index;

        List<MMascotasPerdidas> _ListMP;
        #endregion
        #region CONSTRUCTOR
        public VMListaPerdidas(INavigation navigation, StackLayout Izquierda, StackLayout Derecha)
        {
            Navigation = navigation;
            MostrarMascotaPerdida(Izquierda,Derecha);
            ValidarConexionInternet();
        }
        #endregion
        #region OBJETOS
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }

        public List<MMascotasPerdidas> ListMP
        {
            get { return _ListMP; }
            set { SetValue(ref _ListMP, value); }
        }

        #endregion
        #region PROCESOS
        public async Task IrLogin()
        {
            await Navigation.PushAsync(new Login());
        }
        public void DibujarMascotaPerdida(MMascotasPerdidas mascota, int index, StackLayout CDerecha, StackLayout CIzquierda)
        {
            var _ubicacion = Convert.ToBoolean(index % 2);
            var carril = _ubicacion ? CDerecha : CIzquierda;

            var FramePadre = new Frame
            {
                BackgroundColor = Color.White,
                HeightRequest = 330,
                CornerRadius = 15,
                Padding = 0,
                HasShadow = false,
                Margin = 10
            };

            var SLContenedor = new StackLayout
            {

            };

            var FrameFoto = new Frame
            {
                Margin = new Thickness(10, 5, 10, 0),
                Padding = 0,
                HasShadow = false,
                CornerRadius = 10,
                HeightRequest = 125,
            };

            var ImageFotoMascota = new Image
            {
                Source = mascota.LinkFoto,
                HeightRequest = 125,
                HorizontalOptions = LayoutOptions.Center
            };

            var LabelRaza = new Label
            {
                Text = mascota.Raza,
                TextColor = Color.Black,
                FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                HorizontalOptions = LayoutOptions.Center,
                CharacterSpacing = 1
            };

            var LabelEdad = new Label
            {
                Text = mascota.Edad,
                TextColor = Color.Gray,
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center
            };

            var LabelSexo = new Label
            {
                Text = mascota.Sexo,
                TextColor = Color.Gray,
                FontSize = 18,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, -5, 0, 0)
            };

            var LabelArea = new Label
            {
                Text = mascota.Area,
                TextColor = Color.Gray,
                FontSize = 16,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, -5, 0, 0),
                HorizontalTextAlignment = TextAlignment.Center
            };

            var BtnAdopcion = new Button
            {
                CornerRadius = 10,
                BackgroundColor = Color.FromHex("#F18043"),
                TextTransform = TextTransform.None,
                TextColor = Color.White,
                FontSize = 15,
                FontAttributes = FontAttributes.Bold,
                Text = "¡Yo lo perdí!",
                Margin = 5,
                HeightRequest = 40,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            //AGRUPACION DE LOS ELEMENTOS
            FrameFoto.Content = ImageFotoMascota;
            SLContenedor.Children.Add(FrameFoto);
            SLContenedor.Children.Add(LabelRaza);
            SLContenedor.Children.Add(LabelEdad);
            SLContenedor.Children.Add(LabelSexo);
            SLContenedor.Children.Add(LabelArea);
            SLContenedor.Children.Add(BtnAdopcion);

            FramePadre.Content = SLContenedor;

            carril.Children.Add(FramePadre);
        }

        public async Task MostrarMascotaPerdida(StackLayout CIzquierda, StackLayout CDerecha)
        {
            var funcion = new DMascotasPerdidas();
            ListMP = await funcion.MostrarMPList();

            var box = new BoxView
            {
                HeightRequest = 30
            };
            CDerecha.Children.Add(box);

            foreach(var mascota in ListMP)
            {
                DibujarMascotaPerdida(mascota, _index, CDerecha, CIzquierda);
                _index++;
            }
        }
        #endregion
        #region COMANDOS
        public ICommand IrLogincommand => new Command(async () => await IrLogin());
        //public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}
