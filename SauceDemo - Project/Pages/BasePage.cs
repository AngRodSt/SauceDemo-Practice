using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemo.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;

        protected BasePage(IWebDriver driver, int timeoutSeconds = 10)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));            
        }

        protected IWebElement WaitAndFind(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        protected void WaitAndClick(By locator)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Click();
        }

        protected void WaitAndSendKeys(By locator, string text)
        {
            var element = WaitAndFind(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected bool IsElementPresent(By locator,  int shortTimeout = 3)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(shortTimeout));
                wait.Until(ExpectedConditions.ElementExists(locator));
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
