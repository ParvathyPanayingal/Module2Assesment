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
    internal class ProductsListPage
    {
        IWebDriver driver;
        
        public ProductsListPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            PageFactory.InitElements(driver, this);
           
        }

        [FindsBy(How = How.ClassName, Using = "product_sort_container")]
        private IWebElement? SortOption { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//option[@value='za']")]
        private IWebElement? SortZA { get; set; }

        [FindsBy(How = How.Id, Using = "item_3_title_link")]
        private IWebElement? SelectProduct { get; set; }
        

        public void ClickSortOption()
        {
            SortOption?.Click();
        }
        public void ClickSortZA()
        {
            SortZA?.Click();
        }

        public ProductDetailsPage ClickSelectProduct()
        {
            SelectProduct?.Click();
            return new ProductDetailsPage(driver);
        }

    }
}
