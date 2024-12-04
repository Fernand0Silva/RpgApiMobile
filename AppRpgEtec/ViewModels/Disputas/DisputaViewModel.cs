using AppRpgEtec.Models;
using AppRpgEtec.Services.Personagens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRpgEtec.ViewModels.Disputas
{
    public class DisputaViewModel : BaseViewModel
    {
        private PersonagemService pService;
        private string textoPesquisarPersonagem;

        public ObservableCollection<Personagem> PersonagensEncontrados { get; set; }

        private Command<string> pesquisarPersonagensCommand;

        public Command<string> GetPesquisarPersonagensCommand()
        {
            return pesquisarPersonagensCommand;
        }

        private void SetPesquisarPersonagensCommand(Command<string> value)
        {
            pesquisarPersonagensCommand = value;
        }

        public PersonagemService Atacante { get; set; }
        public PersonagemService Oponente { get; set; }
        public DisputaViewModel()
        {

            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);

            Atacante = new Personagem();
            Oponente = new Personagem();

            PersonagensEncontrados = new ObservableCollection<Personagem>();

            SetPesquisarPersonagensCommand(new Command<string>(async (string pesquisar) => { await PesquisarPersonagens(pesquisar); }));

        }
        public ICommandMapper PesquisarPersonagensCommand { get; set; }

        public async Task PesquisarPersonagens(string textePersquisaPersonagem)
        {
            try
            {
                PersonagensEncontrados = await pService.GetByNomeAproximadoAsync(textoPesquisarPersonagem);
                OnPropertyChanged(nameof(PersonagensEncontrados));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }
        public string DescricaoPersonagemAtacante
        {
            get => Atacante.Nome;
        }
        public string DescricaoPersonagemOponente
        {
            get => Oponente.Nome;
        }
        public async void SelecionarPersonagem(Personagem p)
        {
            try
            {
                string tipoCombate = await Application.Current.MainPage
                    .DisplayActionSheet("Atacante ou Oponente?", "Cancelar", "", "Atacante", "Oponente");

                if (tipoCombate == "Atacante")
                {
                    Atacante = p;
                    OnPropertyChanged(nameof(DescricaoPersonagemOponente));
                }
                else if (tipoCombate == "oponente")
                {
                    Oponente = p;
                    OnPropertyChanged(nameof(DescricaoPersonagemOponente));
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }

        }

        internal object ObterPersonagens()
        {
            throw new NotImplementedException();
        }

        private Personagem personagemSelecionado;
        public Personagem PersonaSelecionado
        {
            set
            {
                if (value != null)
                {
                    PersonaSelecionado = value;
                    SelecionarPersonagem(personagemSelecionado);
                    OnPropertyChanged();
                    PersonagensEncontrados.Clear();

                }
            }
        }
        private string textBuscaDigitado = string.Empty;
        public string TextBuscaDigitado
        {
            get { return textBuscaDigitado; }
            set
            {
                if ((value != null && !string.IsNullOrEmpty(value) && value.Length > 0))
                {
                    textBuscaDigitado = value;
                    _ = PesquisarPersonagens(textBuscaDigitado);
                }
                else
                {
                    PersonagensEncontrados.Clear();
                }
            }

        }
    }
}
