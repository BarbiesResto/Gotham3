using Gotham3.domain;
using Gotham3.persistence.Mocks;
using Gotham3.web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gotham3.UnitTests
{
    public class AlertesUnitTests
    {
        private AlertesController _alertesController;
        private MockAlertesRepository _mockRepo;

        public AlertesUnitTests()
        {
            //Arrange
            _mockRepo = new MockAlertesRepository();
            _alertesController = new AlertesController(_mockRepo);
        }

        //Index
        [Fact]
        public async void Test_Index_Returns_A_ViewResult_Alerte()
        {
            //Arrange

            //Act
            var result = await _alertesController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Test_Index_Model_Is_An_Enumerable_Model_of_Alertes()
        {
            //Arrange
            //Act
            var result = await _alertesController.Index() as ViewResult;

            //Assert
            Assert.IsAssignableFrom<IQueryable<Alerte>>(result.Model);
        }

        [Fact]
        public async Task Test_Index_Model_Contains_Alertes()
        {
            var result = await _alertesController.Index() as ViewResult;

            var model = result.Model as IQueryable<Alerte>;
            var exepctedNumber = _mockRepo._alertes.Count;
            Assert.Equal(exepctedNumber, model.Count());
        }

        //Delete
        [Fact]
        public async Task Test_Delete_Should_Delete_Alerte_FromList()
        {
            const int FIRST_ID = 0;
            int EXPECTED_NUMER = _mockRepo._alertes.Count - 1;
            await _alertesController.Delete(FIRST_ID);

            Assert.Equal(EXPECTED_NUMER, _mockRepo._alertes.Count);
        }

        [Fact]
        public void Test_Delete_WithNullId_ShouldThrowException()
        {
            var response = _alertesController.Delete(null);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        //Details
        [Fact]
        public void Test_Details_WithNullId_ShouldThrowException()
        {
            var response = _alertesController.Details(null);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public void Test_Details_WithHigherIdThanProjects_ShouldThrowException()
        {
            int ANY_HIGHER_NUMBER = _mockRepo._alertes.Count + 1;
            var response = _alertesController.Details(ANY_HIGHER_NUMBER);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void Test_Details_Id_A_ViewResult_Alertes()
        {
            //Arrange
            const int ANY_ID = 1;

            //Act
            var result = await _alertesController.Details(ANY_ID) as ViewResult;

            //Assert
            Assert.IsAssignableFrom<Alerte>(result.Model);
        }

        [Fact]
        public async void Test_Publish_WhenNotPublished_ShouldPublish()
        {
            //Arrange
            Alerte alert = new Alerte() { Id = 0, Event_Nature = "Rats!!", Sector = "Ste-Foy", Risk = "Moyen", Ressource = "Exterminateurs", Advice = "S'enfuir", Published = Status.Attente };

            //Act
            var result = await _alertesController.Publish(alert.Id, alert);

            //Assert
            Status EXPECTED_STATUS = Status.Publiée;
            Alerte updatedAlert = await _mockRepo.GetById(alert.Id);
            Assert.Equal(EXPECTED_STATUS, updatedAlert.Published);
        }

        [Fact]
        public async void Test_Update_ShouldUpdate()
        {
            //Arrange
            Alerte alert = new Alerte() { Id = 0, Event_Nature = "Rats!!", Sector = "Ste-Foy", Risk = "Moyen", Ressource = "Exterminateurs", Advice = "S'enfuir", Published = Status.Publiée };
            Alerte alertUpdated = new Alerte() { Id = 0, Event_Nature = "Test!!", Sector = "Ste-Foy", Risk = "Faible", Ressource = "Exterminateurs", Advice = "S'enfuir", Published = Status.Publiée };

            //Act
            var result = await _alertesController.Edit(alert.Id, alertUpdated);

            //Assert
            Alerte updatedAlert = await _mockRepo.GetById(alert.Id);

            string alertString = updatedAlert.ToString();
            string alertStringExpected = alertUpdated.ToString();
            Assert.Equal(alertStringExpected, alertString);
        }
    }
}
