using log4net;
using OpenQA.Selenium;
using ReportPortal.Shared;

namespace LocatorTask.Utils;

    public static class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Logger));

        public static void Info(string message)
        {
            Log.Info(message);
        }

        public static void Debug(string message)
        {
            Log.Debug(message);
        }

        public static void Warn(string message)
        {
            Log.Warn(message);
        }

        public static void Error(string message)
        {
            Log.Error(message);
        }

        public static void Exception(Exception exception)
        {
            Log.Debug($"Exception message: {exception.Message ?? "empty"}");
            Log.Debug($"Exception stacktrace:{Environment.NewLine}{exception.StackTrace ?? "empty"} ");
        }

        public static void AttachScreenshot(string message, byte[] data) =>
            Context.Current.Log.Debug($"{message}:", "image/png", data);

        public static void SavingScreenshot(Screenshot screenshot)
        {
            Debug("Saving screenshot");
            screenshot.SaveAsFile("C:\\Users\\Viktoriia_Sherstiuk\\Desktop\\ATM\\Locators\\Task\\locators\\Log\\Screenshot.png", ScreenshotImageFormat.Png);
        }
}