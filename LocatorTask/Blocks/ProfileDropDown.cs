using LocatorTask.PageObject;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace LocatorTask.Blocks;

public class ProfileDropDown : BasePage
{
    [FindsBy(How = How.XPath, Using = "//button[text()='Sign out']")]
    private IWebElement signoutButton;

    public void Logout()
    {
        signoutButton.Click();
    }
}