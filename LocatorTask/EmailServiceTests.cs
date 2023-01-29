using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework.Internal.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace LocatorTask
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver driver;
        private  string _url;
        WebDriverWait wait;
        private string subject;
        private string addressee;
        private string body;
        private string username;
        private string password;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            _url = "https://proton.me/";
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            addressee = "vitakennedy@gmail.com";
            body = "This is draft message";
            subject = "draft_subject";
            username = "vitaktoriia@proton.me";
            password= "V_1234567*"; 
            driver.Navigate().GoToUrl(_url);
            Login();
        }

        [SetUp]
        public void Setup()
        {
           
        }

        [Test,Order(1)]
        public void IsUserLoggedIn()
        {
            var welcomeLabel = driver.FindElement(By.CssSelector("div[class='mauto text-center max-w30e'] h1"));
            Assert.True(welcomeLabel.Displayed);
        }

        [Test, Order(2)]
        public void IsEmailSavedAsDraft()
        {
            SaveEmailAsDraft(subject, addressee);
            NavigateToTheDraftMenu();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[title='Drafts']")));
            IList<IWebElement> draftSubjects = driver.FindElements(By.CssSelector(".item-subject span"));
            Assert.IsNotNull(draftSubjects, "There are no drafts");
            Assert.IsTrue(draftSubjects.Any(draft => draft.GetAttribute("title") == subject), "Emails is not saved as draft");
        }

        [Test, Order(3)]
        public void CheckSubject()
        {
            NavigateToTheDraftMenu();
            wait.Until(ExpectedConditions.UrlToBe("https://mail.proton.me/u/0/drafts"));
            IList<IWebElement> draftSubjects = driver.FindElements(By.CssSelector(".item-subject span"));
            var actualSubject = draftSubjects.FirstOrDefault(draft => draft.GetAttribute("title") == subject).Text;
            Assert.That(actualSubject, Is.EqualTo(subject), "Subject is not correct");
        }

        [Test, Order(4)]
        public void CheckAddressee()
        {
            NavigateToTheDraftMenu();
            wait.Until(ExpectedConditions.UrlToBe("https://mail.proton.me/u/0/drafts"));
            IList<IWebElement> draftAddressees = driver.FindElements(By.CssSelector(".item-senders span"));
            var actualAddressees = draftAddressees.FirstOrDefault(draft => draft.GetAttribute("title") == addressee).Text;
            Assert.That(actualAddressees, Is.EqualTo(addressee), "Addressee is not correct");
        }

        [Test, Order(5)]
        public void CheckBody()
        {
            NavigateToTheDraftMenu();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[title='Drafts']")));
            IList<IWebElement> draftSubjects = driver.FindElements(By.CssSelector(".item-subject span"));
            draftSubjects.FirstOrDefault(draft => draft.GetAttribute("title") == subject).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//iframe[contains(@title,'Email composer')]")));
            driver.SwitchTo()
                .Frame(driver.FindElement(By.XPath("//iframe[contains(@title,'Email composer')]")));
            Thread.Sleep(1000);
            var actualBody = driver.FindElement(By.XPath("//div[@id='rooster-editor']")).Text;
            driver.SwitchTo().DefaultContent();
            var closeButton = driver.FindElement(By.CssSelector(".composer-title-bar button:nth-child(4)"));
            closeButton.Click();
            Assert.That(actualBody, Is.EqualTo(body), "Body is not correct");

        }

        [Test, Order(6)]
        public void SentMenuPageContainsSentEmail()
        {
            SendEmailFromTheDraftMenu(subject);
            var sentMenuButton = driver.FindElement(By.CssSelector("a[title*='Sent']"));
            sentMenuButton.Click();
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".item-subject span")));
            IList<IWebElement> sentEmailSubjects = driver.FindElements(By.CssSelector(".item-subject span"));
            Assert.IsTrue(sentEmailSubjects.Any(draft => draft.GetAttribute("title") == subject), "Sent email is not exist in the Sent menu page");
        }

        [Test, Order(7)]
        public void DraftMenuItemDoesNotContainSentEmail()
        {
            NavigateToTheDraftMenu();
            wait.Until(ExpectedConditions.UrlToBe("https://mail.proton.me/u/0/drafts"));
            if (driver.FindElements(By.CssSelector(".item-subject span")).Any())
            {
                IList<IWebElement> draftSubjects = driver.FindElements(By.CssSelector(".item-subject span"));
                Assert.IsFalse(draftSubjects.Any(draft => draft.GetAttribute("title") == subject), "Draft is still exist in the Draft menu page");
            }
            else
            {
                Assert.True(true);
            }
        }

        public void Login()
        {
            var signInButton = driver.FindElement(By.CssSelector("div.ml-auto a:nth-child(1)"));
            signInButton.Click();
            var usernameInputField = driver.FindElement(By.Id("username"));
            var passwordInputField = driver.FindElement(By.Id("password"));
            var submitSigninButton = driver.FindElement(By.CssSelector("button[class='button w100 button-large button-solid-norm mt1-5']"));
            usernameInputField.SendKeys(username);
            passwordInputField.SendKeys(password);
            submitSigninButton.Click();
        }

        public void Logout()
        {
            var profileButton = driver.FindElement(By.CssSelector("button.user-dropdown-button"));
            profileButton.Click();
            var signoutButton = driver.FindElement(By.XPath("//button[text()='Sign out']"));
            signoutButton.Click();
            var signInLabel = driver.FindElement(By.XPath("//h1[text()='Sign in']"));
            //Assert.IsTrue(signInLabel.Displayed, "User is not log out");
        }

        public void NavigateToTheDraftMenu()
        {
            var draftMenuButton = driver.FindElement(By.CssSelector("a[title*='Draft']"));
            draftMenuButton.Click();
        }

        public void SendEmailFromTheDraftMenu(string subject)
        {
            NavigateToTheDraftMenu();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h3[text()='Drafts']")));
            IList<IWebElement> draftSubjects = driver.FindElements(By.CssSelector(".item-subject span"));
            draftSubjects.FirstOrDefault(draft => draft.GetAttribute("title") == subject).Click();
            var sendButton = driver.FindElement(By.CssSelector(".composer-send-button"));
            sendButton.Click();
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".composer-send-button")));
        }

        public void SaveEmailAsDraft(string subject, string addressee)
        {
            var newMessageButton = driver.FindElement(By.CssSelector("button[class='button button-large button-solid-norm w100 no-mobile']"));
            newMessageButton.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[placeholder*='Email address']")));
            driver.FindElement(By.CssSelector("input[placeholder*='Email address']")).SendKeys(addressee);
            var subjectInputField = driver.FindElement(By.XPath("//input[@class='field-two-input w100']"));
            subjectInputField.SendKeys(subject);
            driver.SwitchTo()
                .Frame(driver.FindElement(By.XPath("//iframe[contains(@title,'Email composer')]")));
            var bodyInputField = driver.FindElement(By.Id("rooster-editor"));
            bodyInputField.Clear();
            bodyInputField.SendKeys(body);
            driver.SwitchTo().DefaultContent();
            var closeButton = driver.FindElement(By.CssSelector(".composer-title-bar button:nth-child(4)"));
            closeButton.Click();
            //wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector("input[placeholder*='Email address']")));
        }

        public void DeleteDrafts()
        {
            NavigateToTheDraftMenu();
            wait.Until(ExpectedConditions.UrlToBe("https://mail.proton.me/u/0/drafts"));
            if (driver.FindElements(By.CssSelector(".item-subject span")).Any())
            {
                var checkboxSelectAll = driver.FindElement(By.CssSelector("[for=idSelectAll]"));
                Thread.Sleep(2000);
                checkboxSelectAll.Click();
                var deleteAllDraftsButton = driver.FindElement(By.CssSelector("button[data-testid='toolbar:movetotrash']"));
                deleteAllDraftsButton.Click();
            }
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//figcaption/h3[text()='No messages found']")));
        }

        public void DeleteSentEmails()
        {
            var sentMenuButton = driver.FindElement(By.CssSelector("a[title*='Sent']"));
            sentMenuButton.Click();
            wait.Until(ExpectedConditions.UrlToBe("https://mail.proton.me/u/0/sent"));
            if (driver.FindElements(By.CssSelector(".item-subject span")).Any())
            {
                var checkboxSelectAll = driver.FindElement(By.CssSelector("[for=idSelectAll]"));
                Thread.Sleep(2000);
                checkboxSelectAll.Click();
                var deleteAllDraftsButton = driver.FindElement(By.CssSelector("button[data-testid='toolbar:movetotrash']"));
                deleteAllDraftsButton.Click();
            }
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//figcaption/h3[text()='No messages found']")));
        }

        [TearDown]
        public void Clenaup()
        {
           
        }

        [OneTimeTearDown]
        public void TestFinalize()
        {
           DeleteDrafts();
           DeleteSentEmails();
           Logout();
           driver.Close();
        }
    }
}