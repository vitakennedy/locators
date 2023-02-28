using LocatorTask.Utils;
using LocatorTask.WebDriver;
using NUnit.Framework;

namespace LocatorTask.Tests;
public class BaseTest
{
    protected static Browser Browser;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Config.SetUpConfigFile();
        Browser = Browser.Instance;
        Browser.WindowMaximise();
        Browser.GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
    }

    [OneTimeTearDown]
    public void TestFinalize()
    {
        Browser.Quit();
    }
}
