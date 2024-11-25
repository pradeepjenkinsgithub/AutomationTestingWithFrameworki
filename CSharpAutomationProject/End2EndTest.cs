using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using OpenQA.Selenium.Support.UI;

namespace CSharpAutomationProject
{
    public class End2EndTest
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
            Logon();
        }

        [Test,Category("Smoke")]
        public void Logon()
        {
            String[] ItemsToCheckoutOrExpectedInCheckout = { "iphone X", "Blackberry" };
            String[] ItemsCheckedOutOrActualProductAfterCheckout = new String[2];

            webDriver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            webDriver.FindElement(By.Id("password")).SendKeys("learning");
            webDriver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span[1]/input")).Click();
            webDriver.FindElement(By.XPath("//input[@name='signin']")).Click();
            //When click the sign in it will take sometime so we need the explcit waiting and to prove safetr logon we logged in we are taking
            //partial link text i.e. Checkout, that is visible after logon 
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
           


            //to fetch the product wia tagname
            IList<IWebElement> products = webDriver.FindElements(By.TagName("app-card"));
            foreach (IWebElement productElement in products)
            {
                if (ItemsToCheckoutOrExpectedInCheckout.Contains(productElement.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    productElement.FindElement(By.CssSelector(".card-footer button")).Click();
                }
            }

            webDriver.FindElement(By.PartialLinkText("Checkout")).Click();



            IList<IWebElement> afterCheckoutProducts = webDriver.FindElements(By.CssSelector("h4 a"));
            for(var i = 0; i<=afterCheckoutProducts.Count; i++)
            {
                ItemsCheckedOutOrActualProductAfterCheckout[i] = afterCheckoutProducts[i].Text;
            }

            Assert.AreEqual(ItemsToCheckoutOrExpectedInCheckout, ItemsCheckedOutOrActualProductAfterCheckout);


        }
    }
}
