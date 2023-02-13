using LocatorTask.PageObject;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace LocatorTask.Elements;

public class ProfileDropDown : BasePage
{
    public ProfileDropDown() : base() { }

    [FindsBy(How = How.XPath, Using = "//button[text()='Sign out']")]
    private IWebElement signoutButton;

    [FindsBy(How = How.XPath, Using = "//h1[text()='Sign in']")]
    private IWebElement signInLabel;

    public void Logout()
    {
        signoutButton.Click();
    }
}