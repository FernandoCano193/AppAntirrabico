using Plugin.Media;
using Plugin.Media.Abstractions;
using ProyectoAntirrabico.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoAntirrabico.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormRegistroAdmin : ContentPage
    {

        public FormRegistroAdmin()
        {
            InitializeComponent();
            BindingContext = new VMRegistroAdmin(Navigation);
        }
        MediaFile file;

        private async void btnSeleccionarFoto_Clicked(object sender, EventArgs e)
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

                FotoCelular.Source = ImageSource.FromStream(() =>
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
    }
}