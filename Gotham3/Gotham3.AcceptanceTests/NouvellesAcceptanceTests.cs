
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
        Title="Je veux visualier la liste des nouvelles",
        AsA="Utilisateur général",
        IWant="faire la gestion des nouvelles existantes",
        SoThat="garder une liste à jour et pertinente")]
    
    public class NouvellesAcceptanceTests : AcceptanceTestsBase
    {
        private string _htmlPageContent;
        private Nouvelle _nouvelle;

        [Fact]
        public void Afficher_la_liste_des_signalements()
        {
            this.Given(x => Une_nouvelle())
                .When(x => L_utilisateur_demande_de_voir_la_liste_des_nouvelles())
                .Then(x => L_information_concernant_la_nouvelle_s_affiche())
                .BDDfy();
        }

        private async void Une_nouvelle()
        {
            var mockRepo = new MockNouvellesRepository();
            var nouvelle = await mockRepo.GetAll();
            _nouvelle = nouvelle.First();
        }

        private async Task L_utilisateur_demande_de_voir_la_liste_des_nouvelles()
        {
            var response = await HttpClient.GetAsync("/Nouvelles");
            response.EnsureSuccessStatusCode();

            _htmlPageContent = await response.Content.ReadAsStringAsync();
        }

        private void L_information_concernant_la_nouvelle_s_affiche()
        {
            //Nature de l'évènement, secteur, heure, commentaires
            Assert.Contains(_nouvelle.Title, _htmlPageContent);
            Assert.Contains(_nouvelle.Text_Desc, _htmlPageContent);
            Assert.Contains(_nouvelle.Link_Media, _htmlPageContent);

            //Accessible en tout temps
            Assert.Contains("Nouvelle", _htmlPageContent);

            //Services d'urgences peuvent être contactés facilement
            Assert.Contains("Modifier", _htmlPageContent);
            Assert.Contains("Détails", _htmlPageContent);
            Assert.Contains("Supprimer", _htmlPageContent);
            Assert.Contains("Publier", _htmlPageContent);
        }
    }
}
