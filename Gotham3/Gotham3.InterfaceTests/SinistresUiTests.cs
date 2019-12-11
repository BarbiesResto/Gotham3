using NUnit.Framework.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;

namespace Gotham3.InterfaceTests
{
    public class SinistresUiTests
    {
        [Test]
        public void TestButtonsArePresent()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                
                driver.Navigate().GoToUrl(@"https://localhost:11860/Sinistres");

                var addButton = driver.FindElement(By.Id("AddButton"));
                addButton.Click();
                Assert.IsNotNull(addButton);

                var deleteButton = driver.FindElement(By.ClassName("DeleteButton"));
                deleteButton.Click();
                Assert.IsNotNull(deleteButton);

                var updateButton = driver.FindElement(By.ClassName("UpdateButton"));
                updateButton.Click();
                Assert.IsNotNull(updateButton);

                var publishButton = driver.FindElement(By.ClassName("PublishButton"));
                publishButton.Click();
                Assert.IsNotNull(publishButton);
            }
        }

        [Test]
        public void NavigationBarContainsTitle()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:11860/");

                var navButton = driver.FindElement(By.Id("NavBarSinistres"));
                navButton.Click();
                Assert.IsNotNull(navButton);

                var newUrl = driver.Url;
                const string EXPECTED_URL = "https://localhost:11860/Sinistres";
                Assert.AreEqual(EXPECTED_URL, newUrl);
            }
        }

        [Test]
        public void PageContainsAppropriateCategories()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:11860/");

                var natureLabel = driver.FindElement(By.Id("TitleLabel"));
                Assert.IsNotNull(natureLabel);

                var sectorLabel = driver.FindElement(By.Id("DescriptionLabel"));
                Assert.IsNotNull(sectorLabel);

                var timeLabel = driver.FindElement(By.Id("MonthLabel"));
                Assert.IsNotNull(timeLabel);

                var commentLabel = driver.FindElement(By.Id("StatusLabel"));
                Assert.IsNotNull(commentLabel);
            }
        }
    }
}