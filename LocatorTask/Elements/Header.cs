using LocatorTask.PageObject;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace LocatorTask.Elements;

public class Header : BasePage
{
    public Header(IWebDriver driver) : base(driver) { }

    [FindsBy(How = How.CssSelector, Using = "button.user-dropdown-button")]
    private IWebElement profileButton;

    public ProfileDropDown NavigateToProfileDropDown()
    {
        profileButton.Click();
        return new ProfileDropDown(_driver);
    }
}