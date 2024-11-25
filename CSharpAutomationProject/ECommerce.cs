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

namespace CSharpAutomationProject
{
    public class ECommerce :Base
    {
        
        [Test]
        
        public void Logon()
        {
            String[] ItemsToCheckoutOrExpectedInCheckout = { "iphone X", "Blackberry" };
            String[] ItemsCheckedOutOrActualProductAfterCheckout = new string[2];

            Login login = new Login(webDriver);

            //**************** More Simplified
            Product _products = login.LoginPageAllActions("rahulshettyacademy", "learning");
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
    }
}
