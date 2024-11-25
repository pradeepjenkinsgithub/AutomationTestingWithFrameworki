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
    public class ECommerceTestCaseSourceThreadSafe : BaseThreadSafe
    {
        //Here looking at class ECommerceTestcase multiple inputs passing
        //handle with Testcasesource

        //passing the username password , or any data like products, u can pass in testcase annotation 
        //if want to pass multiple test case data in two test case and more in more testcases, use TestCaseSource as below

        //Implement in 2 ways as shown below

        //[Test]
        //[TestCaseSource("ReadTestCasedata")]  // takes function as arguments

        // Or

        [Test, TestCaseSource(nameof(ReadTestCasedata))]

        [Parallelizable(ParallelScope.All)]
        public void Logon(string username, string password)
        {
            String[] ItemsToCheckoutOrExpectedInCheckout = { "iphone X", "Blackberry" };
            String[] ItemsCheckedOutOrActualProductAfterCheckout = new string[2];

            Login login = new Login(webDriver.Value);

            //**************** More Simplified
            Product _products = login.LoginPageAllActions(username, password);
            _products.ExplicitWaitPage();
            IList<IWebElement> products = _products.GetProducts();

            //*****************COMPARE OLD CODE WITH END2END Class*******************************************************//
            //**********No driver so List is calling FindElement from List not from driver
            //********** Create a By variable for cardtitle (list of elements)
            //*********  Added below how to handle with method created in product page


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

        private static IEnumerable<TestCaseData> ReadTestCasedata()
        {

            // You cant return multiple data
            // so create Yield return which run only once

            //return new TestCaseData("rahulshettyacademy", "learning");
            //return new TestCaseData("rahulshettyacademy1", "learning1");
            //return new TestCaseData("rahulshettyacademy2", "learning2");

            yield return new TestCaseData("rahulshettyacademy", "learning");
            yield return new TestCaseData("rahulshettyacademy1", "learning1");
            yield return new TestCaseData("rahulshettyacademy2", "learning3");
        }
    }
}
