using System.Collections.ObjectModel;
using LocatorTask.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace LocatorTask.Elements;
 public abstract class HtmlElementDecorator : IWrapsElement
{
    public IWebElement WrappedElement { get; }

    public HtmlElementDecorator(IWebElement element) => this.WrappedElement = element;

    private readonly Actions action = new(Browser.GetDriver());

    private readonly IJavaScriptExecutor executor = (IJavaScriptExecutor)Browser.GetDriver();

    public void ClickWithAction()
    {
        action.Click(WrappedElement).Build().Perform();
    }

    public void RightClick()
    {
        action.ContextClick(WrappedElement).Perform();
    }

    public void JsClick( )
    {
        executor.ExecuteScript("arguments[0].click();", WrappedElement);
    }

    public bool Enabled => throw new NotImplementedException();

    public virtual bool Selected => this.WrappedElement.Selected;

    public bool Displayed => throw new NotImplementedException();


    public void Click()
    {
        WrappedElement.Click();
    }

    public IWebElement FindElement(By by)
    {
        throw new NotImplementedException();
    }

    public ReadOnlyCollection<IWebElement> FindElements(By by)
    {
        throw new NotImplementedException();
    }

    public string Text
    {
        get
        {
            string attribute = this.WrappedElement.GetAttribute("textContent");
            return !string.IsNullOrEmpty(attribute) ? attribute : this.WrappedElement.GetAttribute("value") ?? this.WrappedElement.Text;
        }
    }

    public string GetAttribute(string attributeName) => this.WrappedElement.GetAttribute(attributeName);
}
