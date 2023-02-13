using LocatorTask.Elements;
using LocatorTask.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Configuration;

namespace LocatorTask.PageObject;

public class DraftPage : BasePage
{
    public DraftPage() : base()
    {
        Toolbar = new Toolbar();
    }

    [FindsBy(How = How.CssSelector, Using = ".item-subject span")]
    private IList<IWebElement> draftSubjects;

    [FindsBy(How = How.CssSelector, Using = ".item-senders span")]
    private IList<IWebElement> draftAddressees;

    [FindsBy(How = How.ClassName, Using = "dropdown-content")]
    private IWebElement ContextMenuDropDown;

    public IList<IWebElement> GetDraftSubjects() => draftSubjects;

    public IList<IWebElement> GetDraftAddressees() => draftAddressees;

    public IWebElement SelectDraftByItsSubject(string subject)
    {
        waiter.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".item-subject span")));
        return GetDraftSubjects().ToList().FirstOrDefault(draft => draft.Text == subject);
    }
  
    public IWebElement SelectDraftByItsAddressee(string addressee) =>
        GetDraftAddressees().FirstOrDefault(draft => draft.GetAttribute("title") == addressee);

    public bool IsThereAnyDraft(string subject) =>
        GetDraftSubjects().Any(draft => draft.GetAttribute("title") == subject);

    public MessageScreen OpenEmailSavedAsDraft(string subject)
    {
        SelectDraftByItsSubject(subject).Click();
        return new MessageScreen();
    }

    public void OpenContextMenu(string subject)
    {
        RightClick(SelectDraftByItsSubject(subject));
    }

    public bool IsContextMenuDisplayed()
    {
        waiter.Until(ExpectedConditions.ElementIsVisible(By.ClassName("dropdown-content")));
        return ContextMenuDropDown.Displayed;
    }

    public Toolbar Toolbar { get; set; }
}