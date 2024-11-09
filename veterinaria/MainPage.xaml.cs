
namespace veterinaria
{
    public partial class MainPage : FlyoutPage
    {

        public MainPage()
        {
            InitializeComponent();
            Flyout = new Maestro();
            Detail = new NavigationPage(new Detalle());

            App.FlyoutPage = this;
            Application.Current.MainPage = new NavigationPage(new login_registrer.login_registro());

        }


    }

}
