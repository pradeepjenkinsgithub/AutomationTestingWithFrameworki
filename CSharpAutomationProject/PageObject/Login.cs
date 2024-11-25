using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAutomationProject.PageObject
{
    public class Login
    {
        //webDriver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");

        private IWebDriver driver;

        public Login(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        // Creating page objects for FindElements
        [FindsBy(How=How.Id,Using = "username")]
        private IWebElement userName;

        public IWebElement GetUserName()
        {
            return userName;
        }


        [FindsBy(How =How.Id,Using = "password")]
        private IWebElement password;

        public IWebElement GetPasssword()
        {
            return password;
        }

        [FindsBy(How = How.XPath,Using = "//div[@class='form-group'][5]/label/span[1]/input")]
        private IWebElement checkbox;
        public IWebElement ClickCheckControl()
        {
            return checkbox;
        }


        [FindsBy(How = How.XPath, Using = "//input[@name='signin']")]
        private IWebElement signIn;


         public Product LoginPageAllActions(string Username, string Password)
        {
            userName.SendKeys(Username);
            password.SendKeys(Password);
            checkbox.Click();
            signIn.Click();
            return new Product(driver);
        }

    }
}
