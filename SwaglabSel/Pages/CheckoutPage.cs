using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaglabSel.Pages
{
    internal class CheckoutPage
    {
        IWebDriver driver;

        public CheckoutPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);

        }

        [FindsBy(How = How.Id, Using = "first-name")]
        private IWebElement? FirstNameField { get; set; }

        [FindsBy(How = How.Id, Using = "last-name")]
        private IWebElement? LastNameField { get; set; }

        [FindsBy(How = How.Id, Using = "postal-code")]
        private IWebElement? PostalCodeField { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//input[@value='CONTINUE']")]
        private IWebElement? ContinueButton { get; set; }

        public void EnterFirstNameField(string firstName)
        {
            FirstNameField?.SendKeys(firstName);
        }

        public void EnterLastNameField(string lastName)
        {
            LastNameField?.SendKeys(lastName);
        }
        public void EnterPostalCodeField(string postalCode)
        {
            PostalCodeField?.SendKeys(postalCode);
        }
        public void ClickContinueButton()
        {
            ContinueButton?.Click();
        }
    }
}
