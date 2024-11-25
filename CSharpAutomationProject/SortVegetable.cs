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
using System.Collections;

namespace CSharpAutomationProject
{
    public class SortVegetable
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
            webDriver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
        }

        [Test]
        public void  SortTable()
        {
            SelectElement cmbPageCount = new SelectElement(webDriver.FindElement(By.CssSelector("select[id='page-menu']")));
            cmbPageCount.SelectByValue("20");

            IList<IWebElement> BeforeSort = webDriver.FindElements(By.XPath("//tbody//tr/td[1]"));
            ArrayList BeforesortedElements = new ArrayList();
            foreach (var item in BeforeSort)
            {
                BeforesortedElements.Add(item.Text);
                TestContext.Progress.WriteLine(item.Text);
            }
            BeforesortedElements.Sort();

            //table[@class='table table-bordered']
            IList<IWebElement> afterSort = webDriver.FindElements(By.XPath("//tbody//tr/td[1]"));
            foreach (var vegg in afterSort)
            {
                BeforesortedElements.Add(vegg.Text);
                TestContext.Progress.WriteLine(vegg.Text);
            }


            webDriver.FindElement(By.XPath("//tr/th[1]")).Click();

           IList<IWebElement> AfterSort = webDriver.FindElements(By.XPath("//tbody//tr/td[1]"));
           // ArrayList AfterSorttedElements = new ArrayList();
           // foreach (var item in AfterSort)
           // {
           //     AfterSorttedElements.Add(item.GetAttribute);
           // }
           // AfterSorttedElements.Sort();


           //bool isSorted = BeforesortedElements.Equals(AfterSorttedElements);
           // Assert.That(isSorted, Is.True);
        }
    }
}
