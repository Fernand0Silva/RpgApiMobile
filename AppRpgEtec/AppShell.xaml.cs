using AppRpgEtec.ViewModels;
using AppRpgEtec.Views.Personagens;

namespace AppRpgEtec
{
    public partial class AppShell : Shell
    {
        private AppShellViewModel viewModel;

        public AppShell()
        {
            InitializeComponent();

            viewModel = new AppShellViewModel();
            BindingContext = viewModel;

            string login = Preferences.Get("UsuarioUsername", string.Empty);
            lblLogin.Text = $"login: {login}";

            Routing.RegisterRoute("cadPersonagemView", typeof(CadastroPersonagemView));
        }

        
    }
}
