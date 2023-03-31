using LocatorTask.Utils;
using LocatorTask.WebDriver;
using log4net.Config;
using log4net;
using OpenQA.Selenium.Support.Extensions;
using System.Reflection;
using TechTalk.SpecFlow;

namespace LocatorTask.SpecFlow.Steps;
[Binding]
public class Hook : TechTalk.SpecFlow.Steps
{
    protected  Browser BrowserInstance1;

    [BeforeScenario]
    public void BeforeScenarioWithTag()
    {
        Config.SetUpConfigFile();
        BrowserInstance1 = Browser.Instance;
        Browser.WindowMaximise();
        Browser.GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
    }

    [AfterScenario]
    public void AfterScenario()
    {
        if (Browser.GetDriver() != null)
        {
            try
            {
                Browser.Quit();
            }
            catch (Exception e)
            {
                Logger.Error($"Unable to Quit the browser. Reason: {e.Message}");
            }
        }
        else throw new Exception("There was an error while trying to close the driver");
    }

    [AfterScenario(Order = -30000)]
    public void TakeScreenshotOnError()

    {
        if (ScenarioContext.TestError != null)
            TakeScreenshot("Screenshot");
    }

    private void TakeScreenshot(string message)
    {
        try
        {
           var screenshot = Browser.GetDriver().TakeScreenshot();
           Logger.SavingScreenshot(screenshot);
           var screenshotToBytes = screenshot.AsByteArray;
           Logger.AttachScreenshot(message, screenshotToBytes);
        }
        catch (Exception e)
        {
            Logger.Exception(new Exception("Failed to make screenshot via selenium..."));
        }
    }
}