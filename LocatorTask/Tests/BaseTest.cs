using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.Reflection;

namespace LocatorTask.Tests;
public class BaseTest
{
    protected IWebDriver _driver;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _driver = new ChromeDriver();
        _driver.Manage().Window.Maximize();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        SetUpConfigFile();
    }
    protected void SetUpConfigFile()
    {
        var appConfigPath = Assembly.GetExecutingAssembly().Location + ".config";

        if (!File.Exists(appConfigPath))
            return;

        var appConfig = ConfigurationManager.OpenMappedExeConfiguration(
            new ExeConfigurationFileMap { ExeConfigFilename = appConfigPath }, ConfigurationUserLevel.None);

        var activeConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        if (appConfig == activeConfig)
            return;

        activeConfig.AppSettings.Settings.Clear();

        foreach (var key in appConfig.AppSettings.Settings.AllKeys)
            activeConfig.AppSettings.Settings.Add(appConfig.AppSettings.Settings[key]);

        activeConfig.Save();

        ConfigurationManager.RefreshSection("appSettings");
    }

    [OneTimeTearDown]
    public void TestFinalize()
    {
        _driver.Close();
    }
}
