using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace SallyPAA.PageObjects
{
    public class PuppyDetailsPage
    {
        private readonly IWebDriver _driver;
        readonly int _timeout = 10000; // in milliseconds

        public PuppyDetailsPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".rounded_button")]
        private IWebElement AdoptMeButton { get; set; }

        // Returns the Page Title
        public string GetPageTitle()
        {
            return _driver.Title;
        }

        public void WaitPageCompletion(int timeout)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(timeout));

            // Wait for the page to load
            wait.Until(webDriver => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public PuppyAdoptPage Adopt()
        {
            AdoptMeButton.Click();

            //Delay added to ensure that the page load is complete
            WaitPageCompletion(_timeout);

            return new PuppyAdoptPage(_driver);
        }
    }
}
