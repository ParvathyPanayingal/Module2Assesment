using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyNibblesSel.PageObjects
{
    internal class AccountPage
    {
        IWebDriver driver;
        string? productDetailLabel;
        public AccountPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }
        
        [FindsBy(How = How.Id, Using = "customer_register_link")]
        private IWebElement? CreateAccLink { get; set; }

        public void ClickCreateAccLink()
        {
            CreateAccLink?.Click();
        }
    }
}
