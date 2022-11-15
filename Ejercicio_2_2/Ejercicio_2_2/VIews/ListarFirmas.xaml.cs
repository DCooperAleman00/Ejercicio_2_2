using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Ejercicio_2_2.VIews

{ 
    [XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ListarFirmas : ContentPage
{
    public ListarFirmas()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        listaFirmas.ItemsSource = await App.BaseDatosObject.GetFirmasList();
    }

    private void listaFirmas_SizeChanged(object sender, EventArgs e)
    {

    }
}
}