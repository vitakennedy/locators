using LocatorTask.PageObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;
using LocatorTask.Elements;

namespace LocatorTask.Blocks;

public class Toolbar : BasePage
{
    [FindsBy(How = How.CssSelector, Using = "#idSelectAll")]
    private Checkbox checkboxSelectAll;

    [FindsBy(How = How.CssSelector, Using = "button[data-testid='toolbar:movetotrash']")]
    private Checkbox deleteAllDraftsButton;

    public void SelectAllEmails()
    {
        checkboxSelectAll.ClickWithAction();
        waiter.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("button[data-testid='toolbar:movetotrash']")));
    }

    public void DeleteAllEmails()
    {
        deleteAllDraftsButton.JsClick();
    }

    public bool AreAllEmailsSelected()
    {
        return checkboxSelectAll.Selected;
    }
}