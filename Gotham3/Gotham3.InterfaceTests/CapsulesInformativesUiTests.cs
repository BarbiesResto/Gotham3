using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Gotham3.InterfaceTests
{
    public class CapsulesInformativesUiTests
    {
        [Test]
        public void TestButtonArePresent()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:44301/CapsuleInformatives");

                var EditButton = driver.FindElement(By.Id("EditButton"));
                EditButton.Click();
                Assert.IsNotNull(EditButton);

                var DeleteButton = driver.FindElement(By.Id("DeleteButton"));
                DeleteButton.Click();
                Assert.IsNotNull(DeleteButton);

                var PublierButton = driver.FindElement(By.Id("PublierButton"));
                PublierButton.Click();
                Assert.IsNotNull(PublierButton);

                var DepublierButton = driver.FindElement(By.Id("DepublierButton"));
                DepublierButton.Click();
                Assert.IsNotNull(DepublierButton);
            }
        }

        [Test]
        public void NavigationBarContainsTitle()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:44301/");

                var navButton = driver.FindElement(By.Id("NavBarCapsules"));
                navButton.Click();
                Assert.IsNotNull(navButton);

                var newUrl = driver.Url;
                const string EXPECTED_URL = "https://localhost:44301/CapsuleInformatives";
                Assert.AreEqual(EXPECTED_URL, newUrl);
            }
        }

        [Test]
        public void PageContainsAppropriateCategories()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:44301/CapsuleInformatives");

                var titleLabel = driver.FindElement(By.Id("Titre"));
                Assert.IsNotNull(titleLabel);

                var descriptionLabel = driver.FindElement(By.Id("Description"));
                Assert.IsNotNull(descriptionLabel);

                var linkLabel = driver.FindElement(By.Id("Lien"));
                Assert.IsNotNull(linkLabel);

                var statusLabel = driver.FindElement(By.Id("Status"));
                Assert.IsNotNull(statusLabel);
            }
        }
    }
}
