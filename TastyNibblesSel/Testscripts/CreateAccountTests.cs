using OpenQA.Selenium;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyNibblesSel.PageObjects;
using TastyNibblesSel.Utilities;

namespace TastyNibblesSel.Testscripts
{
    internal class CreateAccountTests : CoreCodes
    {

        [Test]
        [Category("Regression Test")]
        public void BuyProductTest()
        {
            test = extent.CreateTest("Create Account Test");

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
            CreateAccountPage createAccountPage = new(driver);
            AccountPage accountPage = new(driver);
            Log.Information("Create Account Test Started");
            tastyNibblesHomePage.ClickAccount();
            Thread.Sleep(3000);
            accountPage.ClickCreateAccLink();
            Thread.Sleep(2000);

            string? excelFilePath = currdir + "/TestData/InputData.xlsx";
            string? sheetName = "CreateAccountData";

            List<CreateAccountExcelData> excelDataList = ExcelUtils.ReadCreateAccExcelData(excelFilePath, sheetName);

            foreach (var excelData in excelDataList)
            {
                string? firstName = excelData?.FirstName;
                string? lastName = excelData?.LastName;
                string? email = excelData?.Email;
                string? password = excelData?.Password;
                createAccountPage.EnterFirstName(firstName);
                createAccountPage.EnterLastName(lastName);
                createAccountPage.EnterEmail(email);
                createAccountPage.EnterPassword(password);


                


                IWebElement CreateButton = driver.FindElement(By.XPath("//p/input[@value='Create']"));
                try
                {

                    Assert.That(CreateButton.GetAttribute("value").Equals("Create")
                        , $"Test failed for Create Account Test");



                    Log.Information("Test passed for Create Account ");
                    
                    string filepath = ScreenShots.TakeScreenShot(driver);
                    test.AddScreenCaptureFromPath(filepath);
                    test.Pass("Create Account Test success");
                    test.Info("Test passed for Create Account ");
                }
                catch (AssertionException ex)
                {
                    Log.Error($"Test failed for Create Account. \n Exception: {ex.Message}");
                    test = extent.CreateTest("Create Account Test");
                    test.Fail("Create Account Test failed");
                    string filepath = ScreenShots.TakeScreenShot(driver);

                    test.AddScreenCaptureFromPath(filepath);
                }

            }
            Log.CloseAndFlush();
        }
    }
}

