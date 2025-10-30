// Drivers/WebDriverFactory.cs
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

public static class WebDriverFactory
{
    public static IWebDriver CreateDriver(string browser = "chrome", bool headless = true)
    {
      
        if (browser.Equals("chrome", StringComparison.OrdinalIgnoreCase))
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var options = new ChromeOptions();
            if (headless) options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--disable-extensions");
            // unique profile per process to avoid "user data dir in use"
            var tempProfile = Path.Combine(Path.GetTempPath(), "chrome-profile-" + Guid.NewGuid());
            Directory.CreateDirectory(tempProfile);
            options.AddArgument($"--user-data-dir={tempProfile}");

            // avoid locking issues (optional cleanup strategy)
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.SuppressInitialDiagnosticInformation = true;
            driverService.HideCommandPromptWindow = true;

            return new ChromeDriver(driverService, options, TimeSpan.FromSeconds(60));
        }

        throw new NotSupportedException($"Browser {browser} not supported.");
    }

  
}