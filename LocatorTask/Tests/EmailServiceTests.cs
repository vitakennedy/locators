using System.Configuration;
using LocatorTask.Elements;
using LocatorTask.PageObject;
using NUnit.Framework;

namespace LocatorTask.Tests
{
    [TestFixture]
    public class Tests : BaseTest
    {
        private string _subject;
        private string _addressee;
        private string _body;

        MainPage _mainPage;
        InboxPage _inboxPage;
        MenuItems _menuItems;
        Header _header;
        SentPage _sendPage;
        DraftPage _draftPage;

        [SetUp]
        public void Setup()
        {
            _mainPage = new MainPage(_driver);
            _inboxPage = new InboxPage(_driver);
            _menuItems = new MenuItems(_driver);
            _header = new Header(_driver);
            _sendPage = new SentPage(_driver);
            _draftPage = new DraftPage(_driver);
            _addressee = "vitakennedy@gmail.com";
            _body = "This is draft message";
            _subject = "draft_subject";
            NavigateToMainPage();
            Login();
        }

        [Test, Order(1)]
        public void IsUserLoggedIn()
        {
            Assert.IsTrue(_inboxPage.IsWelcomeLabelDisplayed(), "User is not signed in");
        }

        [Test, Order(2)]

        public void IsEmailSavedAsADraft()
        {
            SaveEmailAsDraft(_subject, _addressee, _body);
            Assert.IsNotNull(NavigateToTheDraftPage().GetDraftSubjects(), "There are no saved drafts on the Draft page");
            Assert.IsTrue(NavigateToTheDraftPage().IsThereAnyDraft(_subject), "Email is not saved as a draft");
        }

        [Test, Order(3)]
        public void CheckSubject()
        {
            SaveEmailAsDraft(_subject, _addressee, _body);
            Assert.That(NavigateToTheDraftPage().SelectDraftByItsSubject(_subject).Text, Is.EqualTo(_subject), "Subject is not correct");
        }

        [Test, Order(4)]
        public void CheckAddressee()
        {
            SaveEmailAsDraft(_subject, _addressee, _body);
            Assert.That(NavigateToTheDraftPage().SelectDraftByItsAddressee(_addressee).Text, Is.EqualTo(_addressee), "Addressee is not correct");
        }

        [Test, Order(5)]
        public void CheckBody()
        {
            SaveEmailAsDraft(_subject, _addressee, _body);
            var messageScreen = NavigateToTheDraftPage().OpenEmailSavedAsDraft(_subject);
            messageScreen.SwitchToFrame();
            var actualBodyEmail = messageScreen.GetBodyEmail();
            messageScreen.ExitFromFrame();
            messageScreen.CloseMessageScreen();
            Assert.That(actualBodyEmail, Is.EqualTo(_body), "Message body is not correct");
        }

        [Test, Order(6)]
        public void SentMenuPageContainsSentEmail()
        {
            SaveEmailAsDraft(_subject, _addressee, _body);
            SendEmailFromTheDraftMenu(_subject);
            Assert.IsTrue(NavigateToTheSentPage().IsThereAnySentEmail(_subject), "Sent email is not exist in the Sent menu page");
        }

        [Test, Order(7)]
        public void DraftPageDoesNotContainSentEmail()
        {
            SaveEmailAsDraft(_subject, _addressee, _body);
            SendEmailFromTheDraftMenu(_subject);
            Assert.IsEmpty(NavigateToTheDraftPage().GetDraftSubjects(), "Draft is still exist in the Draft page");
        }

        public void Login()
        {
            _mainPage.NavigateToLoginPage().Login(ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]);
        }

        public void NavigateToMainPage()
        {
            _mainPage.OpenProtonMainPage(ConfigurationManager.AppSettings["URL"]);
        }

        public void Logout()
        {
            _header.NavigateToProfileDropDown().Logout();

        }

        public DraftPage NavigateToTheDraftPage() => _menuItems.NavigateToDraftPage();

        public SentPage NavigateToTheSentPage() => _menuItems.NavigateToSentPage();


        public void SendEmailFromTheDraftMenu(string subject)
        {
            NavigateToTheDraftPage().OpenEmailSavedAsDraft(subject).SendEmail();
        }

        public void SaveEmailAsDraft(string subject, string addressee, string body)
        {
            var newMessageScreen = _menuItems.OpenNewMessageScreen();
            newMessageScreen.FillEmail(addressee, subject, body);
            newMessageScreen.CloseMessageScreen();
        }

        public void DeleteDrafts()
        {
            if (NavigateToTheDraftPage().GetDraftSubjects().Any())
            {
                _draftPage.Toolbar.SelectAllEmails();
                _draftPage.Toolbar.DeleteAllEmails();
            }
        }

        public void DeleteSentEmails()
        {
            if (NavigateToTheSentPage().GetSentEmailsSubject().Any())
            {
                _sendPage.Toolbar.SelectAllEmails();
                _sendPage.Toolbar.DeleteAllEmails();
            }
        }

        [TearDown]
        public void Clenaup()
        {
            DeleteDrafts();
            DeleteSentEmails();
            Logout();
        }
    }
}