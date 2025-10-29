using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ReqnrollProject1.Drivers;
using SauceDemo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private readonly IWebDriver _driver;
        private readonly LoginPage _loginPage;

        public LoginSteps()
        {
            _driver = WebDriverFactory.Create();
            _loginPage = new LoginPage(_driver);
        }

        [Given("I am on the SauceDemo login page")]
        public void GivenIAmOnTheSauceDemoLoginPage()
        {
            _loginPage.GoTo();
        }

        [When("I enter valid credentials")]
        public void WhenIEnterValidCredentials()
        {
            _loginPage.EnterUsername("standard_user");
            _loginPage.EnterPassword("secret_sauce");
            _loginPage.ClickLogin();
        }

        [Then("I should see the inventory page")]
        public void ThenIShouldSeeTheInventoryPage()
        {
            Assert.That(_driver.Url, Does.Contain("inventory.html"));
            _driver.Quit();
        }

        //Invalid Login Steps
        [When("I enter invalid credentials")]
        public void WhenIEnterInvalidCredentials()
        {
            _loginPage.EnterUsername("invalid_user");
            _loginPage.EnterPassword("wrong_password");
            _loginPage.ClickLogin();
        }

        [Then("I should see an error message")]
        public void ThenIShouldSeeAnErrorMessage()
        {
            string message = _loginPage.GetErrorMessage();
            Assert.That(message, Does.Contain("Username and password do not match"));
            _driver.Quit();
        }
    }
}
