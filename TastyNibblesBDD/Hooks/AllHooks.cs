using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using Serilog;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium.Edge;

namespace TastyNibblesBDD.Hooks
{
    [Binding]
    public sealed class AllHooks
    {
        public static IWebDriver? driver;

        public static Dictionary<string, string> properties;

        public static ExtentReports extent;
        static ExtentSparkReporter sparkReporter;
       

        
        public static void ReadConfigSettings()
        {
            properties = new Dictionary<string, string>();

            string currdir = Directory.GetParent(@"../../../").FullName;
            string fileName = currdir + "/configsettings/config.properties";
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line.Contains("="))
                {
                    string[] parts = line.Split('=');
                    string key = parts[0].Trim();
                    string value = parts[1].Trim();
                    properties[key] = value;
                }
            }
        }

        [BeforeFeature]
        public static void InitializeBrowser()
        {
            string currdir = Directory.GetParent(@"../../../").FullName;

            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currdir + "/ExtentReports/extent-report-"
                + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html");

            extent.AttachReporter(sparkReporter);

            ReadConfigSettings();
            if (properties["﻿browser"].ToLower() == "chrome")
            {
                driver = new ChromeDriver();
            }
            else if (properties["﻿browser"].ToLower() == "edge")
            {
                driver = new EdgeDriver();
            }
            driver.Url = properties["baseUrl"];
            driver.Manage().Window.Maximize();

        }

        [BeforeFeature]
        public static void LogFileCreation()
        {
            string? currdir = Directory.GetParent(@"../../../")?.FullName;
            string? logfilepath = currdir + "/Logs/log_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day)
            .CreateLogger();

        }

        [AfterFeature]
        public static void CleanUp()
        {
            driver?.Quit();
            extent.Flush();
        }
        [AfterScenario]
        public static void NavigateToHomePage()
        {
            driver?.Navigate().GoToUrl("https://www.tastynibbles.in/");
        }

    }
}