using NUnit.Framework.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Reflection;

namespace Gotham3.InterfaceTests
{
    public class Tests
    {
        [Test]
        public void TestButtonArePresent()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:44301/");

                var copsButton = driver.FindElement(By.Id("ButtonPolices"));
                copsButton.Click();
                Assert.IsNotNull(copsButton);

                var fireFighterButton = driver.FindElement(By.Id("BouttonPompiers"));
                fireFighterButton.Click();
                Assert.IsNotNull(fireFighterButton);

                var medicButton = driver.FindElement(By.Id("BouttonAmbulances"));
                medicButton.Click();
                Assert.IsNotNull(medicButton);
            }
        }

        [Test]
        public void NavigationBarContainsTitle()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:44301/Signalements");

                var navButton = driver.FindElement(By.Id("NavBarSignalements"));
                navButton.Click();
                Assert.IsNotNull(navButton);

                var newUrl = driver.Url;
                const string EXPECTED_URL = "https://localhost:44301/";
                Assert.AreEqual(EXPECTED_URL, newUrl);
            }
        }

        [Test]
        public void PageContainsAppropriateCategories()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:44301/");

                var natureLabel = driver.FindElement(By.Id("NatureLabel"));
                Assert.IsNotNull(natureLabel);

                var sectorLabel = driver.FindElement(By.Id("SectorLabel"));
                Assert.IsNotNull(sectorLabel);

                var timeLabel = driver.FindElement(By.Id("TimeLabel"));
                Assert.IsNotNull(timeLabel);

                var commentLabel = driver.FindElement(By.Id("CommentLabel"));
                Assert.IsNotNull(commentLabel);
            }
        }
    }
}