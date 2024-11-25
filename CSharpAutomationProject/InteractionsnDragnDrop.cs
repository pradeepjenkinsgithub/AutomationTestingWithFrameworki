using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using OpenQA.Selenium.Interactions;

namespace CSharpAutomationProject
{
    internal class InteractionsnDragnDrop
    {

        IWebDriver webDriver;
        //Locators like xPath, ID , classname, Tagname
        Actions actions;
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
            webDriver.Url = "https://rahulshettyacademy.com/";
        }

        [Test]

        public void CursoreMoveActions()
        {
            actions = new Actions(webDriver);
            actions.MoveToElement(webDriver.FindElement(By.CssSelector("a.dropdown-toggle"))).Perform();
            Thread.Sleep(1000);
            webDriver.FindElement(By.CssSelector("ul.dropdown-menu li a[href='about-my-mission']")).Click();

        }

        [Test]
        public void DragDropControls()
        {

            webDriver.Url = "https://demoqa.com/droppable";
            actions.DragAndDrop(webDriver.FindElement(By.Id("draggable")),webDriver.FindElement(By.Id("droppable"))).Perform();

        }
    }
}
