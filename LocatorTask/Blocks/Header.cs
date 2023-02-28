using LocatorTask.PageObject;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace LocatorTask.Blocks;

public class Header : BasePage
{
    [FindsBy(How = How.CssSelector, Using = "button.user-dropdown-button")]
    private IWebElement profileButton;

    public ProfileDropDown NavigateToProfileDropDown()
    {
        profileButton.Click();
        return new ProfileDropDown();
    }
}