using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.PageObjects;

namespace CSharpAutomationProject.PageObject
{
    public class Product
    {
        IWebDriver webDriver;
        By cardTitle = By.CssSelector(".card-title a");
        By cardFooter = By.CssSelector(".card-footer button");
        public Product(IWebDriver webDriver)   
        {
                this.webDriver = webDriver;
                PageFactory.InitElements(webDriver, this);       
        }
  

        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> Products;

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement PartialLinkText;

        public IList<IWebElement> GetProducts()
        {
            return Products;
        }

        public By GetCardTitle()
        {
            return cardTitle;
        }

        public By GetCardFooter()
        {
            return cardFooter;
        }

        public IWebElement GetCheckout()
        {
            return PartialLinkText;            
        }

        public Checkout CheckOutProducts()
        {
            PartialLinkText.Click();

            return new Checkout(webDriver);
        }

        public void ExplicitWaitPage()
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
        }
    }

}
