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
    public class NouvellesUiTests
    {
        [Test]
        public void NavigationButtonIsPresent()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:44301/Nouvelles");

                var copsButton = driver.FindElement(By.Id("NavBarNouvelles"));
                copsButton.Click();
                Assert.IsNotNull(copsButton);
            }
        }

        [Test]
        public void PageContainsAppropriateCategories()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:44301/Nouvelles");

                var natureLabel = driver.FindElement(By.Id("Title"));
                Assert.IsNotNull(natureLabel);

                var sectorLabel = driver.FindElement(By.Id("Text"));
                Assert.IsNotNull(sectorLabel);

                var timeLabel = driver.FindElement(By.Id("link"));
                Assert.IsNotNull(timeLabel);

                var commentLabel = driver.FindElement(By.Id("Status"));
                Assert.IsNotNull(commentLabel);
            }
        }
    }
}