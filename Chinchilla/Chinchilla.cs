using System;
using System.Linq;
using Chinchilla.Extensions;
using Chinchilla.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Chinchilla
{
    public class Chinchilla
    {
        private Uri _rootUrl;

        private IWebDriver _browser;
        public IWebDriver Browser { get { return _browser; } }

        private Page _page;
        public Page Page { get { return _page; } }

        public Chinchilla(string rootUrl) : this (WebBrowser.Current, rootUrl){}

        public Chinchilla(IWebDriver browser, string rootUrl)
        {
            _browser = browser;
            _rootUrl = new Uri(rootUrl);
            _page = new Page(_browser);
        }


        public void ClickLink(string selector = null, SelectorType type = SelectorType.Css, string text = null)
        {
            selector = selector ?? ElementType.Button.GetStringValue();
            new Query(_browser, selector, SelectorType.XPath, text: text).Results.First().Click();
        }

        public void ClickButton(string selector = null, SelectorType type = SelectorType.Css, string text = null)
        {
            selector = String.Format("{0}[@value={1}]", selector ?? ElementType.Button.GetStringValue(), text);
            new Query(_browser, selector, SelectorType.XPath).Results.First().Click();
        }

        public void ClickOn(string selector = null, SelectorType type = SelectorType.Css, string text = null)
        {
            selector = selector ?? ElementType.LinkOrButton.GetStringValue();
            new Query(_browser, selector, SelectorType.XPath, text: text).Results.First().Click();
        }

        public void FillIn(string value, string selector = null, SelectorType type = SelectorType.Css, string labelText = null)
        {
            IWebElement element;
            if(selector == null)
            {
                var label = new Query(_browser, ElementType.Label, SelectorType.XPath, text: labelText).Results.First();
                element = _browser.FindElement(By.Id(label.GetAttribute("for")));
            }
            else
            {
                element = new Query(_browser, selector, SelectorType.XPath).Results.First();
            }
            element.SendKeys(value);
        }

        public void Check(string text)
        {
            throw new NotImplementedException();
        }

        public void Choose(string text)
        {
            throw new NotImplementedException();

        }

        public void Uncheck(string text)
        {
            throw new NotImplementedException();

        }

        public void AttachFile(string text)
        {
            throw new NotImplementedException();

        }

        public void Select(string field, string option)
        {
            new SelectElement(new Query(_browser, ElementType.Select).Results.First()).SelectByText(option);
        }


        public void Visit(string relativeUrl)
        {
            var absoluteUrl = new Uri(_rootUrl, relativeUrl);
            _browser.Navigate().GoToUrl(absoluteUrl);
        }

        public string CurrentPath
        {
            get
            {
                return new Uri(_browser.Url).AbsolutePath;
            }
        }
    }
}
