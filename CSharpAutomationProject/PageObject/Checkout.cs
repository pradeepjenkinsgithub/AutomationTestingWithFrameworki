using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAutomationProject.PageObject
{
    public class Checkout
    {
        private IWebDriver webDriver;
        public Checkout(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList<IWebElement> CheckouCards;

        public IList<IWebElement> GetCards()
        {
            return CheckouCards;
        }

        [FindsBy(How = How.XPath, Using = "//input[@value='Purchase']")]
        private IWebElement signInButton;

        public IWebElement Purchase()
        {
            return signInButton;
        }

        [FindsBy(How = How.XPath, Using = "//button[normalize-space()='Checkout']")]
        private IWebElement checkout;

        public IWebElement _checkout()
        {
            return checkout;
        }


        ////input[@id='country']
        [FindsBy(How = How.XPath, Using = "//input[@id='country']")]
        private IWebElement country;

        public IWebElement Country()
        {
            return country;
        }

        [FindsBy(How = How.XPath, Using = "//label[@for='checkbox2']")]
        private IWebElement checkbox2;

        public IWebElement TermsNConditions()
        {
            return checkbox2;
        }


        //".//a[normalize-space()='Poland']"
        [FindsBy(How = How.XPath, Using = ".//a[normalize-space()='Poland']")]
        private IList<IWebElement> EnterCountry;

        public IList<IWebElement> CountryName()
        {
            return EnterCountry;
        }
    }

}
