using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace CSharpAutomationProject
{
    internal class AlertNPopups
    {

        IWebDriver webDriver;
        //Locators like xPath, ID , classname, Tagname

        [SetUp]
        public void StartBrowser()
        {
            // get methods like click, getURL
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            //new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), " 122.0.6261.129 ");
            webDriver = new ChromeDriver();
            // implicit wait whenever there is early calls and we are here declaring globally
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            webDriver.Manage().Window.Maximize();
            webDriver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
        }

        [Test]
        public void Alert()
        {
            webDriver.FindElement(By.CssSelector("#name")).SendKeys("Pradeep");
            webDriver.FindElement(By.XPath("//input[@id='confirmbtn']")).Click();

            string name = "Pradeep";
            var alertText = webDriver.SwitchTo().Alert().Text;
            webDriver.SwitchTo().Alert().Accept();
            StringAssert.Contains(name, alertText); //Check wheater the name is present in alert what we entered in Text to test

            webDriver.FindElement(By.Id("autocomplete")).SendKeys("ind");
            Thread.Sleep(3000);

            IList<IWebElement> selectorsList = webDriver.FindElements(By.CssSelector(".ui-menu-item div"));
            foreach (IWebElement selector in selectorsList)
            {
                if (selector.Text.Equals("India"))
                {
                    selector.Click();
                }
            }
        }
    }
}
