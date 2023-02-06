using LocatorTask.PageObject;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace LocatorTask.Elements;

public class MenuItems : BasePage
{
    public MenuItems(IWebDriver driver) : base(driver) { }

    [FindsBy(How = How.CssSelector, Using = "button[class='button button-large button-solid-norm w100 no-mobile']")]
    private IWebElement newMessageButton;

    [FindsBy(How = How.CssSelector, Using = "a[title*='Draft']")]
    private IWebElement draftMenuButton;

    [FindsBy(How = How.CssSelector, Using = "a[title*='Sent']")]
    private IWebElement sentMenuButton;

    public MessageScreen OpenNewMessageScreen()
    {
        _waiter.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[class='button button-large button-solid-norm w100 no-mobile']")));
        newMessageButton.Click();
        return new MessageScreen(_driver);
    }

    public DraftPage NavigateToDraftPage()
    {
        draftMenuButton.Click();
        return new DraftPage(_driver);
    }

    public SentPage NavigateToSentPage()
    {
        sentMenuButton.Click();
        return new SentPage(_driver);
    }
}