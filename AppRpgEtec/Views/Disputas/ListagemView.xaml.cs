using AppRpgEtec.ViewModels.Disputas;
using AppRpgEtec.ViewModels.Personagens;

namespace AppRpgEtec.Views.Disputas;

public partial class ListagemView : ContentPage
{
	DisputaViewModel viewModel;

	public ListagemView()
	{
		InitializeComponent();

        //viewModel = new ListagempersonagemViewModel();
        viewModel = new DisputaViewModel();
        BindingContext = viewModel;
		Title = "Personagens - App Rpg Etec";
	}

    protected override void OnAppearing()
    {
        NewMethod();
        _ = viewModel.ObterPersonagens();
    }

    private void NewMethod()
    {
        base.OnAppearing();
    }

    private void Button_DescendantAdded(object sender, ElementEventArgs e)
    {

    }
}