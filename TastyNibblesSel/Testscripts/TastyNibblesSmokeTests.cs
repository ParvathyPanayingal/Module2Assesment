using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyNibblesSel.PageObjects;

namespace TastyNibblesSel.Testscripts
{
    internal class TastyNibblesSmokeTests:CoreCodes
    {
        [Test, Order(1)]
        [Category("Smoke Test")]
        public void LogoTest()
        {
            string? currdir = Directory.GetParent(@"../../../")?.FullName;
            string? logfilepath = currdir + "/Logs/log_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day)
            .CreateLogger();


            TastyNibblesHomePage tastyNibblesHomePage = new(driver);
            tastyNibblesHomePage.ClickLogo();
            Thread.Sleep(2000);
            //TakeScreenShot();
            Log.Information("Page reloaded");
            try
            {
                Assert.That(driver?.Url, Is.EqualTo("https://www.tastynibbles.in/"));

                Log.Information("Test passed for Tasty Nibbles logo");

                test = extent.CreateTest("Tasty Nibbles logo Test");
                test.Pass("Tasty Nibbles logo Test success");

            }
            catch (AssertionException ex)
            {
                Log.Error($"Test failed for Tasty Nibbles logo. \n Exception: {ex.Message}");

                test = extent.CreateTest("Tasty Nibbles logo Test");
                test.Fail("Tasty Nibbles logo  Test failed");
                //TakeScreenShot();
            }
            Log.CloseAndFlush();

        }

        [Test, Order(2)]
        [Category("Smoke Test")]
        public void ReadyToEatTest()
        {
            string? currdir = Directory.GetParent(@"../../../")?.FullName;
            string? logfilepath = currdir + "/Logs/log_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day)
            .CreateLogger();

            if (!driver.Url.Equals("https://www.tastynibbles.in/"))
            {
                driver.Navigate().GoToUrl("https://www.tastynibbles.in/");

            }
            TastyNibblesHomePage tastyNibblesHomePage = new(driver);
            tastyNibblesHomePage.ClickReadyToEat();
            Thread.Sleep(2000);
            //TakeScreenShot();
            Log.Information("Page reloaded");
            try
            {
                Assert.That(driver.Title.Contains("Ready to Eat"));

                Log.Information("Test passed for Readt To Eat");

                test = extent.CreateTest("Readt To Eat Test");
                test.Pass("Readt To Eat Test success");

            }
            catch (AssertionException ex)
            {
                Log.Error($"Test failed for Readt To Eat. \n Exception: {ex.Message}");

                test = extent.CreateTest("Readt To Eat Test");
                test.Fail("Read To Eat  Test failed");
                //TakeScreenShot();
            }
            Log.CloseAndFlush();

        }
    }
}
