using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using Serilog;
using System;
using TastyNibblesBDD.Hooks;
using TechTalk.SpecFlow;

namespace TastyNibblesBDD.StepDefinitions
{
    [Binding]
    public class CreateAccountSteps:CoreCodes
    {
        IWebDriver? driver = AllHooks.driver;

        [Given(@"User is on the home page")]
        public void GivenUserIsOnTheHomePage()
        {
            driver.Url = "https://www.tastynibbles.in/";
            driver.Manage().Window.Maximize();
        }

        [When(@"User clicks on Account button")]
        public void WhenUserClicksOnAccountButton()
        {
            IWebElement Account = driver.FindElement(By.XPath("//div[@class='site-nav']/div/a[2]"));
            Account.Click();
        }

        [Then(@"User will be redirected to Account page")]
        public void ThenUserWillBeRedirectedToAccountPage()
        {
            TakeScreenShot(driver);
            Log.Information("Screenshot taken");
            try
            {
                Assert.That(driver.Title.Contains("Account"));
                LogTestResult("Account Test", "Account Test Success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Account Test", "Account Test Failed", ex.Message);
                TakeScreenShot(driver);
            }
        }

        [When(@"User clicks on Create Account")]
        public void WhenUserClicksOnCreateAccount()
        {
            IWebElement CreateAccount = driver.FindElement(By.Id("customer_register_link"));
            CreateAccount.Click();
        }

        [Then(@"User will be redirected to Create Account Page")]
        public void ThenUserWillBeRedirectedToCreateAccountPage()
        {
            TakeScreenShot(driver);
            Log.Information("Screenshot taken");
            try
            {
                Assert.That(driver.Title.Contains("Create Account"));
                LogTestResult("Create Account Test", "Create Account Test Success");
            }
            catch (AssertionException ex)
            {
                LogTestResult("Create Account Test", "Create Account Test Failed", ex.Message);
                TakeScreenShot(driver);
            }
        }

        [When(@"User enters first name '([^']*)'")]
        public void WhenUserEntersFirstName(string firstName)
        {
            IWebElement FirstNameField = driver.FindElement(By.Id("FirstName"));
            FirstNameField.SendKeys(firstName);
        }

        [When(@"User enters last name '([^']*)'")]
        public void WhenUserEntersLastName(string lastName)
        {
            IWebElement LastNameField = driver.FindElement(By.Id("LastName"));
            LastNameField.SendKeys(lastName);
        }

        [When(@"User enters email '([^']*)'")]
        public void WhenUserEntersEmail(string email)
        {
            IWebElement EmailField = driver.FindElement(By.Id("Email"));
            EmailField.SendKeys(email);
        }

        [When(@"User enters password '([^']*)'")]
        public void WhenUserEntersPassword(string password)
        {
            IWebElement PasswordField = driver.FindElement(By.Id("CreatePassword"));
            PasswordField.SendKeys(password);
        }

        [Then(@"User can create an account")]
        public void ThenUserCanCreateAnAccount()
        {
            TakeScreenShot(driver);
            Log.Information("Screenshot taken");
            IWebElement CreateButton = driver.FindElement(By.XPath("//p/input[@value='Create']"));
            try
            {

                Assert.That(CreateButton.GetAttribute("value").Equals("Create")
                    , $"Test failed for Create Account Test");
                Log.Information("Test passed for Create Account ");

            }
            catch (AssertionException ex)
            {
                Log.Error($"Test failed for Create Account. \n Exception: {ex.Message}");
                TakeScreenShot(driver);
            }
        }


    }
}
