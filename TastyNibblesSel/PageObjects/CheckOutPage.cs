using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyNibblesSel.PageObjects
{
    internal class CheckOutPage
    {
        IWebDriver driver;
        public CheckOutPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        private DefaultWait<IWebDriver> fluentWait()
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(50);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "Element not found.";
            return fluentWait;
        }

        [FindsBy(How = How.Id, Using = "email")]

        private IWebElement? Contact { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='shippingAddressForm']/div/div/div[2]/div/div/div/input")]

        private IWebElement? FirstName { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[@id='shippingAddressForm']/div/div/div[2]/div[2]/div/div/input")]
        private IWebElement? LastName { get; set; }
        
        [FindsBy(How = How.Id, Using = "shipping-address1")]

        private IWebElement? Address { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[@id='shippingAddressForm']/div/div/div[6]/div/div/div/input")]

        private IWebElement? City { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//select[@Name='zone']/option[19]")]

        private IWebElement? State { get; set; }

        [FindsBy(How = How.XPath, Using = "(//div[@id='shippingAddressForm']/div/div/div[6]/div[3])/div/div/div/input")]

        private IWebElement? PinCode { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[@id='shippingAddressForm']/div/div/div[7]/div/div/div/input")]

        private IWebElement? PhoneNo { get; set; }

        public void EnterPhoneNo(string contactNo)
        {
            PhoneNo?.SendKeys(contactNo);
        }

        public void EnterPincode(string pincode)
        {
            PinCode?.SendKeys(pincode);
        }

        public void SelectState()
        {
            State?.Click();
        }

        public void EnterCity(string city) 
        {
            City?.Click();
            City?.SendKeys(city);
        }

        public void EnterAddress(string address) 
        {
            Address?.Click();
            Address?.SendKeys(address);
        }
        public void EnterLastName(string lastName)
        {
            LastName?.Click();
            LastName?.SendKeys(lastName);
        }

        public void EnterFirstName(string firstName)
        {
            FirstName?.Click();
            Thread.Sleep(2000);
            FirstName?.SendKeys(firstName);

        }

        public void EnterContact(string contactNo)
        {
            Contact?.Click();
            Thread.Sleep(3000);
            Contact?.SendKeys(contactNo);
            
        }

       
    }
}
