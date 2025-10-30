using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SauceDemo.Hooks;
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
        private readonly IWebDriver driver;
        private readonly LoginPage _loginPage;

        public LoginSteps()
        {
            driver = Hooks.Hooks.Driver;
            _loginPage = new LoginPage(driver);
        }

        [Given("I am on the SauceDemo login page")]
        public void GivenIAmOnTheSauceDemoLoginPage()
        {
            _loginPage.GoTo();
            Assert.IsTrue(driver.Title.Contains("Swag Labs") || driver.Url.Contains("saucedemo"), "Landing page not loaded");
        }

        [When("I enter valid credentials")]
        public void WhenIEnterValidCredentials()
        {
            _loginPage.Login("standard_user", "secret_sauce");
        }

        [Then("I should see the inventory page")]
        public void ThenIShouldSeeTheInventoryPage()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("inventory.html"));
            Assert.IsTrue(driver.Url.Contains("inventory.html"));
        }

        //Invalid Login Steps
        [When("I enter invalid credentials")]
        public void WhenIEnterInvalidCredentials()
        {
            _loginPage.Login("invalid_user", "wrong_pass");
        }

        [Then("I should see an error message")]
        public void ThenIShouldSeeAnErrorMessage()
        {
            var text = _loginPage.GetErrorText();
            Assert.IsTrue(!string.IsNullOrEmpty(text), "Expected error message but none shown");
            Assert.That(text.ToLower(), Does.Contain("username").Or.Contain("error"));
        }
    }
}
