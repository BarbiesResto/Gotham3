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
        Title = "Je veux visualier la liste des capsules informatives",
        AsA = "Utilisateur general",
        IWant = "voir la liste de capsules informatives",
        SoThat = "Prendre action sur celles-ci")]
    public class CapsulesInformativesAcceptancesTests : AcceptanceTestsBase
    {
        private string _htmlPageContent;
        private CapsuleInformative _capsuleInformative;

        [Fact]
        public void Afficher_la_liste_des_capsules_informatives()
        {
            this.Given(x => Une_capsule())
                .When(x => L_utilisateur_demande_de_voir_la_liste_des_capsules_informatives())
                .Then(x => L_information_concernant_la_capsule_s_affiche())
                .BDDfy();
        }

        private async void Une_capsule()
        {
            var mockRepo = new MockCapsulesInformativesRepository();
            var signalements = await mockRepo.GetAll();
            _capsuleInformative = signalements.First();
        }

        private async Task L_utilisateur_demande_de_voir_la_liste_des_capsules_informatives()
        {
            var response = await HttpClient.GetAsync("/CapsuleInformatives");
            response.EnsureSuccessStatusCode();

            _htmlPageContent = await response.Content.ReadAsStringAsync();
        }

        private void L_information_concernant_la_capsule_s_affiche()
        {
            //Titre, Description, Status, Lien
            Assert.Contains(_capsuleInformative.Title, _htmlPageContent);
            Assert.Contains(_capsuleInformative.Description, _htmlPageContent);
            Assert.Contains(_capsuleInformative.Status.ToString(), _htmlPageContent);
            Assert.Contains(_capsuleInformative.Link, _htmlPageContent);

            //Accessible en tout temps
            Assert.Contains("Capsules Informatives", _htmlPageContent);

            //Toutes les donnees sont affichees
            Assert.Contains("Title", _htmlPageContent);
            Assert.Contains("Description", _htmlPageContent);
            Assert.Contains("Link", _htmlPageContent);
            Assert.Contains("Status", _htmlPageContent);
        }
    }
}
