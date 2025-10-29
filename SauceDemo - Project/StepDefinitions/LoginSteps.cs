using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        private IWebDriver driver;
        private LoginPage loginPage;

        [Given("I am on the SauceDemo login page")]
        public void GivenIAmOnTheSauceDemoLoginPage()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);
            loginPage.GoTo();
        }

        [When("I enter valid credentials")]
        public void WhenIEnterValidCredentials()
        {
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.ClickLogin();
        }

        [Then("I should see the inventory page")]
        public void ThenIShouldSeeTheInventoryPage()
        {
            Assert.That(driver.Url, Does.Contain("inventory.html"));
            driver.Quit();
        }

        //Invalid Login Steps
        [When("I enter invalid credentials")]
        public void WhenIEnterInvalidCredentials()
        {
            loginPage.EnterUsername("invalid_user");
            loginPage.EnterPassword("wrong_password");
            loginPage.ClickLogin();
        }

        [Then("I should see an error message")]
        public void ThenIShouldSeeAnErrorMessage()
        {
            string message = loginPage.GetErrorMessage();
            Assert.That(message, Does.Contain("Username and password do not match"));
            driver.Quit();
        }
    }
}
