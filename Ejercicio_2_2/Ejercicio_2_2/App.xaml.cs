using Ejercicio_2_2.Controllers;
using Ejercicio_2_2;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ejercicio_2_2
{
    public partial class App : Application
    {
        static FirmaDataBase BaseDatos;

        public static FirmaDataBase BaseDatosObject
        {
            get
            {
                if (BaseDatos == null)
                {
                    BaseDatos = new FirmaDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FirmasDB.db3"));
                }
                return BaseDatos;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
