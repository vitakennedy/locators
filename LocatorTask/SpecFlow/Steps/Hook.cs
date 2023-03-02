using LocatorTask.Utils;
using LocatorTask.WebDriver;
using TechTalk.SpecFlow;

namespace LocatorTask.SpecFlow.Steps;
[Binding]
public class Hook
{
    protected static Browser BrowserInstance1;

    [BeforeScenario]
    public void BeforeScenarioWithTag()
    {
        Config.SetUpConfigFile();
        BrowserInstance1 = Browser.Instance;
        Browser.WindowMaximise();
        Browser.GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
    }

    [AfterScenario]
    public static void AfterScenario()
    {
        if (Browser.GetDriver() != null)
        {
            Browser.Quit();
        }
        else throw new Exception("There was an error while trying to close the driver");
    }
}