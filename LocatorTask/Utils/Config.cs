using LocatorTask.PageObject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LocatorTask.Utils;
public static class Config
{
    public static void SetUpConfigFile()
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
}
