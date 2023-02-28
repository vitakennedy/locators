using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorTask.Elements;
internal class Checkbox : HtmlElementDecorator
{
    public Checkbox(IWebElement element) : base(element)
    {
    }
}
