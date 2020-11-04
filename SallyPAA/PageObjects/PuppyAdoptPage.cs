using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace SallyPAA.PageObjects
{
    public class PuppyAdoptPage
    {
        private readonly IWebDriver _driver;
        readonly int _timeout = 10000; // in milliseconds

        public PuppyAdoptPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }
        
        [FindsBy(How = How.Id, Using = "collar")]
        private IWebElement _collarAndLeashCheck { get; set; }

        [FindsBy(How = How.Id, Using = "toy")]
        private IWebElement _chewToyCheck { get; set; }

        [FindsBy(How = How.Id, Using = "carrier")]
        private IWebElement _travelCarrierCheck { get; set; }

        [FindsBy(How = How.Id, Using = "vet")]
        private IWebElement _firstVetVisitCheck { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.button_to:nth-child(1)")]
        private IWebElement _completeAdoptionBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.button_to:nth-child(2)")]
        private IWebElement _adoptAnotherPuppyBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "form.button_to:nth-child(3)")]
        private IWebElement _changeYourMindBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".total_cell")]
        private IWebElement _totalCell { get; set; }

        public void WaitPageCompletion(int timeout)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(timeout));

            // Wait for the page to load
            wait.Until(webDriver => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void AddCollarAndLeash() => _collarAndLeashCheck.Click();
        public void AddChewToy() => _chewToyCheck.Click();
        public void AddTravelCarrier() => _travelCarrierCheck.Click();
        public void AddVetVisit() => _firstVetVisitCheck.Click();
        public string TotalValue => _totalCell.Text;
        
        public OrderPage Order()
        {
            _completeAdoptionBtn.Click();

            //Delay added to ensure that the page load is complete
            WaitPageCompletion(_timeout);

            return new OrderPage(_driver);
        }

        //public PuppyListPage AdoptAnother()
        //{
        //    _adoptAnotherPuppyBtn.Click();

        //    //Delay added to ensure that the page load is complete
        //    WaitPageCompletion(_timeout);

        //    return new PuppyListPage(_driver);
        //}

        public void AdoptAnother()
        {
            _adoptAnotherPuppyBtn.Click();
            WaitPageCompletion(_timeout);
        }
    }
}
