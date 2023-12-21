using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TastyNibblesSel.PageObjects
{
    internal class CreateAccountPage
    {
        IWebDriver driver;
        public CreateAccountPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }
        
        [FindsBy(How = How.Id, Using = "FirstName")]
        private IWebElement? FirstName { get; set; }

        [FindsBy(How = How.Id, Using = "LastName")]
        private IWebElement? LastName { get; set; }
        
        [FindsBy(How = How.Id, Using = "Email")]
        private IWebElement? Email { get; set; }

        [FindsBy(How = How.Id, Using = "CreatePassword")]
        private IWebElement? Password { get; set; }


        public void EnterFirstName(string firstName)
        {
            FirstName?.SendKeys(firstName);
        }

        public void EnterLastName(string lastName)
        {  
            LastName?.SendKeys(lastName); 
        }

        public void EnterEmail(string email) 
        { 
            Email?.SendKeys(email);
        }

        public void EnterPassword(string password) 
        {
            Password.SendKeys(password);
        }
    }
}
