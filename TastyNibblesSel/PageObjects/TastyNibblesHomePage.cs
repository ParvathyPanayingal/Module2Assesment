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
    internal class TastyNibblesHomePage
    {
        IWebDriver driver;
        public TastyNibblesHomePage(IWebDriver? driver)
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

        [FindsBy(How = How.XPath, Using = "(//input[@type='search'])[4]")]
        [CacheLookup]
        private IWebElement? SearchBox { get; set; }

        [FindsBy(How = How.XPath, Using = "(//form/button[@type='submit'])[3]")]
        [CacheLookup]
        private IWebElement? SearchIcon { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[@class='site-nav']/div/a[2]")]
        private IWebElement? Account { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//a[@href='/']")]
        private IWebElement? Logo { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[@class='page-width']/ul/li[3]/a")]
        private IWebElement? ReadyToEat { get; set; }

        public void ClickReadyToEat()
        {
            ReadyToEat?.Click();
        }

        public void ClickLogo()
        {
            Logo?.Click();
        }

        

        public AccountPage ClickAccount()
        {
            Account?.Click();
            return new AccountPage(driver);
        }

        public void ClickSearchIcon()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", SearchIcon);
            Thread.Sleep(3000);
        }


        public PicklesResultPage TypeSearchInput(string searchText)
        {
            if (SearchBox == null)
            {
                throw new NoSuchElementException(nameof(SearchBox));
            }
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click()", SearchBox);
            SearchBox.SendKeys(searchText);
            SearchBox.SendKeys(Keys.Enter);
            return new PicklesResultPage(driver);
        }
    }
}
