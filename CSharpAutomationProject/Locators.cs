using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using static System.Net.Mime.MediaTypeNames;
using OpenQA.Selenium.Support.UI;

namespace CSharpAutomationProject
{

    public class Locators
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
        public void Test1()
        {
            //For textbox or anything copy paste use XPath
            webDriver.FindElement(By.Id("username")).SendKeys("rahulshetty");
            webDriver.FindElement(By.Id("password")).SendKeys("123456");

            //css attribute
            //tagname[attribute='value']
            //for button use CSS selector
            webDriver.FindElement(By.CssSelector("input[name=\"signin\"]")).Click();

            //XPath 
            // //tagname[@attribute = 'value']
            webDriver.FindElement(By.XPath("//input[@name='signin']")).Click();

            //To capture the text
            //Thread.Sleep(3000);
            // implicit we have 5 seconds but for this we want 8 seconds so we declare EXPLICIT Time out
            //Here the condition is sign in disappear after the alert and we have to wait until it appears back and it will wait for 8 seconds maximum            
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(webDriver.FindElement(By.Id("signInBtn")), "Sign In"));

            string errorMessage = webDriver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errorMessage);

            #region Implicit and Explicit and Link Text

            //LinkText

            IWebElement webElement = webDriver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            string linkTextLink =  webElement.GetAttribute("href");

            string expectedLink = "https://rahulshettyacademy.com/documents-request";
            Assert.AreEqual(expectedLink,linkTextLink);


            // When u dont have ID, Name/ Class then we have to traverse Parent-Child or Child-Parent to find the element or control

            //exmaple - XPath consider we dont have the for checkbox ID/C/N then we traverse from top to down

            webDriver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span[1]/input")).Click();



            // When u dont have ID, Name/ Class then we have to traverse Parent-Child or Child-Parent to find the element or control

            //exmaple - CSSSelector consider we dont have the for checkbox ID/C/N then we traverse from top to down

            #endregion

            #region Radio Button

            IList<IWebElement> listOfNoOfRadioButtons = webDriver.FindElements(By.XPath("//input[@type='radio']"));
            foreach (var element in listOfNoOfRadioButtons)
            {
                var radio = element.GetAttribute("value").Equals("user") ;
                if(radio == true)
                    element.Click();
            }

            //webDriver.Close();

            WebDriverWait okayClick = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            okayClick.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("okayBtn")));

            // I want to check if the user radio is selected 
            webDriver.FindElement(By.XPath("//button[@id='okayBtn']")).Click();

        

            bool isSelected = webDriver.FindElement(By.Id("usertype")).Selected;

            Assert.That(isSelected,Is.True);

            Thread.Sleep(100);

            webDriver.Close();
            #endregion
        }

    }
}
