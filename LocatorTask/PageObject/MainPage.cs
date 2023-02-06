using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace LocatorTask.PageObject;

public class MainPage : BasePage
{
    public MainPage(IWebDriver driver) : base(driver) { }

    [FindsBy(How = How.CssSelector, Using = "div.ml-auto a:nth-child(1)")]
    private IWebElement signInButton;

    public void OpenProtonMainPage(string url)
    {
        GetDriver().Navigate().GoToUrl(url);
    }

    public LoginPage NavigateToLoginPage()
    {
        _waiter.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.ml-auto a:nth-child(1)")));
        signInButton.Click();
        return new LoginPage(_driver);
    }
}