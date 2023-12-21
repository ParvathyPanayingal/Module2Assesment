using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyNibblesSel.PageObjects
{
    internal class PicklesResultPage
    {
        IWebDriver driver;
        public PicklesResultPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "((//div[@class=\"grid-item__meta-main\"])/div)[6]")]
        [CacheLookup]
        private IWebElement? Product { get; set; }

        public ProductDetailsPage ClickProduct()
        {
            Product?.Click();
            return new ProductDetailsPage(driver);
        }
    }
}
