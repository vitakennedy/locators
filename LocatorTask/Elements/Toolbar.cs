using LocatorTask.PageObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;

namespace LocatorTask.Elements;

public class Toolbar : BasePage
{
    public Toolbar() : base() { }

    [FindsBy(How = How.CssSelector, Using = "#idSelectAll")]
    private IWebElement checkboxSelectAll;

    [FindsBy(How = How.CssSelector, Using = "button[data-testid='toolbar:movetotrash']")]
    private IWebElement deleteAllDraftsButton;

    public void SelectAllEmails()
    {
        ClickWithAction(checkboxSelectAll);
        waiter.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[data-testid='toolbar:movetotrash']")));
    }

    public void DeleteAllEmails()
    {
        JsClick(deleteAllDraftsButton);
    }

    public bool AreAllEmailsSelected()
    {
        return checkboxSelectAll.Selected;
    }
}