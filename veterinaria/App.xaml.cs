namespace veterinaria
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new login_registrer.Login();
        }
    }
}
