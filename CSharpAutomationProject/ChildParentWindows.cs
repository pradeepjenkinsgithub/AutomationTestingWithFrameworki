using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace CSharpAutomationProject
{
    public class ChildParentWindows
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
            webDriver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }

        [Test]
        public void WindowHandle()
        {
            webDriver.FindElement(By.ClassName("blinkingText")).Click();


            var parentWindowName = webDriver.WindowHandles[1];
            var childWindowName = webDriver.CurrentWindowHandle;

            string FirstTabInBrowser = parentWindowName;
            string secondTabInBrowser = childWindowName;
        }

    }
}
