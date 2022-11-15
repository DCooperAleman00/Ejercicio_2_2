using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SignaturePad.Forms;
using Xamarin.Essentials;
using Ejercicio_2_2.Models;
using Ejercicio_2_2.VIews;
using Xamarin.Essentials;


namespace Ejercicio_2_2
{
    public partial class MainPage : ContentPage
    {
        byte[] ImageBytes;
        public MainPage()
        {
            InitializeComponent();

        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            var status = await Permissions.RequestAsync<Permissions.StorageRead>();
            var status2 = await Permissions.RequestAsync<Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted || status2 != PermissionStatus.Granted)
            {
                return;  
            }

            try
            {
                 var image = await PadView.GetImageStreamAsync(SignatureImageFormat.Png);

                 SaveToDevice(image);

                 var mStream = (MemoryStream)image;
                byte[] data = mStream.ToArray();
                string base64Val = Convert.ToBase64String(data);
                ImageBytes = Convert.FromBase64String(base64Val);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Warning", "Favor haga su firma", "Ok");
            }

           FirmaModelo firma = new FirmaModelo();
            firma.Nombre = txtnombre.Text;
            firma.Descripcion = txtdescripcion.Text;
            firma.Firma = ImageBytes;

            if (String.IsNullOrEmpty(txtnombre.Text) || String.IsNullOrEmpty(txtdescripcion.Text))
            {
                await DisplayAlert("Warning", "LLENE TODOS LOS CAMPOS", "Ok");
            }
            else
            {
                try
                {

                    await App.BaseDatosObject.CreateFirma(firma);
                    await DisplayAlert("Aviso", "GUARDADO CON EXITO", "Ok");
                    txtnombre.Text = "";
                    txtdescripcion.Text = "";
                    PadView.Clear();
                }
                catch
                {
                    await DisplayAlert("Advertencia", " ERROR!! NO SE PUDO GUARDAR LA FIRMA", "OK");
                }

            }

        }

        private async void btnListarFirma_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new ListarFirmas());
        }


        private void SaveToDevice(Stream img)
        {
            try
            {
                var filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures).ToString(), "Firmas");

                if (!Directory.Exists(filename))
                {
                    Directory.CreateDirectory(filename);
                }

                string nameFile = DateTime.Now.ToString("yyyyMMddhhmmss") + ".png";
                filename = Path.Combine(filename, nameFile);

                var mStream = (MemoryStream)img;
                Byte[] bytes = mStream.ToArray();
                File.WriteAllBytes(filename, bytes);

                DisplayAlert("Notificación", "Firma guardada en la ruta: " + filename, "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Ok");
            }

        }
    }
}
