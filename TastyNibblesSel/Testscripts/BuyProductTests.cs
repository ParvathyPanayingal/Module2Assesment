using TastyNibblesSel;
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
    internal class BuyProductTests:CoreCodes
    {
        [Test]
        [Category("Regression Test")]
        public void BuyProductTest()
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
            PicklesResultPage picklesResultPage= new(driver);
            ProductDetailsPage productDetailsPage = new(driver);
            CheckOutPage checkOutPage = new(driver);
            Log.Information("Buy Product Test Started");
            
            Thread.Sleep(2000);

            string? excelFilePath = currdir + "/TestData/InputData.xlsx";
            string? sheetName = "SearchData";

            List<BuyProductExcelData> excelDataList = ExcelUtils.ReadSearchExcelData(excelFilePath, sheetName);

            foreach (var excelData in excelDataList)
            {
                string? searchText = excelData?.SearchText;
                string? contactNo = excelData?.ContactNo;
                string? firstName = excelData?.FirstName;
                string? lastName = excelData?.LastName;
                string? address = excelData?.Address;
                string? city = excelData?.City;
                string? pincode = excelData?.Pincode;
                tastyNibblesHomePage.ClickSearchIcon();
                tastyNibblesHomePage.TypeSearchInput(searchText);
                TakeScreenShot();
                Log.Information("Searched for pickle");

                picklesResultPage.ClickProduct();
                Thread.Sleep(3000);

                try
                {
                    IWebElement productLabel = driver.FindElement(By.XPath("((//div[@class=\"grid-item__meta-main\"])/div)[6]"));
                    Assert.IsTrue(driver?.FindElement(By.XPath(
                        "(//div[@class='page-width'])/div/h1")).Text.Equals(productLabel.Text)
                        , $"Test failed for Buy Product Test");



                    Log.Information("Test passed for select Product ");
                    test = extent.CreateTest("select Product Test");
                    test.Pass("select Product Test success");

                }
                catch (AssertionException ex)
                {
                    Log.Error($"Test failed for select Product. \n Exception: {ex.Message}");
                    test = extent.CreateTest("select ProductTest");
                    test.Fail("select Product Test failed");
                    TakeScreenShot();
                }
                Thread.Sleep(3000);
                productDetailsPage.ClickAddToCartButton();
                Thread.Sleep(2000);
                productDetailsPage.ClickCheckOutButton();
                Thread.Sleep(3000);

                try
                {
                    Assert.That(driver.Title.Contains("Checkout"));

                    Log.Information("Test passed for Checkout Page ");
                    test = extent.CreateTest("Checkout Page Test");
                    test.Pass("Checkout Page Test success");

                }
                catch (AssertionException ex)
                {
                    Log.Error($"Test failed for Checkout Page. \n Exception: {ex.Message}");
                    test = extent.CreateTest("Checkout Page Test");
                    test.Fail("Checkout Page Test failed");
                    TakeScreenShot();
                }
                checkOutPage.EnterContact(contactNo);
                checkOutPage.EnterFirstName(firstName);
                checkOutPage.EnterLastName(lastName);
                checkOutPage.EnterAddress(address);
                checkOutPage.EnterCity(city);
                checkOutPage.SelectState();
                checkOutPage.EnterPincode(pincode);
                checkOutPage.EnterPhoneNo(contactNo);
                Thread.Sleep(3000);

                IWebElement PayNowButton = driver.FindElement(By.XPath("//div[@id='pay-button-container']/div/div/button/span/span"));
                    try
                {
                    
                    Assert.That(PayNowButton.Text.Contains("Pay")
                        , $"Test failed for Buy Product Test");



                    Log.Information("Test passed for Buy Product ");
                    test = extent.CreateTest("Buy Product Test");
                    test.Pass("Buy Product Test success");

                }
                catch (AssertionException ex)
                {
                    Log.Error($"Test failed for select Product. \n Exception: {ex.Message}");
                    test = extent.CreateTest("Buy Product Test");
                    test.Fail("Buy Product Test failed");
                    TakeScreenShot();
                }

            }
            Log.CloseAndFlush();
        }
    }
}
