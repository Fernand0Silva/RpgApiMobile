using AppRpgEtec.ViewModels.Personagens;

namespace AppRpgEtec.Views.Disputas;

public partial class ListagemView : ContentPage
{
	ListagempersonagemViewModel viewModel;

	public ListagemView()
	{
		InitializeComponent();

		viewModel = new ListagempersonagemViewModel();
		BindingContext = viewModel;
		Title = "Personagens - App Rpg Etec";
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_=viewModel.ObterPersonagens();
    }

    private void Button_DescendantAdded(object sender, ElementEventArgs e)
    {

    }
}