using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saucedemo_task3
{
    class HomePageTest
    {
        //DR no. 2
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver();
            driver.Url = "https://www.saucedemo.com/";

            IWebElement usernameTextBox = driver.FindElement(By.Id("user-name"));
            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

            usernameTextBox.SendKeys("standard_user");
            passwordTextBox.SendKeys("secret_sauce");
            loginButton.Click();

            Console.WriteLine("Setup - succesful login: done");
        }

        #region Design Requirement 2.1.
        [Test]
        public void BurgerButton()
        {
            IWebElement burgerButton = driver.FindElement(By.Id("react-burger-menu-btn"));            
            Assert.IsTrue(burgerButton.Enabled);         

            Console.WriteLine("test 2.1. - Burger button enabled: done");
        }
        #endregion

        #region Design Requirement 2.1.1.
        [Test]
        public void BurgerMenu()
        {
            IWebElement burgerButton = driver.FindElement(By.Id("react-burger-menu-btn"));
            Assert.IsTrue(burgerButton.Enabled);

            IWebElement burgerMenu = driver.FindElement(By.ClassName("bm-menu-wrap"));       
            burgerButton.Click();
            
            Assert.IsTrue(burgerMenu.Displayed);

            Console.WriteLine("test 2.1.1. - Burger button clicked: done");
        }
        #endregion

        #region Design Requirement 2.1.1.1.
        [Test]
        public void BurgerMenuAllItemsButton()
        {
            OpenBurgerMenu();           

            IWebElement allItemsButton = driver.FindElement(By.Id("inventory_sidebar_link"));
            Assert.IsTrue(allItemsButton.Enabled);

            Console.WriteLine("test 2.1.1.1. - \"All items\" button on side bar: done");
        }
        #endregion

        #region Design Requirement 2.1.1.2.
        [Test]
        public void BurgerMenuAboutButton()
        {
            OpenBurgerMenu();

            IWebElement aboutButton = driver.FindElement(By.Id("about_sidebar_link"));
            Assert.IsTrue(aboutButton.Enabled);

            Console.WriteLine("test 2.1.1.2. - \"About\" button on side bar: done");
        }
        #endregion

        #region Design Requirement 2.1.1.3.
        [Test]
        public void BurgerMenuLogoutButton()
        {
            OpenBurgerMenu();

            IWebElement logoutButton = driver.FindElement(By.Id("logout_sidebar_link"));
            Assert.IsTrue(logoutButton.Enabled);

            Console.WriteLine("test 2.1.1.3. - \"Logout\" button on side bar: done");
        }
        #endregion

        #region Design Requirement 2.1.1.4.
        [Test]
        public void BurgerMenuLogoutResetAppState()
        {
            OpenBurgerMenu();

            IWebElement resetAppStageButton = driver.FindElement(By.Id("about_sidebar_link"));
            Assert.IsTrue(resetAppStageButton.Enabled);

            Console.WriteLine("test 2.1.1.4. - \"Reset app stage\" button on side bar: done");
        }
        #endregion

        #region Design Requirement 2.1.2.
        [Test]
        public void CloseSideBarButton() 
        {
            Console.WriteLine("somethimes returns \"not interactable error\" - try to run it alone, should be okay.");
            OpenBurgerMenu();

            IWebElement closeSideBarButton = driver.FindElement(By.Id("react-burger-cross-btn"));
            Assert.IsTrue(closeSideBarButton.Enabled);

            wait.Until(driver => driver.FindElement(By.Id("react-burger-cross-btn")).Displayed);
            closeSideBarButton.Click();
            
            IWebElement burgerMenu = driver.FindElement(By.ClassName("bm-menu-wrap"));

            string isHiddenMessage = burgerMenu.GetAttribute("aria-hidden");
            Assert.AreEqual(isHiddenMessage, "true");

            Console.WriteLine("test 2.1.2. - Close menu button on side bar closes side bar: done");
        }
        #endregion

        #region Design Requirement 2.2.
        [Test]
        public void ShoppingCartButton()
        {
            IWebElement shoppingCartButton = driver.FindElement(By.Id("shopping_cart_container"));
            Assert.IsTrue(shoppingCartButton.Enabled);

            Console.WriteLine("test 2.2. - Shopping cart button enabled: done");
        }
        #endregion

        #region Design Requirement 2.2.1.
        [Test]
        public void ShoppingCartButtonRedirect()
        {
            IWebElement shoppingCartButton = driver.FindElement(By.Id("shopping_cart_container"));
            Assert.IsTrue(shoppingCartButton.Enabled);
            shoppingCartButton.Click();

            string checkOutUrl = "https://www.saucedemo.com/cart.html";
            Assert.AreEqual(checkOutUrl, driver.Url);

            Console.WriteLine("test 2.2.1. - Shopping cart button redirects to checkout when clicked: done");
        }
        #endregion

        //TODO: Learn how to get elements under same div as a list (and use foreach())
        #region Design Requirement 2.3.
        [Test]
        public void SauceLabsBackpackItem()
        {
            CheckRedirectionFromElement("item_4_title_link", "https://www.saucedemo.com/inventory-item.html?id=4");

            Console.WriteLine("test 2.3. - Item button redirects to item page when clicked: done");
        }
        #endregion

        #region Design Requirement 2.3.1.
        [Test]
        public void SauceLabsBackpackItemAddToChartButton()
        {
            CheckAddToCartButton("add-to-cart-sauce-labs-backpack", "remove-sauce-labs-backpack");

            Console.WriteLine("test 2.3.1. - Item button redirects to item page when clicked: done");
        }
        #endregion

        #region Design Requirement 2.4.
        [Test]
        public void SauceLabsBikeLightItem()
        {
            CheckRedirectionFromElement("item_0_title_link", "https://www.saucedemo.com/inventory-item.html?id=0");

            Console.WriteLine("test 2.4. - Item button redirects to item page when clicked: done");
        }
        #endregion

        #region Design Requirement 2.4.1.
        [Test]
        public void SauceLabsBikeLightAddToChartButton()
        {
            CheckAddToCartButton("add-to-cart-sauce-labs-bike-light", "remove-sauce-labs-bike-light");                     

            Console.WriteLine("test 2.4.1. - Item button redirects to item page when clicked: done");
        }

        #endregion

        #region Design Requirement 2.5.
        [Test]
        public void SauceLabsBoltTShirtItem()
        {
            CheckRedirectionFromElement("item_1_title_link", "https://www.saucedemo.com/inventory-item.html?id=1");

            Console.WriteLine("test 2.5. - Item button redirects to item page when clicked: done");
        }
        #endregion

        #region Design Requirement 2.5.1.
        [Test]
        public void SauceLabsBoltTShirtAddToChartButton()
        {
            CheckAddToCartButton("add-to-cart-sauce-labs-bolt-t-shirt", "remove-sauce-labs-bolt-t-shirt");

            Console.WriteLine("test 2.5.1. - Item button redirects to item page when clicked: done");
        }

        #endregion

        #region Design Requirement 2.6.
        [Test]
        public void SauceLabsFleeceJacketItem()
        {
            CheckRedirectionFromElement("item_5_title_link", "https://www.saucedemo.com/inventory-item.html?id=5");

            Console.WriteLine("test 2.6. - Item button redirects to item page when clicked: done");
        }
        #endregion

        #region Design Requirement 2.6.1.
        [Test]
        public void SauceLabsFleeceJacketAddToChartButton()
        {
            CheckAddToCartButton("add-to-cart-sauce-labs-fleece-jacket", "remove-sauce-labs-fleece-jacket");

            Console.WriteLine("test 2.5.1. - Item button redirects to item page when clicked: done");
        }
        #endregion

        #region Design Requirement 2.7.
        [Test]
        public void SauceLabsOnesieItem()
        {
            CheckRedirectionFromElement("item_2_title_link", "https://www.saucedemo.com/inventory-item.html?id=2");

            Console.WriteLine("test 2.7. - Item button redirects to item page when clicked: done");
        }
        #endregion

        #region Design Requirement 2.7.1.
        [Test]
        public void SauceLabsOnesieAddToChartButton()
        {
            CheckAddToCartButton("add-to-cart-sauce-labs-onesie", "remove-sauce-labs-onesie");

            Console.WriteLine("test 2.7.1. - Item button redirects to item page when clicked: done");
        }
        #endregion

        #region Design Requirement 2.8.
        [Test]
        public void TestAllThingsItem()
        {
            CheckRedirectionFromElement("item_3_title_link", "https://www.saucedemo.com/inventory-item.html?id=3");

            Console.WriteLine("test 2.8. - Item button redirects to item page when clicked: done");
        }
        #endregion

        #region Design Requirement 2.8.1.
        [Test]
        public void TestAllThingsAddToChartButton()
        {
            CheckAddToCartButton("add-to-cart-test.allthethings()-t-shirt-(red)", "remove-test.allthethings()-t-shirt-(red)");

            Console.WriteLine("test 2.8.1. - Item button redirects to item page when clicked: done");
        }
        #endregion

        #region Design Requirement 2.9.
        [Test]
        public void TwitterSocialButton()
        {            
            CheckSocialsButton("social_twitter", "https://twitter.com/saucelabs");       
            Console.WriteLine("test 2.9. - Twitter social button redirects to Twitter page when clicked: done");
        }
        #endregion

        #region Design Requirement 2.10.
        [Test]
        public void FacebookSocialButton()
        {
            CheckSocialsButton("social_facebook", "https://web.facebook.com/saucelabs?_rdc=1&_rdr");
            Console.WriteLine("test 2.10. - Facebook social button redirects to Facebook page when clicked: done");
        }
        #endregion

        #region Design Requirement 2.10.
        [Test]
        public void LinkedinSocialButton()
        {
            CheckSocialsButton("social_linkedin", "https://www.linkedin.com/company/sauce-labs/");
            Console.WriteLine("test 2.11. - Linkedin button redirects to Linkedin page when clicked: done");
        }
        #endregion

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
                       
            Console.WriteLine("TearDown: done");
        }

        public void OpenBurgerMenu()
        {
            IWebElement burgerButton = driver.FindElement(By.Id("react-burger-menu-btn"));
            burgerButton.Click();
        }

        public void CheckRedirectionFromElement(string id, string expectedUrl)
        {
            IWebElement element = driver.FindElement(By.Id(id));
            Assert.IsTrue(element.Enabled);
            element.Click();

            Assert.AreEqual(expectedUrl, driver.Url);
        }
        public void CheckAddToCartButton(string addToChartButtonId, string removeFromChartId)
        {
            IWebElement addToCartButton = driver.FindElement(By.Id(addToChartButtonId));
            Assert.IsTrue(addToCartButton.Enabled);
            addToCartButton.Click();

            IWebElement removeFromCartButton = driver.FindElement(By.Id(removeFromChartId));
            Assert.IsTrue(removeFromCartButton.Enabled);
        }

        public void CheckSocialsButton(string className, string expectedSocialUrl)
        {
            string originalWindow = driver.CurrentWindowHandle;
            //string expectedSocialUrl = "https://twitter.com/saucelabs";

            IWebElement socialButton = driver.FindElement(By.ClassName(className));
            Assert.IsTrue(socialButton.Enabled);

            int oldNumOfTabs = driver.WindowHandles.Count();
            socialButton.Click();

            wait.Until(driver => driver.WindowHandles.Count == 1 + oldNumOfTabs);

            int newNumOfTabs = driver.WindowHandles.Count();
            Assert.IsTrue(newNumOfTabs > oldNumOfTabs);

            //Loop through until we find a new window handle
            //Note: probably wont work with more than two tabs open (refactor?)
            foreach (string window in driver.WindowHandles)
            {
                if (originalWindow != window)
                {
                    driver.SwitchTo().Window(window);
                    break;
                }
            }
            Assert.AreEqual(expectedSocialUrl, driver.Url);

        }

    }
}

