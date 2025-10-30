using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.Pages
{
    public class LoginPage : BasePage
    {
        
        //Constructor
        public LoginPage(IWebDriver driver) : base(driver) { }

        //Locators
        private readonly By usernameInput = By.Id("user-name");
        private readonly By passwordInput = By.Id("password");
        private readonly By loginButton = By.Id("login-button");
        private readonly By errorMessage = By.CssSelector("h3[data-test='error']");
        private readonly string url = "https://www.saucedemo.com/";

        //Methods
        public void GoTo()=> Driver.Navigate().GoToUrl(url);
        public void Login(string user, string password)
        {
            WaitAndSendKeys(usernameInput, user);
            WaitAndSendKeys(passwordInput, password);
            WaitAndClick(loginButton);
        }
       public string GetErrorText()
       {
            return IsElementPresent(errorMessage, 2) ? WaitAndFind(errorMessage).Text : string.Empty;
       }


    }
}
