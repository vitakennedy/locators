using LocatorTask.Elements;
using LocatorTask.Utils;
using LocatorTask.Utils.Login;using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace LocatorTask.PageObject;

public class LoginPage : BasePage
{
    [FindsBy(How = How.Id, Using = "username")]
    protected IWebElement usernameInputField;

    [FindsBy(How = How.Id, Using = "password")]
    protected IWebElement passwordInputField;

    [FindsBy(How = How.CssSelector, Using = "button[class='button w100 button-large button-solid-norm mt1-5']")]
    protected Button submitSigninButton;

    [FindsBy(How = How.CssSelector, Using = ".sign-layout-title")]
    protected IWebElement SignInLabel;

    public InboxPage Login(ILoginStrategy webLogin, string username, string password)
    {
        Logger.Info($"User is logging to application with username {username} and password {password}");
        webLogin.Login(username, password);
        Logger.Info($"User is redirecting to Inbox page");
        return new InboxPage();
    }
    public bool IsLabelDisplayed() => SignInLabel.Displayed;
}