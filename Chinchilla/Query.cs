using System.Collections.Generic;
using System.Linq;
using Chinchilla.Attributes;
using Chinchilla.Extensions;
using OpenQA.Selenium;

namespace Chinchilla
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
            Selector = selector.GetStringValue();
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

    }
    public enum SelectorType
    {
        Css,
        XPath
    }
    public enum ElementType
    {
        [StringValue("//a")]
        Link,
        [StringValue("//button | //input")]
        Button,
        [StringValue("//input")]
        Input,
        [StringValue("//label")]
        Label,
        [StringValue("//select")]
        Select,
        [StringValue("//a | //button | //input")]
        LinkOrButton,
        [StringValue("//*")]
        Unknown
    }


}
