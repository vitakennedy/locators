using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using LocatorTask.Blocks;
using LocatorTask.Elements;

namespace LocatorTask.PageObject;

public class DraftPage : BasePage
{
    public Toolbar Toolbar => new();

    [FindsBy(How = How.CssSelector, Using = ".item-subject span")]
    private IList<HtmlElement> draftSubjects;

    [FindsBy(How = How.CssSelector, Using = ".item-senders span")]
    private IList<HtmlElement> draftAddressees;

    [FindsBy(How = How.ClassName, Using = "dropdown-content")]
    private IWebElement ContextMenuDropDown;

    public IList<HtmlElement> GetDraftSubjects() => draftSubjects;

    public IList<HtmlElement> GetDraftAddressees() => draftAddressees;

    public HtmlElement SelectDraftByItsSubject(string subject)
    {
        waiter.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".item-subject span")));
        return GetDraftSubjects().ToList().FirstOrDefault(draft => draft.Text == subject);
    }

    public HtmlElement SelectDraftByItsAddressee(string addressee) =>
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
        SelectDraftByItsSubject(subject).RightClick();
    }

    public bool IsContextMenuDisplayed()
    {
        waiter.Until(ExpectedConditions.ElementIsVisible(By.ClassName("dropdown-content")));
        return ContextMenuDropDown.Displayed;
    }
}