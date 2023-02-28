using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorTask.Elements;
public class Button : HtmlElementDecorator
{
    public Button(IWebElement element) : base(element)
    {
    }


}
