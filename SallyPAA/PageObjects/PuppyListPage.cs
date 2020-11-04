using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SallyPAA.Helpers;
using SeleniumExtras.PageObjects;

namespace SallyPAA.PageObjects
{
    public class PuppyListPage
    {
        private readonly string _testUrl = "http://puppies.herokuapp.com/";
        readonly int _timeout = 10000; // in milliseconds
        private readonly IWebDriver _driver;
        private WebDriverWait _wait;

        public PuppyListPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }
        
        [FindsBy(How = How.CssSelector, Using = "div.puppy_list:nth-child(3) > div:nth-child(1) > div:nth-child(2) > h3:nth-child(1)")]
        private IWebElement _puppyName { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.puppy_list > div:nth-child(1) > div:nth-child(2) > h3:nth-child(1)")]
        private IList<IWebElement> _puppyNames { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".puppy_list")]
        private IList<IWebElement> _puppyList { get; set; }

        [FindsBy(How = How.Id, Using = "notice")]
        private IWebElement _thankYouNotice { get; set; }

        // Pagination
        [FindsBy(How = How.CssSelector, Using = ".next_page")]
        private IWebElement _nextPageButton { get; set; }

        public void WaitPageCompletion(int timeout)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(timeout));

            // Wait for the page to load
            wait.Until(webDriver => ((IJavaScriptExecutor)webDriver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        // Go to the designated page
        public void GoToPage()
        {
            _driver.Navigate().GoToUrl(_testUrl);
        }

        public void PuppyAllNames1()
        {
            IList<IWebElement> all = _driver.FindElements(By.CssSelector("div.puppy_list > div:nth-child(1) > div:nth-child(2) > h3:nth-child(1)"));

            var names = new List<string>();
            foreach (var element in all)
            {
                Console.WriteLine(element.Text);
                names.Add(element.Text);
            }

            foreach (var element in _puppyNames)
            {
                Console.WriteLine($" Puppy Names: {element.Text}  {element.Displayed}");

            }
        }

        public void PuppyAllNames2()
        {
            IList<IWebElement> all = _driver.FindElements(By.CssSelector("div.puppy_list > div:nth-child(1) > div:nth-child(2) > h3:nth-child(1)"));

            var names = new List<string>();

            foreach (var element in all)
            {
                Console.WriteLine(element.Text);
                names.Add(element.Text);
            }
            
            var nextButtonClassName = _driver.FindElement(By.CssSelector(".next_page")).GetAttribute("class");
            Console.WriteLine($"nextButtonClassName: {nextButtonClassName}");
            while (!nextButtonClassName.Contains("disabled"))
            {
                _driver.FindElement(By.CssSelector(".next_page")).Click();
                all = _driver.FindElements(By.CssSelector("div.puppy_list > div:nth-child(1) > div:nth-child(2) > h3:nth-child(1)"));

                foreach (var element in all)
                {
                    Console.WriteLine(element.Text);
                    names.Add(element.Text);
                }
                nextButtonClassName = _driver.FindElement(By.CssSelector(".next_page")).GetAttribute("class");
            }

            Console.WriteLine(names);
            Console.WriteLine(names.Count);
        }

        private IEnumerable<string> PuppyAllNames()
        {
            var names = new List<string>();

            // first page names
            foreach (var element in _puppyNames) names.Add(element.Text);

            // handle names from other pages
            var nextButtonClassName = _nextPageButton.GetAttribute("class");
            while (!nextButtonClassName.Contains("disabled"))
            {
                _nextPageButton.Click();

                names.AddRange(_puppyNames.Select(element => element.Text));
                nextButtonClassName = _nextPageButton.GetAttribute("class");
            }

            Console.WriteLine(names.Count);
            return names;
        }

        public List<string> TwoRandomPuppies()
        {
            return PuppyAllNames().GetRandomElements(2);
        }

        public IWebElement GetPuppyByName(string name)
        {
            // Puppy on first page
            var puppy = _puppyList.SingleOrDefault(element =>
                element.FindElement(By.CssSelector("div:nth-child(1) > div:nth-child(2) > h3:nth-child(1)")).Text.Equals(name));
            if (puppy != null) return puppy;
            
            // navigate through all list
            var nextButtonClassName = _nextPageButton.GetAttribute("class");
            while (!nextButtonClassName.Contains("disabled"))
            {
                _nextPageButton.Click();
                puppy = _puppyList.SingleOrDefault(element =>
                    element.FindElement(By.CssSelector("div:nth-child(1) > div:nth-child(2) > h3:nth-child(1)")).Text.Equals(name));
                if (puppy != null) 
                    return puppy;

                nextButtonClassName = _nextPageButton.GetAttribute("class");
            }

            return null;
        }

        public PuppyDetailsPage GoToPuppyDetails(IWebElement puppy)
        {
            var detailsButton = puppy.FindElement(By.CssSelector(".rounded_button"));
            detailsButton.Click();

            //Delay added to ensure that the page load is complete
            WaitPageCompletion(_timeout);

            return new PuppyDetailsPage(_driver);
        }
    }
}
