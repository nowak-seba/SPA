using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SallyPAA.Helpers;
using SeleniumExtras.PageObjects;

namespace SallyPAA.PageObjects
{
    public class OrderPage
    {
        private readonly IWebDriver _driver;

        public OrderPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "order_name")]
        [CacheLookup]
        private IWebElement _nameInput { get; set; }

        [FindsBy(How = How.Id, Using = "order_address")]
        [CacheLookup]
        private IWebElement _addressInput { get; set; }

        [FindsBy(How = How.Id, Using = "order_email")]
        [CacheLookup]
        private IWebElement _emailInput { get; set; }

        [FindsBy(How = How.Id, Using = "order_pay_type")]
        [CacheLookup]
        private IWebElement _paymentTypeDropDown { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".actions > input:nth-child(1)")]
        [CacheLookup]
        private IWebElement _placeOrderBtn { get; set; }

        public void WaitPageCompletion(int timeout)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(timeout));
            // Wait for the page to load
            wait.Until(webDriver => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void PlaceOrder(string paymentType)
        {
            _nameInput.SendKeys(RandomString.GenerateRandomString(5, true));
            _addressInput.SendKeys(RandomString.GenerateRandomString(15));
            _emailInput.SendKeys(RandomEmail.GenerateEmail("gmail.com", 10));

            var selectElement = new SelectElement(_paymentTypeDropDown);
            selectElement.SelectByText(paymentType);

            _placeOrderBtn.Click();
        }
    }
}
