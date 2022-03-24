using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saucedemo_task3
{
    //DR no. 1
    class LoginPageTest
    {
        IWebDriver driver;
        string loginSuccesfulRedirectUrl = "https://www.saucedemo.com/inventory.html";

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver();
            driver.Url = "https://www.saucedemo.com/";
            Console.WriteLine("Setup - go to saucedemo web page: done");
        }

        #region Design Requirement 1.
        [Test]
        public void LoginPageDisplayed()
        {
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));
            Assert.IsTrue(loginButton.Enabled);

            Console.WriteLine("test 1. - Login Page displayed: done");
        }
        #endregion

        #region Design Requirement 1.1.
        [Test]
        public void UsernameTextbox()
        {    
            IWebElement usernameTextBox = driver.FindElement(By.Id("user-name"));

            Assert.IsTrue(usernameTextBox.Enabled);
            Assert.IsTrue(string.IsNullOrEmpty(usernameTextBox.GetAttribute("Value")));
            
            Console.WriteLine("test 1.1. - UsernameTexbox enabled and empty: done");
        }
        #endregion

        #region Design Requirement 1.2.
        [Test]
        public void PasswordTextBox()
        {
            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));

            Assert.IsTrue(passwordTextBox.Enabled);
            Assert.IsTrue(string.IsNullOrEmpty(passwordTextBox.GetAttribute("Value")));

            Console.WriteLine("test 1.2. - PasswordTextbox enabled and null/empty: done");
        }
        #endregion

        #region Design Requirement 1.3.
        [Test]
        public void ValidUsernamesTxt()
        {
            IWebElement validUsernamesTxt = driver.FindElement(By.Id("login_credentials"));
            
            Assert.IsFalse(string.IsNullOrEmpty(validUsernamesTxt.Text));
                        
            Console.WriteLine("test 1.3. - ValidUsernames not null/empty: done");
        }
        #endregion

        #region Design Requirement 1.4.
        [Test]
        public void PasswordHeader()
        {
            IWebElement passwordHeader = driver.FindElement(By.Id("login_credentials"));

            Assert.IsFalse(string.IsNullOrEmpty(passwordHeader.Text));

            Console.WriteLine("test 1.4. - ValidPassword not null or empty: done");
        }
        #endregion
        
        #region Design Requirement 1.5.
        [Test]
        public void LoginButton()
        {
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));

            Assert.IsTrue(loginButton.Enabled);

            Console.WriteLine("test 1.5. - LoginButton enabled: done");
        }
        #endregion

        #region Design Requirement 1.5.1.
        [Test]
        public void EmptyUsernameAndPasswordLoginAttempt()
        {
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));

            loginButton.Click();

            IWebElement errorMessage = driver.FindElement(By.ClassName("error-message-container"));

            Assert.AreEqual(errorMessage.Text, "Epic sadface: Username is required");
            //Console.WriteLine(errorMessage.Text);

            Console.WriteLine("test 1.5.1. - Correct error message displayed after login attempt with empty username and password: done");
        }
        #endregion

        #region Design Requirement 1.5.2.
        [Test]
        public void WrongUsernameAndPasswordLoginAttempt()
        {
            IWebElement usernameTextBox = driver.FindElement(By.Id("user-name"));
            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));
            
            usernameTextBox.SendKeys("soMthendfg Random");
            passwordTextBox.SendKeys("random not valid");
            loginButton.Click();

            IWebElement errorMessage = driver.FindElement(By.ClassName("error-message-container"));
            
            Assert.AreNotEqual(driver.Url, loginSuccesfulRedirectUrl);
            Assert.AreEqual(errorMessage.Text, "Epic sadface: Username and password do not match any user in this service");

            Console.WriteLine("test 1.5.2. - Correct error message displayed after login attempt with wrong username and password: done");
        }
        #endregion

        #region Design Requirement 1.5.3.
        [Test]
        public void UsernameFilledPasswordEmptyLoginAttempt()
        {
            IWebElement usernameTextBox = driver.FindElement(By.Id("user-name"));
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));

            usernameTextBox.SendKeys("standard_user");            
            loginButton.Click();

            IWebElement errorMessage = driver.FindElement(By.ClassName("error-message-container"));

            Assert.AreNotEqual(driver.Url, loginSuccesfulRedirectUrl);
            Assert.AreEqual(errorMessage.Text, "Epic sadface: Password is required");

            usernameTextBox.Clear();            

            Console.WriteLine("test 1.5.3. - Correct error message displayed after login attempt with username and without password: done");
        }
        #endregion

        #region Design Requirement 1.5.4.
        [Test]
        public void UsernameEmptyPasswordFilledLoginAttempt()
        {
            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));

            passwordTextBox.SendKeys("secret_sauce");
            loginButton.Click();

            IWebElement errorMessage = driver.FindElement(By.ClassName("error-message-container"));

            Assert.AreNotEqual(driver.Url, loginSuccesfulRedirectUrl);
            Assert.AreEqual(errorMessage.Text, "Epic sadface: Username is required");

            Console.WriteLine("test 1.5.4. - Correct error message displayed after login attempt without username and with password: done");
        }
        #endregion

        #region Design Requirement 1.5.5.
        [Test]
        public void UnauthorisedRedirectToHomePage()
        {
            driver.Url = "https://www.saucedemo.com/inventory.html";

            IWebElement errorMessage = driver.FindElement(By.ClassName("error-message-container"));

            Assert.AreNotEqual(driver.Url, loginSuccesfulRedirectUrl);
            Assert.AreEqual(errorMessage.Text, "Epic sadface: You can only access '/inventory.html' when you are logged in.");

            Console.WriteLine("test 1.5.5. - Correct error message displayed after login attempt without username and with password: done");
        }
        #endregion

        #region Design Requirement 1.6.
        [Test]
        public void ErrorButton()
        {            
            IWebElement errorButton;

            IWebElement usernameTextBox = driver.FindElement(By.Id("user-name"));
            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.Id("login-button"));

            //TODO: refactor this into methods and use them 1.5.1 - 1.5.5 and here
            //1.5.1.
            passwordTextBox.SendKeys("secret_sauce");
            loginButton.Click();
            Console.WriteLine("Login attempt: done");

            errorButton = driver.FindElement(By.ClassName("error-button"));
            Assert.IsTrue(errorButton.Enabled);
            errorButton.Click();

            passwordTextBox.Clear();
            Console.WriteLine("1.5.1. case: done");

            //1.5.2.
            usernameTextBox.SendKeys("soMthendfg Random");
            passwordTextBox.SendKeys("random not valid");
            loginButton.Click();

            errorButton = driver.FindElement(By.ClassName("error-button"));
            Assert.IsTrue(errorButton.Enabled);
            errorButton.Click();

            usernameTextBox.Clear();
            passwordTextBox.Clear();
            Console.WriteLine("1.5.2. case: done");

            //1.5.3.
            usernameTextBox.SendKeys("standard_user");
            loginButton.Click();

            errorButton = driver.FindElement(By.ClassName("error-button"));
            Assert.IsTrue(errorButton.Enabled);
            errorButton.Click();

            usernameTextBox.Clear();
            Console.WriteLine("1.5.3. case: done");

            //1.5.4.
            passwordTextBox.SendKeys("secret_sauce");
            loginButton.Click();

            errorButton = driver.FindElement(By.ClassName("error-button"));
            Assert.IsTrue(errorButton.Enabled);
            errorButton.Click();

            passwordTextBox.Clear();
            Console.WriteLine("1.5.4. case: done");

            //1.5.5.
            driver.Url = "https://www.saucedemo.com/inventory.html";

            errorButton = driver.FindElement(By.ClassName("error-button"));
            Assert.IsTrue(errorButton.Enabled);
            errorButton.Click();

            Console.WriteLine("1.5.5. case: done");
        }
        #endregion

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
            Console.WriteLine("TearDown: done");
        }
    }
}
