using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Specflow.Tests.Selenium;
using Specflow.Tests.StepDefinitions;


namespace CLC.Web.UiTests.StepDefinitions
{
    public class Chinchilla
    {
        private Uri _rootUrl { get { return new Uri(ConfigurationManager.AppSettings["RootUrl"]); } }
        protected IWebDriver _browser
        {
            get { return WebBrowser.Current; }
        }

        private Page _page;

        public Page Page { get { return _page; } }

        public Chinchilla()
        {
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

        public void Select(string field, string value)
        {
            new SelectElement(new Query(_browser, ElementType.Select).Results.First()).SelectByText(value);
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
                return new Uri(WebBrowser.Current.Url).AbsolutePath;
            }
        }
    }
}
