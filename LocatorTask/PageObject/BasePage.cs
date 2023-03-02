using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using LocatorTask.WebDriver;
using System.Security.Policy;

namespace LocatorTask.PageObject;

public abstract class BasePage
{
    public static WebDriverWait waiter;

    protected BasePage()
    {
        waiter = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(30));
        PageFactory.InitElements(Browser.GetDriver(), this);
    }

    public static bool IsPageUrlContaining(string url)
    {
        return Browser.GetDriver().Url.EndsWith(url);
    }

    public bool IsElementPresent(By locator)
    {
        return Browser.GetDriver().FindElements(locator).Count() > 0;
    }

    public static IWebElement WaitUntilElementExists(By elementLocator)
    {
        try
        {
            return waiter.Until(ExpectedConditions.ElementExists(elementLocator));
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
            return waiter.Until(ExpectedConditions.ElementIsVisible(elementLocator));
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
            return waiter.Until(ExpectedConditions.UrlToBe(url));
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("Page with URL: '" + url + "' is not open.");
            throw;
        }
    }
}