using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog;
using SwaglabSel.Helpers;
using SwaglabSel.Pages;
using SwaglabSel.Utilities;


namespace SwaglabSel.Tests
{
    [TestFixture]
    
    internal class BuyProductTests:CoreCodes
    {
        [Test]
        [Category("Regression Test")]
        public void BuyingProduct()
        {
            test = extent.CreateTest("Buying Product Test");
            string label;

            //logs
            string? currdir = Directory.GetParent(@"../../../")?.FullName;
            string? logfilepath = currdir + "/Logs/log_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilepath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            //wait
            DefaultWait<IWebDriver> wait;
            wait = new DefaultWait<IWebDriver>(driver);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(6);

            SwagLabLoginPage swaglabLoginPage = new(driver);
            ProductsListPage productListPage = new(driver);
            ProductDetailsPage productDetailsPage = new(driver);
            CartPage cartPage= new(driver);
            CheckoutPage checkoutPage=new(driver);
            CheckoutOverviewPage checkOverviewPage=new(driver);

            Log.Information("Buying Product Test Started");
            test.Info("Buying Product Test Started");

            string? excelFilePath = currdir + "/TestData/InputData.xlsx";
            string? sheetName = "LoginData";

            List<ExcelData> excelDataList = ExcelUtils.ReadSearchExcelData(excelFilePath, sheetName);

            foreach (var excelData in excelDataList)
            {
                string? validuserName = excelData?.ValidUserName;
                string? validpassword = excelData?.ValidPassword;
                string? firstName = excelData?.FirstName;
                string? lastName = excelData?.LastName;
                string? postalCode = excelData?.PostalCode;

                swaglabLoginPage.EnterUserName(validuserName);
                swaglabLoginPage.EnterPassword(validpassword);
                swaglabLoginPage.ClickLoginButton();

                IWebElement productLabel = driver.FindElement(By.XPath("(//a[@id='item_3_title_link'])/div"));
                label= productLabel.Text;
                IWebElement pageHeading = driver.FindElement(By.ClassName("product_label"));
                wait.Until(d => pageHeading.Displayed);

                //adding screenshot to extent report
                string filepath = ScreenShot.TakeScreenShot(driver);

                test.AddScreenCaptureFromPath(filepath,"User logged in");

                Log.Information("Logged in successfully");
                test.Info("Logged in successfully");
                
                try
                {
                    Assert.That(pageHeading.Text.Contains("Products"));

                    Log.Information("Product list page loaded.");
                    test.Info("Product list page loaded.");

                    productListPage.ClickSortOption();
                    productListPage.ClickSortZA();
                    Log.Information("Sorted from Z to A.");
                    test.Info("Sorted from Z to A.");
                    productListPage.ClickSelectProduct();

                    ScreenShot.TakeScreenShot(driver);
                    IWebElement productDetailLabel = driver.FindElement(By.ClassName("inventory_details_name"));

                    Assert.AreEqual(label, productDetailLabel.Text);
                    Log.Information("Product Details page loaded.");
                    test.Info("Product Details page loaded.");

                    productDetailsPage.ClickAddToCartButton();
                    productDetailsPage.ClickCartIcon();

                    ScreenShot.TakeScreenShot(driver); Assert.That(driver.Url.Contains("cart"));
                    Log.Information("Cart page loaded.");
                    test.Info("Cart page loaded.");

                    cartPage.ClickCheckOutButton();
                    ScreenShot.TakeScreenShot(driver);
                    Assert.That(driver.Url.Contains("checkout"));
                    Log.Information("Checkout page loaded.");
                    test.Info("Checkout page loaded.");

                    checkoutPage.EnterFirstNameField(firstName);
                    checkoutPage.EnterLastNameField(lastName);
                    checkoutPage.EnterPostalCodeField(postalCode);
                    checkoutPage.ClickContinueButton();

                    ScreenShot.TakeScreenShot(driver);
                    Assert.That(driver.FindElement(By.ClassName("subheader")).Text.Contains("Overview"));
                    Log.Information("Checkout overview displayed.");
                    test.Info("Checkout overview displayed.");

                    checkOverviewPage.ClickFinishButton();

                    ScreenShot.TakeScreenShot(driver); Assert.That(driver.FindElement(By.ClassName("complete-header")).Text.Contains("THANK YOU"));
                    Log.Information("Buying Product Test Passed.");
                    test.Pass("Buying Product Test Passed.");

                }
                catch (AssertionException ex)
                {
                    Log.Error($"Test failed for select Product. \n Exception: {ex.Message}");
                    test = extent.CreateTest("select ProductTest");
                    test.Info("Buying Product Test failed");
                    test.Fail("Buying Product Test failed");
                    test.AddScreenCaptureFromPath(filepath, "Buying Product Test Failed screenshot");
                }
            }
            Log.CloseAndFlush();
        }

    }

    }

