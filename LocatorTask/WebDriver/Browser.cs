using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorTask.WebDriver;
public class Browser
{

    private static Browser currentInstane;
    private static IWebDriver driver;
    public static BrowserFactory.BrowserType CurrentBrowser;
    public static int ImplWait;
    public static double TimeoutForElement;
    private static string browser;

    private Browser()
    {
        InitParamas();
        driver = BrowserFactory.GetDriver(CurrentBrowser, 1000);
    }

    private static void InitParamas()
    {
        browser = ConfigurationManager.AppSettings["Browser"];
        Enum.TryParse(browser, out CurrentBrowser);
    }

    public static Browser Instance => currentInstane ?? (currentInstane = new Browser());

    public static void WindowMaximise()
    {
        driver.Manage().Window.Maximize();
    }

    public static void NavigateTo(string url)
    {
        driver.Navigate().GoToUrl(url);
    }

    public static IWebDriver GetDriver()
    {
        return driver;
    }

    public static void Quit()
    {
        driver.Quit();
        currentInstane = null;
        driver = null;
        browser = null;
    }
}