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
    public class CapsulesInformativesUnitTests
    {
        private CapsuleInformativesController _capsuleInformativesController;
        private MockCapsulesInformativesRepository _mockRepo;

        public CapsulesInformativesUnitTests()
        {
            //Arrange
            _mockRepo = new MockCapsulesInformativesRepository();
            _capsuleInformativesController = new CapsuleInformativesController(_mockRepo);
        }

        //Index
        [Fact]
        public async void Test_Index_Returns_A_ViewResult_Signalement()
        {
            //Arrange

            //Act
            var result = await _capsuleInformativesController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Test_Index_Model_Is_An_Enumerable_Model_of_Signalements()
        {
            //Arrange
            //Act
            var result = await _capsuleInformativesController.Index() as ViewResult;

            //Assert
            Assert.IsAssignableFrom<IQueryable<CapsuleInformative>>(result.Model);
        }

        [Fact]
        public async Task Test_Index_Model_Contains_Signalements()
        {
            var result = await _capsuleInformativesController.Index() as ViewResult;

            var model = result.Model as IQueryable<CapsuleInformative>;
            var exepctedNumber = _mockRepo._capsuleInformatives.Count;
            Assert.Equal(exepctedNumber, model.Count());
        }

        //Delete
        [Fact]
        public async Task Test_Delete_Should_DeleteFromList()
        {
            const int FIRST_ID = 0;
            int EXPECTED_NUMER = _mockRepo._capsuleInformatives.Count - 1;
            await _capsuleInformativesController.DeleteConfirmed(FIRST_ID);

            Assert.Equal(EXPECTED_NUMER, _mockRepo._capsuleInformatives.Count);
        }

        [Fact]
        public void Test_Delete_WithNullId_ShouldThrowException()
        {
            var response = _capsuleInformativesController.Delete(null);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        //Details
        [Fact]
        public void Test_Details_WithNullId_ShouldThrowException()
        {
            var response = _capsuleInformativesController.Details(null);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public void Test_Details_WithHigherIdThanProjects_ShouldThrowException()
        {
            int ANY_HIGHER_NUMBER = _mockRepo._capsuleInformatives.Count + 1;
            var response = _capsuleInformativesController.Details(ANY_HIGHER_NUMBER);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void Test_Details_Id_A_ViewResult_Signalement()
        {
            //Arrange
            const int ANY_ID = 1;

            //Act
            var result = await _capsuleInformativesController.Details(ANY_ID) as ViewResult;

            //Assert
            Assert.IsAssignableFrom<CapsuleInformative>(result.Model);
        }
    }
}
