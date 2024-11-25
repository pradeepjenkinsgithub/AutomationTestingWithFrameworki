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
using CSharpAutomationProject.PageObject;
using CSharpAutomationProject.Utilities;
using NUnit.Framework;

namespace CSharpAutomationProject
{
    public class ECommerceJSON : BaseThreadSafe
    {

        
        [Test, TestCaseSource(nameof(ReadTestCasedata)),Category("Regression")]

        [Parallelizable(ParallelScope.All)]
        public void Logon(string username, string password, string[] Products)
        {
            String[] ItemsToCheckoutOrExpectedInCheckout = Products; //{ "iphone X", "Blackberry" };
            String[] ItemsCheckedOutOrActualProductAfterCheckout = new string[2];

            Login login = new Login(getDriver());
            Product _products = login.LoginPageAllActions(username, password);
            _products.ExplicitWaitPage();

            IList<IWebElement> products = _products.GetProducts();
            foreach (IWebElement productElement in products)
            {
                if (ItemsToCheckoutOrExpectedInCheckout.Contains(productElement.FindElement(_products.GetCardTitle()).Text)) //By.CssSelector(".card-title a")).Text))
                {
                    productElement.FindElement(_products.GetCardFooter()).Click(); //By.CssSelector(".card-footer button")).Click();
                }
            }

            Checkout _checkout = _products.CheckOutProducts();
            IList<IWebElement> afterCheckoutProducts = _checkout.GetCards();// webDriver.FindElements(By.CssSelector("h4 a"));
            for (var i = 0; i < afterCheckoutProducts.Count; i++)
            {
                ItemsCheckedOutOrActualProductAfterCheckout[i] = afterCheckoutProducts[i].Text;
            }
            _checkout._checkout().Click();
            _checkout.Country().SendKeys("Pol");
            Thread.Sleep(3000);
            IList<IWebElement> selectorsList = _checkout.CountryName();
            foreach (IWebElement selector in selectorsList)
            {
                if (selector.Text.Equals("Poland"))
                {
                    selector.Click();
                }
            }

            _checkout.TermsNConditions().Click();
            _checkout.Purchase().Click();
        }

        [Test,Category("Smoke")]
        public void LocatorsIdentification()

        {

            webDriver.Value.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            webDriver.Value.FindElement(By.Id("username")).Clear();
            webDriver.Value.FindElement(By.Id("username")).SendKeys("rahulshetty");
            webDriver.Value.FindElement(By.Name("password")).SendKeys("123456");
            //css selector & xpath
            //  tagname[attribute ='value']
            //    #id  #terms  - class name -> css .classname
            //    driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();

            //    //tagName[@attribute = 'value']

            // CSS - .text-info span:nth-child(1) input
            //xpath - //label[@class='text-info']/span/input

            webDriver.Value.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();

            webDriver.Value.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            WebDriverWait wait = new WebDriverWait(webDriver.Value, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
           .TextToBePresentInElementValue(webDriver.Value.FindElement(By.Id("signInBtn")), "Sign In"));

            String errorMessage = webDriver.Value.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errorMessage);


        }


        private static IEnumerable<TestCaseData> ReadTestCasedata()
        {
            //Without JSON reader

            //yield return new TestCaseData("rahulshettyacademy", "learning");
            //yield return new TestCaseData("rahulshettyacademy1", "learning1");
            //yield return new TestCaseData("rahulshettyacademy2", "learning3");


            //With JSON reader from input file

            yield return new TestCaseData(DataParserFromJSON().ExtractJSON("username"), DataParserFromJSON().ExtractJSON("password"), DataParserFromJSON().ExtractArrayJSON("products"));
            yield return new TestCaseData(DataParserFromJSON().ExtractJSON("username_wrong"), DataParserFromJSON().ExtractJSON("password_wrong"), DataParserFromJSON().ExtractArrayJSON("products"));

        }
    }
}
