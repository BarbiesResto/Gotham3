using Gotham3.domain;
using Gotham3.persistence.Mocks;
using System.Linq;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace Gotham3.AcceptanceTests
{
    [Story(
        Title="Je veux pouvoir publier une prevention de sinistre",
        AsA="Utilisateur general",
        IWant="publier une prevention de sinistre",
        SoThat="preparer les citoyens à reagir.")]

    public class SinistresAcceptanceTests : AcceptanceTestsBase
    {
        private string _htmlPageContent;
        private Sinistre _sinistre;

        [Fact]
        public void Afficher_la_liste_des_signalements()
        {
            this.Given(x => Un_sinistre())
                .When(x => L_utilisateur_demande_de_voir_les_prevention_de_sinistre())
                .Then(x => L_information_concernant_le_sinistre_s_affiche())
                .BDDfy();
        }

        private async void Un_sinistre()
        {
            var mockRepo = new MockSinistresRepository();
            var sinistres = await mockRepo.GetAll();
            _sinistre = sinistres.First();
        }

        private async Task L_utilisateur_demande_de_voir_les_prevention_de_sinistre()
        {
            var response = await HttpClient.GetAsync("/Sinistres");
            response.EnsureSuccessStatusCode();

            _htmlPageContent = await response.Content.ReadAsStringAsync();
        }

        private void L_information_concernant_le_sinistre_s_affiche()
        {
            // Titre, texte de la prevention, mois vise, statut
            Assert.Contains(_sinistre.Title, _htmlPageContent);
            Assert.Contains(_sinistre.Description, _htmlPageContent);
            Assert.Contains(_sinistre.Month, _htmlPageContent);
            Assert.Contains(_sinistre.Status.ToString(), _htmlPageContent);

            //Accessible en tout temps
            Assert.Contains("Preventions", _htmlPageContent);
        }
    }
}
