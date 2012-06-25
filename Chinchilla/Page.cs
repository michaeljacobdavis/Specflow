using System;
using OpenQA.Selenium;

namespace MJD
{
    public class Page
    {
        public Page(IWebDriver browser)
        {
            _browser = browser;
        }

        private IWebDriver _browser { get; set; }

        //public bool HasSelector(string path, int count = 1, bool visible = true, string text = null)
        //{
        //    return HasSelector(path, SelectorType.Css, count, visible, text);
        //}

        //public bool HasNoSelector(string path, int count = 1, bool visible = true, string text = null)
        //{
        //    return HasSelector(path, SelectorType.Css, count, visible, text);
        //}

        //public bool HasNoSelector(string path, SelectorType selector, int count = 1, bool visible = true, string text = null)
        //{
        //    return !HasSelector(path, selector, count, visible, text);
        //}

        //public bool HasSelector(string path, SelectorType selector, int count = 1, bool visible = true, string text = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Has(string path)
        //{
        //    return HasSelector(path, SelectorType.Css);
        //}

        //public bool Has(string path, SelectorType selector)
        //{
        //    return HasSelector(path, SelectorType.XPath);
        //}

        public bool HasContent(string content)
        {
            return _browser.PageSource.Contains(content);
        }

        public bool HasNoContent(string content)
        {
            return !_browser.PageSource.Contains(content);
        }

        

    }
}