using LocatorTask.Entities;
using LocatorTask.PageObject;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace LocatorTask.Elements;

public class MessageScreen : BasePage
{
    public MessageScreen() : base() { }

    [FindsBy(How = How.CssSelector, Using = "input[placeholder*='Email address']")]
    private IWebElement addresseeInputField;

    [FindsBy(How = How.XPath, Using = "//input[@class='field-two-input w100']")]
    private IWebElement subjectInputField;

    [FindsBy(How = How.XPath, Using = "//iframe[contains(@title,'Email composer')]")]
    private IWebElement bodyFrame;

    [FindsBy(How = How.Id, Using = "rooster-editor")]
    private IWebElement bodyInputField;

    [FindsBy(How = How.CssSelector, Using = ".composer-title-bar button:nth-child(4)")]
    private IWebElement closeButton;

    [FindsBy(How = How.CssSelector, Using = ".composer-send-button")]
    private IWebElement sendButton;

    public void SwitchToFrame()
    {
        waiter.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[contains(@title,'Email composer')]")));
    }

    public void ExitFromFrame()
    {
        driver.SwitchTo().DefaultContent();
    }

    public void FillEmail(Email email)
    {
        addresseeInputField.SendKeys(email.Addressee);
        subjectInputField.SendKeys(email.Subject);
        SwitchToFrame();
        bodyInputField.Clear();
        bodyInputField.SendKeys(email.Body);
        ExitFromFrame();
    }

    public void CloseMessageScreen()
    {
        closeButton.Click();
    }

    public string GetBodyEmail()
    {
        return bodyInputField.Text;
    }

    public void SendEmail()
    {
        sendButton.Click();
        waiter.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".composer-title-bar button:nth-child(4)")));
    }
}