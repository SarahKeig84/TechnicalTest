using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace GenbaTechnicalTest
{

    public class LoginPage
    {
        public IWebElement LoginButton => Properties.driver.FindElement(By.CssSelector("[ng-click='login()']"));

        public void VerifyPage()
        {
            string loginPageTitle = Properties.driver.Title;
            Assert.AreEqual(expectedLoginPageTitle, loginPageTitle);
            Console.WriteLine("Login Page Title is: " + loginPageTitle);
        }

        public void Login(string email, string password, IWebElement loginButton)
        {
            WebDriverWait wait = new WebDriverWait(Properties.driver, TimeSpan.FromSeconds(60));
            var emailBox = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("[ng-model='email']")));
            var passwordBox = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("[ng-model='password']")));
            emailBox.SendKeys(email);
            passwordBox.SendKeys(password);
            loginButton.Click();
        }

        //Expected strings
        public static string expectedLoginPageTitle = "Galactico Eleven - Premier League Draft with Auction Fantasy Football. Your way. For free!";
    }
}