using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyNibblesSel.PageObjects
{
    internal class ProductDetailsPage
    {
        IWebDriver driver;
        public ProductDetailsPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "(//button[@type='submit'])[5]")]
        private IWebElement? AddToCartButton { get; set; }

        
        [FindsBy(How = How.XPath, Using = "(//button[@type='submit'])[4]")]
        private IWebElement? CheckOutButton { get; set; }

        public ProductDetailsPage ClickAddToCartButton()
        {
            //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", SearchBox);
            AddToCartButton?.Click();
            return new ProductDetailsPage(driver);
        }

        public CheckOutPage ClickCheckOutButton()
        {
            CheckOutButton?.Click();
            return new CheckOutPage(driver);
        }
    }
}
