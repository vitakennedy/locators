using LocatorTask.PageObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;

namespace LocatorTask.Elements;

public class Toolbar : BasePage
{
    public Toolbar(IWebDriver driver) : base(driver) { }

    [FindsBy(How = How.CssSelector, Using = "[for=idSelectAll]")]
    private IWebElement checkboxSelectAll;

    [FindsBy(How = How.CssSelector, Using = "button[data-testid='toolbar:movetotrash']")]
    private IWebElement deleteAllDraftsButton;

    public void SelectAllEmails()
    {
        _waiter.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[for=idSelectAll]")));
        Actions action = new Actions(_driver);
        action.Click(checkboxSelectAll).Build().Perform();
        _waiter.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[data-testid='toolbar:movetotrash']")));
    }

    public void DeleteAllEmails()
    {
        deleteAllDraftsButton.Click();
    }
}