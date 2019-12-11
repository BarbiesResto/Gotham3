using NUnit.Framework.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Reflection;

namespace Gotham3.InterfaceTests
{
    public class AlerteUiTests
    {
        [Test]
        public void NavigationBarContainsTitle()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:44301/");

                var navButton = driver.FindElement(By.Id("NavBarAlertes"));
                navButton.Click();
                Assert.IsNotNull(navButton);

                var newUrl = driver.Url;
                const string EXPECTED_URL = "https://localhost:44301/Alertes";
                Assert.AreEqual(EXPECTED_URL, newUrl);
            }
        }

        [Test]
        public void PageContainsAppropriateCategories()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:44301/Alertes");

                var natureLabel = driver.FindElement(By.Id("NatureLabel"));
                Assert.IsNotNull(natureLabel);

                var sectorLabel = driver.FindElement(By.Id("SectorLabel"));
                Assert.IsNotNull(sectorLabel);

                var riskLabel = driver.FindElement(By.Id("RiskLabel"));
                Assert.IsNotNull(riskLabel);

                var RessourcetLabel = driver.FindElement(By.Id("RessourceLabel"));
                Assert.IsNotNull(RessourcetLabel);

                var adviceLabel = driver.FindElement(By.Id("AdviceLabel"));
                Assert.IsNotNull(adviceLabel);

                var publishedLabel = driver.FindElement(By.Id("PublishedLabel"));
                Assert.IsNotNull(publishedLabel);
            }
        }
    }
}
