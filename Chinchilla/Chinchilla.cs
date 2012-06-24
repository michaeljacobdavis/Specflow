using System;
using System.Linq;
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


        public void ClickLink(string text)
        {
            var a = new Query(_browser, ElementType.Link, SelectorType.XPath, text: text).Results;
             a.First().Click();
        }

        public void ClickButton(string text)
        {
            _browser.FindElement(FindBy.ButtonText(text)).Click();
        }

        public void ClickOn(string text)
        {
            throw new NotImplementedException();

        }

        public void FillIn(string field, string value)
        {
            var label = new Query(_browser, ElementType.Label, SelectorType.XPath, text: field).Results.First();
            _browser.FindElement(By.Id(label.GetAttribute("for"))).SendKeys(value);
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
