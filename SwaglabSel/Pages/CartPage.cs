using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaglabSel.Pages
{
    internal class CartPage
    {
        IWebDriver driver;

        public CartPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
            
        }

        [FindsBy(How = How.XPath, Using = "(//div[@class='cart_footer'])/a[2]")]
        private IWebElement? CheckOutButton { get; set; }

        public void ClickCheckOutButton()
        {
            CheckOutButton?.Click();
        }

        
    }
}
