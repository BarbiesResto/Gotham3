
using Gotham3.domain;
using Gotham3.persistence.Mocks;
using Gotham3.web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Gotham3.UnitTests
{
    public class SignalementsUnitTests
    {
        private SignalementsController _signalementController;
        private MockSignalementsRepository _mockRepo;

        public SignalementsUnitTests() 
        {
            //Arrange
            _mockRepo = new MockSignalementsRepository();
            _signalementController = new SignalementsController(_mockRepo);
        }

        //Index
        [Fact]
        public async void Test_Index_Returns_A_ViewResult_Signalement()
        {
            //Arrange

            //Act
            var result = await _signalementController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Test_Index_Model_Is_An_Enumerable_Model_of_Signalements()
        {
            //Arrange
            //Act
            var result = await _signalementController.Index() as ViewResult;

            //Assert
            Assert.IsAssignableFrom<IQueryable<Signalement>>(result.Model);
        }

        [Fact]
        public async Task Test_Index_Model_Contains_Signalements()
        {
            var result = await _signalementController.Index() as ViewResult;

            var model = result.Model as IQueryable<Signalement>;
            var exepctedNumber = _mockRepo._signalements.Count;
            Assert.Equal(exepctedNumber, model.Count());
        }

        //Delete
        [Fact]
        public async Task Test_Delete_Should_DeleteFromList()
        {
            const int FIRST_ID = 0;
            int EXPECTED_NUMER = _mockRepo._signalements.Count - 1;
            await _signalementController.Delete(FIRST_ID);

            Assert.Equal(EXPECTED_NUMER, _mockRepo._signalements.Count);
        }

        [Fact]
        public void Test_Delete_WithNullId_ShouldThrowException()
        {
            var response = _signalementController.Delete(null);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        //Details
        [Fact]
        public void Test_Details_WithNullId_ShouldThrowException()
        {
            var response = _signalementController.Details(null);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public void Test_Details_WithHigherIdThanProjects_ShouldThrowException()
        {
            int ANY_HIGHER_NUMBER = _mockRepo._signalements.Count + 1;
            var response = _signalementController.Details(ANY_HIGHER_NUMBER);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void Test_Details_Id_A_ViewResult_Signalement()
        {
            //Arrange
            const int ANY_ID = 1;

            //Act
            var result = await _signalementController.Details(ANY_ID) as ViewResult;

            //Assert
            Assert.IsAssignableFrom<Signalement>(result.Model);
        }
    }
}
