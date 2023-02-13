using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace LocatorTask.PageObject;

public class InboxPage : BasePage
{
    public InboxPage() : base() { }

    [FindsBy(How = How.CssSelector, Using = "div[class='mauto text-center max-w30e'] h1")]
    private IWebElement welcomeLabel;

    public IWebElement GetWelcomeLabel()
    {
        return welcomeLabel;
    }

    public bool IsWelcomeLabelDisplayed()
    {
        waiter.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[class='mauto text-center max-w30e'] h1")));
        return welcomeLabel.Displayed;
    }
}