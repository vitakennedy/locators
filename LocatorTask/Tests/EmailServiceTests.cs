using System.Configuration;
using LocatorTask.Blocks;
using LocatorTask.Entities;
using LocatorTask.PageObject;
using LocatorTask.Utils;
using LocatorTask.Utils.Login;
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
            subject = "draft_subject";
            body = "This is draft message";
            NavigateToMainPage();
            Login();
        }

        [Test, Order(1)]
        public void IsUserLoggedIn()
        {
            Assert.IsTrue(inboxPage.IsWelcomeLabelDisplayed(), "User is not signed in");
        }

        [Test, TestCaseSource(nameof(GetDataFromCsv)), Order(2)]
        public void IsEmailSavedAsADraft( string addressee1, string subject1, string body1)
        {
            SaveEmailAsDraft(addressee1, subject1, body1);
            Assert.IsNotNull(NavigateToTheDraftPage().GetDraftSubjects(), "There are no saved drafts on the Draft page");
            Assert.IsTrue(NavigateToTheDraftPage().IsThereAnyDraft(subject1), "Email is not saved as a draft");
        }

        [Test, Order(3)]
        public void IsContextMenuDisplayed()
        {
            SaveEmailAsDraft(addressee, subject, body);
            NavigateToTheDraftPage().OpenContextMenu(subject);
            Assert.IsTrue(draftPage.IsContextMenuDisplayed(), "Context Menu is not displayed");
        }

        [Test, Order(4)]
        public void CheckSubject()
        {
            SaveEmailAsDraft(addressee, subject, body);
            Assert.That(NavigateToTheDraftPage().SelectDraftByItsSubject(subject).Text, Is.EqualTo(subject), "Subject is not correct");
        }

        [Test, Order(5)]
        public void CheckAddressee()
        {
            SaveEmailAsDraft(addressee, subject, body);
            Assert.That(NavigateToTheDraftPage().SelectDraftByItsAddressee(addressee).Text, Is.EqualTo(addressee), "Addressee is not correct");
        }

        [Test, Order(6)]
        public void CheckBody()
        {
            SaveEmailAsDraft(addressee, subject, body);
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
            SaveEmailAsDraft(addressee, subject, body);
            SendEmailFromTheDraftMenu(subject);
            Assert.IsTrue(NavigateToTheSentPage().IsThereAnySentEmail(subject), "Sent email is not exist in the Sent menu page");
        }

        [Test, Order(8)]
        public void DraftPageDoesNotContainSentEmail()
        {
            SaveEmailAsDraft(addressee, subject, body);
            SendEmailFromTheDraftMenu(subject);
            Assert.IsEmpty(NavigateToTheDraftPage().GetDraftSubjects(), "Draft is still exist in the Draft page");
        }

        public void Login()
        {
           var username= ConfigurationManager.AppSettings["username"];
           var password= ConfigurationManager.AppSettings["password"];

           mainPage.NavigateToLoginPage().Login(new UILogin(), username, password);

           var user = new User(username, password);
        }

        public void NavigateToMainPage()
        {
           Browser.NavigateTo(ConfigurationManager.AppSettings["URL"]);
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

        public void SaveEmailAsDraft(string addressee, string subject, string body)
        {
            var email = new Email(addressee, subject, body);
            var newMessageScreen = menuItems.OpenNewMessageScreen();
            newMessageScreen.FillEmail(email);
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

        private static IEnumerable<string[]> GetDataFromCsv()
        {
            var reader = CsvReader.GetReader;
            while (reader.Next())
            {
                var column1 = (reader[0]);
                var column2 = (reader[1]);
                var column3 = (reader[2]);
                yield return new [] { column1, column2, column3 };
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