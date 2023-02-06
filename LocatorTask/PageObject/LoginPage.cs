using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace LocatorTask.PageObject;

public class LoginPage : BasePage
{
    public LoginPage(IWebDriver driver) : base(driver)
    {
    }

    [FindsBy(How = How.Id, Using = "username")]
    private IWebElement usernameInputField;

    [FindsBy(How = How.Id, Using = "password")]
    private IWebElement passwordInputField;

    [FindsBy(How = How.CssSelector, Using = "button[class='button w100 button-large button-solid-norm mt1-5']")]
    private IWebElement submitSigninButton;

    public InboxPage Login(string username, string password)
    {
        _waiter.Until((_driver) => usernameInputField.Displayed);
        usernameInputField.SendKeys(username);
        passwordInputField.SendKeys(password);
        submitSigninButton.Click();
        return new InboxPage(_driver);
    }
}