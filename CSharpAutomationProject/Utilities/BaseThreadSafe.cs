using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using NUnit.Framework.Internal;
using System.Configuration;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using ICSharpCode.SharpZipLib.Zip;
using NUnit.Framework.Interfaces;

namespace CSharpAutomationProject.Utilities
{
    public class BaseThreadSafe
    {
        public ThreadLocal<IWebDriver> webDriver;
        public String? browserName;
        public ExtentReports extentReports;
        public ExtentTest TestResult;

        [OneTimeSetUp] // only run once for all test
        public void Setup()
        {
            string? MainPath = Environment.CurrentDirectory;
            string? ProjectPath = Directory.GetParent(MainPath).Parent.Parent.FullName;
            string? reportPath = ProjectPath + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extentReports = new ExtentReports();
            extentReports.AttachReporter(htmlReporter);
            extentReports.AddSystemInfo("HostName", "Local host");
            extentReports.AddSystemInfo("Environment", "QA");
            extentReports.AddSystemInfo("Pradeep G", "LearningPath");

        }
        //Locators like xPath, ID , classname, Tagname
        [SetUp] // this will call before each test
        public void StartBrowser()
        {
            webDriver = new ThreadLocal<IWebDriver>();

            TestResult = extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
            //Configuration

            //Run from CLI Command
            //dotnet test CSharpAutomationProject.csproj -- filter=Smoke  -- TestRunParameters.Parameter\(name=\"browserName\":value="Chrome\"\)
            browserName = TestContext.Parameters["browserName"];

            if (browserName == null)
            {

                browserName = "Chrome";

                InitBrowser(browserName);

                webDriver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                webDriver.Value.Manage().Window.Maximize();
                webDriver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";


            }
        }
        public void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    webDriver.Value = new FirefoxDriver();
                    break;
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

                    webDriver.Value = new ChromeDriver();
                    break;
                case "Edge":
                    webDriver.Value = new EdgeDriver();
                    break;
            }
        }

        public IWebDriver getDriver()

        {
            return webDriver.Value;
        }

        public static JSONReader DataParserFromJSON()
        {
            return new JSONReader();
        }

        [TearDown]

        public void CloseBrowser()
        {
           var isStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var statusTrack = TestContext.CurrentContext.Result.StackTrace;
            if (isStatus == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                DateTime dateTime = DateTime.Now;
                string timeStamp = "ScreenShot" + "" + dateTime.ToString("h_mm_ss") + ".png";
                TestResult.Fail("Test Failed", ScreenShot(webDriver.Value, timeStamp));
                TestResult.Log(Status.Fail,"Failed: " + statusTrack);
            }
            else if (isStatus == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                TestResult.Pass("");
            }
            extentReports.Flush();
            webDriver.Value.Quit();
        }


        public MediaEntityModelProvider ScreenShot(IWebDriver webDriver, string ScreenShots)
        {
            var screenShot = (ITakesScreenshot)webDriver;
            var screenShotName = screenShot.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShotName, ScreenShots).Build();
        }
    }
}
