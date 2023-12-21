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
    internal class ProductDetailsPage
    {
        IWebDriver driver;
        DefaultWait<IWebDriver> wait;
        
        public ProductDetailsPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
            wait = new DefaultWait<IWebDriver>(driver);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(6);
        }

        [FindsBy(How = How.XPath, Using = "(//div[@class='inventory_details_price'])/following::button")]
        private IWebElement? AddToCartButton { get; set; }
        
        [FindsBy(How = How.XPath, Using = "(//div[@id='shopping_cart_container'])/a")]
        private IWebElement? CartIcon { get; set; }

        public void ClickAddToCartButton()
        {
            wait.Until(d => AddToCartButton.Displayed);
            AddToCartButton?.Click();
        }

        public void ClickCartIcon()
        {
            CartIcon?.Click();
        }

    }
}
