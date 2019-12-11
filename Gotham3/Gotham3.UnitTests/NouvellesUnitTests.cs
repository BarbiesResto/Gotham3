
using Gotham3.domain;
using Gotham3.persistence.Mocks;
using Gotham3.web;
using Gotham3.web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Gotham3.UnitTests
{
    public class NouvellesUnitTests
    {
        private NouvellesController _nouvelleController;
        private MockNouvellesRepository _mockRepo;

        public NouvellesUnitTests()
        {
            _mockRepo = new MockNouvellesRepository();
            _nouvelleController = new NouvellesController(_mockRepo);
        }

        //Index
        [Fact]
        public async void Test_Index_Returns_A_ViewResult()
        {
            //Arrange

            //Act
            var result = await _nouvelleController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Test_Index_Model_Is_An_Enumerable_Model_of_Nouvelles()
        {
            //Arrange
            //Act
            var result = await _nouvelleController.Index() as ViewResult;

            //Assert
            Assert.IsAssignableFrom<IQueryable<Nouvelle>>(result.Model);
        }

        [Fact]
        public async Task Test_Index_Model_Contains_Nouvelles()
        {
            var result = await _nouvelleController.Index() as ViewResult;

            var model = result.Model as IQueryable<Nouvelle>;
            var exepctedNumber = _mockRepo._nouvelles.Count;
            Assert.Equal(exepctedNumber, model.Count());
        }
        
        //Delete
        [Fact]
        public async Task Test_Delete_Should_DeleteFromList()
        {
            const int FIRST_ID = 0;
            int EXPECTED_NUMER = _mockRepo._nouvelles.Count - 1;
            await _nouvelleController.Delete(FIRST_ID);

            Assert.Equal(EXPECTED_NUMER, _mockRepo._nouvelles.Count);
        }

        [Fact]
        public void Test_Delete_WithNullId_ShouldThrowException()
        {
            var response = _nouvelleController.Delete(null);
            Assert.IsType<NotFoundResult>(response.Result);
        }
        
        //Details
        [Fact]
        public void Test_Details_WithNullId_ShouldThrowException()
        {
            var response = _nouvelleController.Details(null);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public void Test_Details_WithHigherIdThanProjects_ShouldThrowException()
        {
            int ANY_HIGHER_NUMBER = _mockRepo._nouvelles.Count + 1;
            var response = _nouvelleController.Details(ANY_HIGHER_NUMBER);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void Test_Details_Id_A_ViewResult_Nouvelle()
        {
            //Arrange
            const int ANY_ID = 1;

            //Act
            var result = await _nouvelleController.Details(ANY_ID) as ViewResult;

            //Assert
            Assert.IsAssignableFrom<Nouvelle>(result.Model);
        }

        //Create
        [Fact]
        public async void Test_Create_NewNouvelle_ShouldAddToRepoLength()
        {
            //Arrange
            var nouvelle = new Nouvelle() { Title="Any_title", Text_Desc="Any_desc" };
            var firstLength = _mockRepo._nouvelles.Count();

            //Act
            await _nouvelleController.Create(nouvelle);

            //Assert
            var secondLength = _mockRepo._nouvelles.Count();
            Assert.NotEqual(firstLength, secondLength);
        }

        [Fact]
        public async void Test_Create_ShouldAddNewProjectTorepo()
        {
            //Arrange
            var nouvelle = new Nouvelle() { Title = "Any_title", Text_Desc = "Any_desc" };

            //Act
            await _nouvelleController.Create(nouvelle);

            //Assert
            Assert.Equal(nouvelle, _mockRepo._nouvelles.LastOrDefault());
        }

        //Edit
        [Fact]
        public async void Test_Edit_WhenNouvelleIsEditted_ShouldChangeNouvelle()
        {
            //Arrange
            var nouvelle = new Nouvelle() { Id = 0, Title = "Any_title", Text_Desc = "Any_desc" };
            var firstNouvelle = _mockRepo._nouvelles.First();

            //Act
            await _nouvelleController.Edit(firstNouvelle.Id, nouvelle);

            //Assert
            Assert.Equal(nouvelle.Title, firstNouvelle.Title);
            Assert.Equal(nouvelle.Text_Desc, firstNouvelle.Text_Desc);
        }

        //Publish
        [Fact]
        public async void Test_Publish_ShouldChangeStatusOfExistingNouvelle()
        {
            //Arrange
            var firstStatus = _mockRepo._nouvelles.First().Status;

            //Act
            await _nouvelleController.Publish(_mockRepo._nouvelles.First().Id);

            //Assert
            var secondStatus = _mockRepo._nouvelles.First().Status;
            Assert.NotEqual(firstStatus, secondStatus);
        }

        [Fact]
        public async void Test_Publish_ShouldChangeStatusOfExistingNouvelleToGoodValue()
        {
            //Arrange
            var firstStatus = _mockRepo._nouvelles.First().Status;
            Assert.Equal(Status.Attente, firstStatus);

            //Act
            await _nouvelleController.Publish(_mockRepo._nouvelles.First().Id);

            //Assert
            var secondStatus = _mockRepo._nouvelles.First().Status;
            Assert.Equal(Status.Publiee, secondStatus);
        }
    }
}
