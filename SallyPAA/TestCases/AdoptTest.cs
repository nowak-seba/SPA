using System;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SallyPAA.PageObjects;

namespace SallyPAA.TestCases
{
    [TestFixture]
    class AdoptTest
    {
        private IWebDriver _driver;

        [SetUp]
        public void Init()
        {
            _driver = new FirefoxDriver();
            _driver.Manage().Window.Maximize();
        }

        //Scenarios
        //* Adopt Brook, add a Chewy Toy and a Travel Carrier, pay with Check
        //* Adopt Sparky, add a Collar & Leash, pay with Credit Card
        //* Adopt 2 Dogs add a Collar & Leash to each, pay with Credit Card

        [Test]
        [TestCase(TestName = "Adopt Brook, add a Chewy Toy and a Travel Carrier, pay with Check")]
        public void AdoptBrookeTest()
        {
            var puppyList = new PuppyListPage(_driver);
            puppyList.GoToPage();
            Console.WriteLine(_driver.Url);

            var puppy = puppyList.GetPuppyByName("Brook");
            puppyList.GoToPuppyDetails(puppy);

            var puppyDetails = new PuppyDetailsPage(_driver);
            Console.WriteLine(_driver.Url);
            puppyDetails.Adopt();

            var puppyAdopt = new PuppyAdoptPage(_driver);
            Console.WriteLine(_driver.Url);
            puppyAdopt.AddChewToy();
            puppyAdopt.AddTravelCarrier();
            Console.WriteLine($"Total amount: {puppyAdopt.TotalValue}");
            puppyAdopt.Order();

            var order = new OrderPage(_driver);
            order.PlaceOrder("Check");

            // Assertion goes here
        }

        [Test]
        [TestCase(TestName = "Adopt Sparky, add a Collar & Leash, pay with Credit Card")]
        public void AdoptSparkyTest()
        {
            var puppyList = new PuppyListPage(_driver);
            puppyList.GoToPage();
            Console.WriteLine(_driver.Url);

            var puppy = puppyList.GetPuppyByName("Sparky");
            puppyList.GoToPuppyDetails(puppy);

            var puppyDetails = new PuppyDetailsPage(_driver);
            Console.WriteLine(_driver.Url);
            puppyDetails.Adopt();

            var puppyAdopt = new PuppyAdoptPage(_driver);
            Console.WriteLine(_driver.Url);
            puppyAdopt.AddCollarAndLeash();
            Console.WriteLine($"Total amount: {puppyAdopt.TotalValue}");
            puppyAdopt.Order();

            var order = new OrderPage(_driver);
            order.PlaceOrder("Credit card");

            // Assertion goes here
        }

        [Test]
        [TestCase(TestName = "Adopt 2 Dogs add a Collar & Leash to each, pay with Credit Card")]
        public void AdoptTwoDogs()
        {
            var puppyList = new PuppyListPage(_driver);
            puppyList.GoToPage();

            // get two random puppies
            var twoDogs = puppyList.TwoRandomPuppies();
            puppyList.GoToPage();

            // handle first Puppy
            var firstPuppy = puppyList.GetPuppyByName(twoDogs.FirstOrDefault());
            puppyList.GoToPuppyDetails(firstPuppy);

            var puppyDetails = new PuppyDetailsPage(_driver);
            puppyDetails.Adopt();

            var puppyAdopt = new PuppyAdoptPage(_driver);
            puppyAdopt.AddCollarAndLeash();
            puppyAdopt.AdoptAnother();

            // handle second Puppy
            var secondPuppy = puppyList.GetPuppyByName(twoDogs.LastOrDefault());
            puppyList.GoToPuppyDetails(secondPuppy);

            puppyDetails.Adopt();

            puppyAdopt.AddCollarAndLeash();
            puppyAdopt.Order();

            var order = new OrderPage(_driver);
            order.PlaceOrder("Credit card");

            // Assertion goes here
        }

        [TearDown]
        public void Cleanup()
        {
            var passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
            Console.WriteLine("Test Status: " + (passed ? "passed" : "failed"));
            try
            {
                
            }
            finally
            {
                // Terminate remote webdriver session
                _driver.Quit();
            }
        }
    }
}
