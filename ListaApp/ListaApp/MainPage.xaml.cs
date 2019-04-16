using Xamarin.Forms;
using ListaApp.ViewModel;


namespace ListaApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ContatoViewModel();
        }
    }
}
