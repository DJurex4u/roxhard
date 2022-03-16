using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTestAutomation
{
    class Class1
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver();
            Console.WriteLine("Setup done");
        }

        [Test]
        public void test()
        {
            driver.Url = "https://www.youtube.com/";

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            
            IWebElement agreeButton = wait.Until(e => e.FindElement(By.XPath("//*[contains(text(), 'Slažem se')]")));
            agreeButton.Click();
            Console.WriteLine("agreeButton - pressed");

            IWebElement searchQuery = wait.Until(e => e.FindElement(By.Name("search_query")));
            Console.WriteLine("searchQuery - found");

            IWebElement searchButton = driver.FindElement(By.Id("search-icon-legacy"));
            Console.WriteLine("searchButton - found");

            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);  // not working for some reason
            System.Threading.Thread.Sleep(1000); // yikes! 

            //element not interactable exception - without pause/sleep
            searchQuery.SendKeys("Won't Rick Roll you, I promise");
            Console.WriteLine("Won't Rick Roll you - text sent");
            System.Threading.Thread.Sleep(3000);

            IWebElement searchClearButton = wait.Until(e => e.FindElement(By.Id("search-clear-button")));
            Console.WriteLine("searchClearButton - found");          
            searchClearButton.Click();
            Console.WriteLine("searchClearButton - pressed");

            searchQuery.SendKeys("Rick roll");
            searchButton.Click();

            //TODO: learn how to select an item from search results
            //driver.FindElement(By.XPath("/html/body/ytd-app/div/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div/ytd-section-list-renderer/div[2]/ytd-item-section-renderer/div[3]/ytd-video-renderer[1]/div[1]")).Click();
            System.Threading.Thread.Sleep(1000);
            searchClearButton.Click();            
            searchQuery.SendKeys("'cause I don't know how to select from search results xD");

            driver.Manage().Window.Maximize();

            Console.WriteLine("Test done");
        }

        [TearDown]
        public void closeBrowser()
        {
            //driver.Close();
            Console.WriteLine("Čiča miča gotova je priča");
            Console.WriteLine("TearDown done");
        }
    }
}
