using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Specflow.Tests.StepDefinitions
{
    public class Query
    {
        public Query(IWebDriver browser, string selector, SelectorType locator = SelectorType.Css, string text = null)
        {
            _browser = browser;
            Selector = selector;
            Locator = locator;
            Text = text;
        }

        public Query(IWebDriver browser, ElementType selector, SelectorType locator = SelectorType.XPath, string text = null)
        {
            _browser = browser;
            Selector = XPathName[selector];
            Locator = locator;
            Text = text;
        }

        private IWebDriver _browser;

        private string Selector { get; set; }

        private SelectorType Locator { get; set; }

        private int Count { get; set; }

        private string Text { get; set; }

        private void Find()
        {
            ICollection<IWebElement> results;
            if (Locator == SelectorType.Css)
            {
                results = _browser.FindElements(By.CssSelector(Selector));
            }
            else
            {
                results = _browser.FindElements(By.XPath(Selector));
            }
            if (Text != null)
            {
                results = results.Where(e => e.Text == Text).ToList();
            }
            _results = results;
        }

        private bool Visible { get; set; }

        private ICollection<IWebElement> _results;

        public ICollection<IWebElement> Results
        {
            get
            {
                if (_results == null)
                {
                    Find();
                }
                return _results;
            }
        }

        private Dictionary<ElementType, string> XPathName = new Dictionary<ElementType, string>
        {
            {ElementType.Link, "//a"},
            {ElementType.Button, "//button | //input"},
            {ElementType.Input, "//input"},
            {ElementType.Label, "//label"},
            {ElementType.Select, "//select"},
            {ElementType.Unknown, "//*"}
        };

    }
    public enum SelectorType
    {
        Css,
        XPath
    }
    public enum ElementType
    {
        Link,
        Button,
        Input,
        Label,
        Select,
        Unknown
    }


}
