using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using System;
using System.Reflection.Emit;
using TastyNibblesBDD.Hooks;
using TechTalk.SpecFlow;

namespace TastyNibblesBDD.StepDefinitions
{
    [Binding]
    public class BuyProductSteps:CoreCodes
    {
        IWebDriver? driver = AllHooks.driver;
        string? label;
        ExtentTest test;
        ExtentReports extent=AllHooks.extent;
        
        [Given(@"User is on home page")]
        public void GivenUserIsOnTheHomePage()
        {
            // driver.Url = "https://www.tastynibbles.in/";
            // driver.Manage().Window.Maximize();
            // AllHooks.InitializeBrowser();
           
            
        }

        [When(@"User will type the '([^']*)' in the search box")]
        public void WhenUserWillTypeTheInTheSearchBox(string searchtext)
        {
            
            IWebElement SearchIcon = driver.FindElement(By.XPath("(//form/button[@type='submit'])[1]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", SearchIcon);
            
            IWebElement SearchBox = driver.FindElement(By.XPath("(//input[@type='search'])[4]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", SearchBox);
            SearchBox.SendKeys(searchtext);
            SearchBox.SendKeys(Keys.Enter);
        }
        [Then(@"Search results are loaded in the same page with '([^']*)'")]
        public void ThenSearchResultsAreLoadedInTheSamePageWith(string searchtext)
        {
            TakeScreenShot(driver);
            Log.Information("Screenshot Taken");
            try
            {
                Assert.That(driver.Url.Contains(searchtext));
                test=extent.CreateTest("sdvgasdhg");
                test.Pass();
                LogTestResult("Search Test", "Search Test Success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Search Test", "Search Test Failed", ex.Message);
                TakeScreenShot(driver);
            }

        }
        
        [When(@"User clicks on '([^']*)'")]
        public void WhenUserClicksOn(string productno)
        {
            IWebElement Product = driver.FindElement(By.XPath("((//div[@class=\"grid-item__meta-main\"])/div)["+productno+"]"));
            label = Product.Text;
            Product.Click();
        }

        [Then(@"Product page is loaded")]
        public void ThenProductPageIsLoaded()
        {
            TakeScreenShot(driver);
            Log.Information("Screenshot taken");
            try
            {
                Assert.That(driver.Title.Contains(label));
                LogTestResult("Product Test", "Product Test Success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Product Test", "Product Test Failed", ex.Message);
                TakeScreenShot(driver);
            }
        }

        [When(@"User clicks on Add to Cart Button")]
        public void WhenUserClicksOnAddToCartButton()
        {
            
            IWebElement AddToCart = driver.FindElement(By.XPath("(//button[@type='submit'])[5]"));
            AddToCart.Click();
            Thread.Sleep(3000);
        }

        [Then(@"The product should be present inside the cart")]
        public void ThenTheProductShouldBePresentInsideTheCart()
        {
            
            IWebElement CartDetails = driver.FindElement(By.XPath("//div[@class='cart__item-title']/a"));
            TakeScreenShot(driver);
            Log.Information("Screenshot taken");
            try
            {
                Assert.IsTrue(CartDetails.Text.Contains(label));
                LogTestResult("Cart Test", "Cart Test Success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Cart Test", "Cart Test Failed", ex.Message);
                TakeScreenShot(driver);
            }
        }

        [When(@"User clicks on Checkout")]
        public void WhenUserClicksOnCheckout()
        {
            IWebElement CheckoutButton = driver.FindElement(By.XPath("(//button[@type='submit'])[4]"));
            CheckoutButton.Click();
        }

        [Then(@"User will be redirected to the Checkout page")]
        public void ThenUserWillBeRedirectedToTheCheckoutPage()
        {
            TakeScreenShot(driver);
            Log.Information("Screenshot taken");
            try
            {
                Assert.That(driver.Title.Contains("Checkout"));
                LogTestResult("Buy Product Test", "Buy Product Test Success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Buy Product Test", "Buy Product Test Failed", ex.Message);
                TakeScreenShot(driver);
            }
        }



    }
}
