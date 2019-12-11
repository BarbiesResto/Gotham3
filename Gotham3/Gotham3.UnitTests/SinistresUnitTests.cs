using Gotham3.domain;
using Gotham3.persistence.Mocks;
using Gotham3.web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Gotham3.UnitTests
{
    public class SinistresUnitTests
    {
        private SinistresController _sinistresController;
        private MockSinistresRepository _mockRepo;

        public SinistresUnitTests() 
        {
            //Arrange
            _mockRepo = new MockSinistresRepository();
            _sinistresController = new SinistresController(_mockRepo);
        }

        //Index
        [Fact]
        public async void Test_Index_Returns_A_ViewResult_Sinistre()
        {
            //Arrange

            //Act
            var result = await _sinistresController.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void Test_Index_Model_Is_An_Enumerable_Model_of_Sinistres()
        {
            //Arrange
            //Act
            var result = await _sinistresController.Index() as ViewResult;

            //Assert
            Assert.IsAssignableFrom<IQueryable<Sinistre>>(result.Model);
        }

        [Fact]
        public async Task Test_Index_Model_Contains_Sinistres()
        {
            var result = await _sinistresController.Index() as ViewResult;

            var model = result.Model as IQueryable<Sinistre>;
            var exepctedNumber = _mockRepo._sinistres.Count;
            Assert.Equal(exepctedNumber, model.Count());
        }

        //Delete
        [Fact]
        public async Task Test_Delete_Should_DeleteFromList()
        {
            const int FIRST_ID = 0;
            int EXPECTED_NUMBER = _mockRepo._sinistres.Count - 1;
            await _sinistresController.DeleteConfirmed(FIRST_ID);

            Assert.Equal(EXPECTED_NUMBER, _mockRepo._sinistres.Count);
        }

        [Fact]
        public void Test_Delete_WithNullId_ShouldThrowException()
        {
            var response = _sinistresController.Delete(null);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        //Details
        [Fact]
        public void Test_Details_WithNullId_ShouldThrowException()
        {
            var response = _sinistresController.Details(null);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public void Test_Details_WithHigherIdThanSinistres_ShouldThrowException()
        {
            int ANY_HIGHER_NUMBER = _mockRepo._sinistres.Count + 1;
            var response = _sinistresController.Details(ANY_HIGHER_NUMBER);
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void Test_Details_Id_A_ViewResult_Sinistre()
        {
            //Arrange
            const int ANY_ID = 1;

            //Act
            var result = await _sinistresController.Details(ANY_ID) as ViewResult;

            //Assert
            Assert.IsAssignableFrom<Sinistre>(result.Model);
        }
    }
}
