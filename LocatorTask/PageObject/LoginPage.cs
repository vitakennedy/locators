using System.ComponentModel.Design;
using LocatorTask.Elements;
using LocatorTask.Utils.Login;
using LocatorTask.WebDriver;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace LocatorTask.PageObject;

public class LoginPage : BasePage
{
    [FindsBy(How = How.Id, Using = "username")]
    protected IWebElement usernameInputField;

    [FindsBy(How = How.Id, Using = "password")]
    protected IWebElement passwordInputField;

    [FindsBy(How = How.CssSelector, Using = "button[class='button w100 button-large button-solid-norm mt1-5']")]
    protected Button submitSigninButton;

    public InboxPage Login(ILoginStrategy webLogin, string username, string password)
    {
        webLogin.Login(username, password);
        return new InboxPage();
    }
}