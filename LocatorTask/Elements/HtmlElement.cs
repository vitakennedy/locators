using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorTask.Elements;
public class HtmlElement : HtmlElementDecorator
{
    public HtmlElement(IWebElement element) : base(element)
    {
    }
}
