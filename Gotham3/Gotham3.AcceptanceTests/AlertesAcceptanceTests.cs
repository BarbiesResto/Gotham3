using Gotham3.domain;
using Gotham3.persistence.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;

namespace Gotham3.AcceptanceTests
{
    [Story(
        Title = "Je veux visualier la liste des alertes",
        AsA = "Utilisateur general",
        IWant = "voir la listes des alertes",
        SoThat = "Prendre action sur ceux-ci")]

    public class AlertesAcceptanceTests
    {
        public class SignalementsAcceptanceTests : AcceptanceTestsBase
        {
            private string _htmlPageContent;
            private Alerte _alerte;

            [Fact]
            public void Afficher_la_liste_des_alertes()
            {
                this.Given(x => Un_alerte())
                    .When(x => L_utilisateur_demande_de_voir_la_liste_des_alertes())
                    .Then(x => L_information_concernant_le_alertes_affiche())
                    .BDDfy();
            }

            private async void Un_alerte()
            {
                var mockRepo = new MockAlertesRepository();
                var alertes = await mockRepo.GetAll();
                _alerte = alertes.First();
            }

            private async Task L_utilisateur_demande_de_voir_la_liste_des_alertes()
            {
                var response = await HttpClient.GetAsync("/Alertes");
                response.EnsureSuccessStatusCode();

                _htmlPageContent = await response.Content.ReadAsStringAsync();
            }

            private void L_information_concernant_le_alertes_affiche()
            {
                //Nature de l'evènement, secteur, heure, commentaires
                Assert.Contains(_alerte.Event_Nature, _htmlPageContent);
                Assert.Contains(_alerte.Sector, _htmlPageContent);
                Assert.Contains(_alerte.Ressource, _htmlPageContent);
                Assert.Contains(_alerte.Risk, _htmlPageContent);
                Assert.Contains(_alerte.Advice, _htmlPageContent);

                //Accessible en tout temps
                Assert.Contains("Liste des alertes", _htmlPageContent);
            }
        }
    }
}
