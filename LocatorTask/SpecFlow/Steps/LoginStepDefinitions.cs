using LocatorTask.Entities;
using LocatorTask.PageObject;
using LocatorTask.Tests;
using LocatorTask.Utils.Login;
using NUnit.Framework;
using System.Configuration;
using LocatorTask.WebDriver;
using TechTalk.SpecFlow;

namespace LocatorTask.SpecFlow.Steps
{
    [Binding]
    public class LoginStepDefinitions : Hook
    {
        public MainPage MainPage = new();
        public InboxPage InboxPage = new();
        public LoginPage LoginPage = new();

        private static ScenarioContext context;

        public LoginStepDefinitions(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Given(@"an user navigates to the main page")]
        public void GivenAnUserNavigatesToTheMainPage()
        {
            Browser.NavigateTo(ConfigurationManager.AppSettings["URL"]);
        }

        [StepDefinition(@"an user clicks 'Sign in' button")]
        public void GivenAnUserClicksButton()
        {
            MainPage.ClickSignInButton();
        }

        [When(@"an user submits '(.*)' and '(.*)'")]
        public void WhenAnUserSubmitsUsernameAndPassword(string username, string password)
        {
            LoginPage.Login(new UILogin(), username, password);
        }

        [Then(@"an user should( not|) be redirected to the '(.*)' page")]
        public void ThenAnUserShouldBeRedirectedToThePage(string value, string url)
        {
            switch (value)
            {
                case " not": Assert.That(BasePage.IsPageUrlContaining(url.ToLower()), Is.False); break;
                default: Assert.That(BasePage.IsPageUrlContaining(url.ToLower()), Is.True); break;
            }
        }

        [Then(@"un user should see 'Welcome' label")]
        public void ThenUnUserShouldSeeLabel()
        {
            Assert.IsTrue(InboxPage.IsWelcomeLabelDisplayed(), "User is not signed in");
        }

        [Then(@"an user should stay on the 'Sign in' page")]
        public void ThenUnUserShouldSeErrorMessage()
        {
            Assert.IsTrue(LoginPage.IsLabelDisplayed());
        }
    }
}