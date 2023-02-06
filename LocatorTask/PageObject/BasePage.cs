using LocatorTask.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Threading;

namespace LocatorTask.PageObject;

public abstract class BasePage
{
    protected static IWebDriver _driver;
    protected static WebDriverWait _waiter;

    protected BasePage(IWebDriver driver)
    {
        _driver = driver;
        _waiter = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        PageFactory.InitElements(driver, this);
    }

    public IWebDriver GetDriver()
    {
        return _driver;
    }

    public bool IsElementPresent(By locator)
    {
        return _driver.FindElements(locator).Count() > 0;
    }

    public static IWebElement WaitUntilElementExists(By elementLocator)
    {
        try
        {
            return _waiter.Until(ExpectedConditions.ElementExists(elementLocator));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Element with locator: '" + elementLocator + "' was not found in current context page.");
            throw;
        }
    }

    public static IWebElement WaitUntilElementVisible(By elementLocator)
    {
        try
        {
            return _waiter.Until(ExpectedConditions.ElementIsVisible(elementLocator));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Element with locator: '" + elementLocator + "' was not found.");
            throw;
        }

    }

    public static bool WaitUntilUrlToBe(string url)
    {
        try
        {
            return _waiter.Until(ExpectedConditions.UrlToBe(url));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Page with URL: '" + url + "' is not open.");
            throw;
        }
    }
}