using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Specflow.Tests.Selenium
{
    public class FindBy : By
    {
        public static By LabelText(string text)
        {
            return By.XPath(string.Format("//label[text()='{0}']", text));
        }

        public static By Text(string text)
        {
            return By.XPath(string.Format("//*[text()='{0}']", text));
        }

        public static By ButtonText(string text)
        {
            return By.XPath(string.Format("//input[@value='{0}' and @type='submit']", text));
        }
    }
}