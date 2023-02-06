using LocatorTask.Elements;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Configuration;

namespace LocatorTask.PageObject;

public class DraftPage : BasePage
{
    public DraftPage(IWebDriver driver) : base(driver)
    {
        Toolbar = new Toolbar(driver);
    }

    [FindsBy(How = How.CssSelector, Using = ".item-subject span")]
    private IList<IWebElement> draftSubjects;

    [FindsBy(How = How.CssSelector, Using = ".item-senders span")]
    private IList<IWebElement> draftAddressees;

    public IList<IWebElement> GetDraftSubjects() => draftSubjects;

    public IList<IWebElement> GetDraftAddressees() => draftAddressees;

    public IWebElement SelectDraftByItsSubject(string subject)
    {
        return GetDraftSubjects().ToList().FirstOrDefault(draft => draft.Text == subject);
    }
  

    public IWebElement SelectDraftByItsAddressee(string addressee) =>
        GetDraftAddressees().FirstOrDefault(draft => draft.GetAttribute("title") == addressee);

    public bool IsThereAnyDraft(string subject) =>
        GetDraftSubjects().Any(draft => draft.GetAttribute("title") == subject);

    public MessageScreen OpenEmailSavedAsDraft(string subject)
    {
        SelectDraftByItsSubject(subject).Click();
        return new MessageScreen(_driver);
    }

    public Toolbar Toolbar { get; set; }
}