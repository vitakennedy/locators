using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;
using Microsoft.VisualBasic.FileIO;

namespace LocatorTask.WebDriver;
public class BrowserFactory
{
    public enum BrowserType
    {
        Chrome,
        Firefox,
        remoteFirefox,
        remoteChrome
    }

    public static IWebDriver GetDriver(BrowserType type, int timeOutSec)
    {
        IWebDriver driver = null;

        switch (type)
        {
            case BrowserType.Chrome:
            {
                var service = ChromeDriverService.CreateDefaultService();
                var option = new ChromeOptions();
                option.AddArgument("disable-infobars");
                driver = new ChromeDriver(service, option, TimeSpan.FromSeconds(timeOutSec));
                break;
            }
            case BrowserType.Firefox:
            {
                var service = FirefoxDriverService.CreateDefaultService();
                var options = new FirefoxOptions();
                driver = new FirefoxDriver(service, options, TimeSpan.FromSeconds(timeOutSec));
                break;
            }
            case BrowserType.remoteFirefox:
                {
                    var options = new FirefoxOptions();
                    driver = new RemoteWebDriver(new Uri("http://localhost:5566/wd/hub"), options.ToCapabilities(), TimeSpan.FromMinutes(3));
                    break;
                }
            case BrowserType.remoteChrome:
                {
                    var option = new ChromeOptions();
                    option.AddArgument("disable-infobars");
                    option.AddArgument("--no-sandbox");
                    driver = new RemoteWebDriver(new Uri("http://localhost:5566/wd/hub"), option.ToCapabilities());
                    break;
                }
        }

        return driver;
    }
}
