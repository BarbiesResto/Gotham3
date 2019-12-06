
using Gotham3.domain;
using Gotham3.persistence.Mocks;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace Gotham3.AcceptanceTests
{
    [Story(
        Title="Je veux visualier la liste des signalements",
        AsA="Utilisateur général",
        IWant="voir la listes des signalements",
        SoThat="Prendre action sur ceux-ci")]

    public class SignalementsAcceptanceTests : AcceptanceTestsBase
    {
        private string _htmlPageContent;
        private Signalement _signalement;

        [Fact]
        public void Afficher_la_liste_des_signalements()
        {
            this.Given(x => Un_signalement())
                .When(x => L_utilisateur_demande_de_voir_la_liste_des_signalements())
                .Then(x => L_information_concernant_le_signalement_s_affiche())
                .BDDfy();
        }

        private async void Un_signalement()
        {
            var mockRepo = new MockSignalementsRepository();
            var signalements = await mockRepo.GetAll();
            _signalement = signalements.First();
        }

        private async Task L_utilisateur_demande_de_voir_la_liste_des_signalements()
        {
            var response = await HttpClient.GetAsync("");
            response.EnsureSuccessStatusCode();

            _htmlPageContent = await response.Content.ReadAsStringAsync();
        }

        private void L_information_concernant_le_signalement_s_affiche()
        {
            //Nature de l'évènement, secteur, heure, commentaires
            Assert.Contains(_signalement.Event_Nature, _htmlPageContent);
            Assert.Contains(_signalement.Sector, _htmlPageContent);
            Assert.Contains(_signalement.Time, _htmlPageContent);
            Assert.Contains(_signalement.Comment, _htmlPageContent);

            //Accessible en tout temps
            Assert.Contains("Listes des signalements", _htmlPageContent);

            //Services d'urgences peuvent être contactés facilement
            Assert.Contains("Appeler les policiers", _htmlPageContent);
            Assert.Contains("Appeler les pompiers", _htmlPageContent);
            Assert.Contains("Appeler l'ambulance", _htmlPageContent);
        }
    }
}
