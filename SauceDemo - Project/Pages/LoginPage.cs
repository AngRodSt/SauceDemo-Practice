using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        //Constructor
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        //Locators
        private readonly By usernameInput = By.Id("user-name");
        private readonly By passwordInput = By.Id("password");
        private readonly By loginButton = By.Id("login-button");
        private readonly By errorMessage = By.CssSelector("h3[data-test='error']");

        //Methods
        public void GoTo()=> driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        public void EnterUsername(string username) => driver.FindElement(usernameInput).SendKeys(username);
        public void EnterPassword(string password) => driver.FindElement(passwordInput).SendKeys(password);
        public void ClickLogin() => driver.FindElement(loginButton).Click();
        public string GetErrorMessage() => driver.FindElement(errorMessage).Text;


    }
}
