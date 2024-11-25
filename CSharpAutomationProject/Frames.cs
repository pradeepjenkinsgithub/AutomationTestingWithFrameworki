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
    internal class Frames
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
        public void FramesTest()
        {
            //scrolling to the frame in page
            var tillFrames = webDriver.FindElement(By.Id("courses-iframe"));
            IJavaScriptExecutor scrollJS = (IJavaScriptExecutor)webDriver;
            scrollJS.ExecuteScript("arguments[0].scrollIntoView(true);",tillFrames);

            //from Parent webpage to frame page
            webDriver.SwitchTo().Frame("courses-iframe");            
            webDriver.FindElement(By.LinkText("All Access Plan")).Click();

            //from frame page to parent webpage
            webDriver.SwitchTo().DefaultContent();
        }
    }
}
