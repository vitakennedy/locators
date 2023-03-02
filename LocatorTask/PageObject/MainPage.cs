using LocatorTask.Elements;
using LocatorTask.WebDriver;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace LocatorTask.PageObject;

public class MainPage : BasePage
{
    [FindsBy(How = How.CssSelector, Using = "div.ml-auto a:nth-child(1)")]
    private IWebElement signInButton;

    public LoginPage NavigateToLoginPage()
    {
        waiter.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.ml-auto a:nth-child(1)")));
        signInButton.Click();
        return new LoginPage();
    }

    public void ClickSignInButton()
    {
        waiter.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.ml-auto a:nth-child(1)")));
        signInButton.Click();
    }
}