using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using Serilog;
using TastyNibblesSel.Utilities;

namespace TastyNibblesSel
{
    internal class CoreCodes
    {

        public Dictionary<string, string> properties;
        public static IWebDriver driver;

        public ExtentReports extent;
        ExtentSparkReporter sparkReporter;
        public ExtentTest test;

        public void ReadConfigSettings()
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

        public static void ScrollIntoView(IWebDriver driver, IWebElement? element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
        public bool CheckLinkStatus(string url)
        {
            try
            {
                var request = (System.Net.HttpWebRequest)
                    System.Net.WebRequest.Create(url);

                request.Method = "HEAD";
                using (var response = request.GetResponse())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        [OneTimeSetUp]
        public void InitializeBrowser()
        {
            string currdir = Directory.GetParent(@"../../../").FullName;

            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currdir + "/ExtentReports/extent-report-"
                + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");

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

        [OneTimeTearDown]
        public void Cleanup()
        {
            driver?.Quit();
            extent.Flush();
        }
       

        protected void LogTestResult(string testName, string result, string errorMessage = null)
        {
            Log.Information(result);

            test = extent.CreateTest(testName);

            if (errorMessage == null)
            {
                test.Pass(result);
            }
            else
            {//taking screenshot while there is error
                string filepath = ScreenShots.TakeScreenShot(driver);

                test.AddScreenCaptureFromPath(filepath);

                Log.Error($"Test failed for {testName}. \n Exception: \n {errorMessage}");
                test.Fail(result);
            }
        }
    }
}