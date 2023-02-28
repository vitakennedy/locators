using LocatorTask.PageObject;

namespace LocatorTask.Utils.Login;

internal class UILogin : LoginPage, ILoginStrategy
{
    public void Login(string username, string password)
    {
        waiter.Until((driver) => usernameInputField.Displayed);
        usernameInputField.SendKeys(username);
        passwordInputField.SendKeys(password);
        submitSigninButton.Click();
    }
}