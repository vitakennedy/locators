using LocatorTask.Elements;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace LocatorTask.PageObject;

public class SentPage : BasePage
{
    public SentPage() : base()
    {
        Toolbar = new Toolbar();
    }

    [FindsBy(How = How.CssSelector, Using = ".item-subject span")]
    private IList<IWebElement> sentEmailSubjects;

    [FindsBy(How = How.CssSelector, Using = ".item-senders span")]
    private IList<IWebElement> draftAddressees;

    public IList<IWebElement> GetSentEmailsSubject() => sentEmailSubjects; 


    public IList<IWebElement> GetDraftAddressees() => draftAddressees;

    public IWebElement SelectDraftByItsSubject(string subject) => 
        GetSentEmailsSubject().FirstOrDefault(draft => draft.GetAttribute("title") == subject);

    public MessageScreen OpenDraft(string subject)
    {
        SelectDraftByItsSubject(subject).Click();
        return new MessageScreen();
    }

    public bool IsThereAnySentEmail(string subject)
    {
        waiter.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".item-subject span")));
        return GetSentEmailsSubject().Any(draft => draft.GetAttribute("title") == subject);
    }

    public Toolbar Toolbar { get; set; }
}