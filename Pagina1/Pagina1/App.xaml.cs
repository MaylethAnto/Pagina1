using Pagina1.Controlador;
using Pagina1.Vista;
using System;
using System.IO;
using Xamarin.Forms;

namespace Pagina1
{
    public partial class App : Application
    {
        static PetProfileController database;
        public static PetProfileController Database
        {
            get
            {
                if (database == null)
                {
                    string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PetProfile.db");
                    database = new PetProfileController(databasePath);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new PetProfilePage());
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