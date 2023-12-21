using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaglabSel.Pages
{
    internal class SwagLabLoginPage
    {
        IWebDriver driver;
        
        public SwagLabLoginPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
           
        }

        [FindsBy(How = How.Id, Using = "user-name")]
        private IWebElement? UserNameField { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement? PasswordField { get; set; }

        [FindsBy(How = How.Id, Using = "login-button")]
        private IWebElement? LoginButton { get; set; }
        

        public void EnterUserName(string validuserName)
        {
            UserNameField?.SendKeys(validuserName);
        }

        public void EnterPassword(string validpassword)
        {
            PasswordField?.SendKeys(validpassword);
        }

        public ProductsListPage ClickLoginButton()
        {
            LoginButton?.Click();
            return new ProductsListPage(driver);
        }
    }
}
