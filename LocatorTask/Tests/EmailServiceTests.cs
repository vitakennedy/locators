using System.Configuration;
using LocatorTask.Elements;
using LocatorTask.PageObject;
using LocatorTask.WebDriver;
using NUnit.Framework;

namespace LocatorTask.Tests
{
    [TestFixture]
    public class Tests : BaseTest
    {
        private string subject;
        private string addressee;
        private string body;

        MainPage mainPage;
        InboxPage inboxPage;
        MenuItems menuItems;
        Header header;
        SentPage sendPage;
        DraftPage draftPage;

        [SetUp]
        public void Setup()
        {
            mainPage = new MainPage();
            inboxPage = new InboxPage();
            menuItems = new MenuItems();
            header = new Header();
            sendPage = new SentPage();
            draftPage = new DraftPage();
            addressee = "vitakennedy@gmail.com";
            body = "This is draft message";
            subject = "draft_subject";
            NavigateToMainPage();
            Login();
        }

        [Test, Order(1)]
        public void IsUserLoggedIn()
        {
            Assert.IsTrue(inboxPage.IsWelcomeLabelDisplayed(), "User is not signed in");
        }

        [Test, Order(2)]

        public void IsEmailSavedAsADraft()
        {
            SaveEmailAsDraft(subject, addressee, body);
            Assert.IsNotNull(NavigateToTheDraftPage().GetDraftSubjects(), "There are no saved drafts on the Draft page");
            Assert.IsTrue(NavigateToTheDraftPage().IsThereAnyDraft(subject), "Email is not saved as a draft");
        }

        [Test, Order(3)]
        public void IsContextMenuDisplayed()
        {
            SaveEmailAsDraft(subject, addressee, body);
            NavigateToTheDraftPage().OpenContextMenu(subject);
            Assert.IsTrue(draftPage.IsContextMenuDisplayed(), "Context Menu is not displayed");
        }

        [Test, Order(4)]
        public void CheckSubject()
        {
            SaveEmailAsDraft(subject, addressee, body);
            Assert.That(NavigateToTheDraftPage().SelectDraftByItsSubject(subject).Text, Is.EqualTo(subject), "Subject is not correct");
        }

        [Test, Order(5)]
        public void CheckAddressee()
        {
            SaveEmailAsDraft(subject, addressee, body);
            Assert.That(NavigateToTheDraftPage().SelectDraftByItsAddressee(addressee).Text, Is.EqualTo(addressee), "Addressee is not correct");
        }

        [Test, Order(6)]
        public void CheckBody()
        {
            SaveEmailAsDraft(subject, addressee, body);
            var messageScreen = NavigateToTheDraftPage().OpenEmailSavedAsDraft(subject);
            messageScreen.SwitchToFrame();
            var actualBodyEmail = messageScreen.GetBodyEmail();
            messageScreen.ExitFromFrame();
            messageScreen.CloseMessageScreen();
            Assert.That(actualBodyEmail, Is.EqualTo(body), "Message body is not correct");
        }

        [Test, Order(7)]
        public void SentMenuPageContainsSentEmail()
        {
            SaveEmailAsDraft(subject, addressee, body);
            SendEmailFromTheDraftMenu(subject);
            Assert.IsTrue(NavigateToTheSentPage().IsThereAnySentEmail(subject), "Sent email is not exist in the Sent menu page");
        }

        [Test, Order(8)]
        public void DraftPageDoesNotContainSentEmail()
        {
            SaveEmailAsDraft(subject, addressee, body);
            SendEmailFromTheDraftMenu(subject);
            Assert.IsEmpty(NavigateToTheDraftPage().GetDraftSubjects(), "Draft is still exist in the Draft page");
        }

        public void Login()
        {
            mainPage.NavigateToLoginPage().Login(ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"]);
        }

        public void NavigateToMainPage()
        {
            mainPage.OpenProtonMainPage(ConfigurationManager.AppSettings["URL"]);
        }

        public void Logout()
        {
            header.NavigateToProfileDropDown().Logout();
        }

        public DraftPage NavigateToTheDraftPage() => menuItems.NavigateToDraftPage();

        public SentPage NavigateToTheSentPage() => menuItems.NavigateToSentPage();


        public void SendEmailFromTheDraftMenu(string subject)
        {
            NavigateToTheDraftPage().OpenEmailSavedAsDraft(subject).SendEmail();
        }

        public void SaveEmailAsDraft(string subject, string addressee, string body)
        {
            var newMessageScreen = menuItems.OpenNewMessageScreen();
            newMessageScreen.FillEmail(addressee, subject, body);
            newMessageScreen.CloseMessageScreen();
        }

        public void DeleteDrafts()
        {
            if (NavigateToTheDraftPage().GetDraftSubjects().Any())
            {
                if (!draftPage.Toolbar.AreAllEmailsSelected())
                    draftPage.Toolbar.SelectAllEmails();
                draftPage.Toolbar.DeleteAllEmails();
            }
        }

        public void DeleteSentEmails()
        {
            if (NavigateToTheSentPage().GetSentEmailsSubject().Any())
            {
               
                sendPage.Toolbar.SelectAllEmails();
                sendPage.Toolbar.DeleteAllEmails();
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