using LocatorTask.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using LocatorTask.WebDriver;
using OpenQA.Selenium.Interactions;

namespace LocatorTask.PageObject;

public abstract class BasePage
{
    protected IWebDriver driver = Browser.GetDriver();

    protected static WebDriverWait waiter;

    protected static Actions action = new Actions(Browser.GetDriver());

    protected BasePage()
    {
        waiter = new WebDriverWait(Browser.GetDriver(), TimeSpan.FromSeconds(30));
        PageFactory.InitElements(Browser.GetDriver(), this);
    }

    public bool IsElementPresent(By locator)
    {
        return driver.FindElements(locator).Count() > 0;
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

    public static void ClickWithAction(IWebElement element)
    {
        
        action.Click(element).Build().Perform();
    }

    public static void RightClick(IWebElement element)
    {
        action.ContextClick(element).Perform();
    }

    public void JsClick(IWebElement element)
    {
        IJavaScriptExecutor executor = (IJavaScriptExecutor)Browser.GetDriver();
        executor.ExecuteScript("arguments[0].click();", element);
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