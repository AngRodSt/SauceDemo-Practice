using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using System;
using System.IO;

namespace SauceDemo.Hooks
{
    [Binding]
    public class Hooks
    {
        public static IWebDriver? Driver { get; private set; }
        private readonly ScenarioContext _scenarioContext;

 
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var browser = Environment.GetEnvironmentVariable("TEST_BROWSER") ?? "chrome";
            var headless = (Environment.GetEnvironmentVariable("HEADLESS") ?? "true") == "true";

            Driver = WebDriverFactory.CreateDriver(browser, headless);
        }

        [AfterStep]
        public void AfterStep()
        {
            if (_scenarioContext.TestError != null && Driver != null)
            {
                try
                {
                    var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                    var dir = Path.Combine(Directory.GetCurrentDirectory(), "TestResults", "Screenshots");
                    Directory.CreateDirectory(dir);

                    var fileName = $"{SanitizeFileName(_scenarioContext.ScenarioInfo.Title)}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    var filePath = Path.Combine(dir, fileName);

                    screenshot.SaveAsFile(filePath);
                    TestContext.AddTestAttachment(filePath, "Failure screenshot");

                    Console.WriteLine($"Screenshot saved: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to capture screenshot: {ex.Message}");
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                Driver?.Quit();
                Driver?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while closing WebDriver: {ex.Message}");
            }
        }

        private string SanitizeFileName(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }
    }
}
