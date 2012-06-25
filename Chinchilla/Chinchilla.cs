using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MJD.Extensions;
using MJD.Selenium;
using MJD.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MJD
{
    public class Chinchilla
    {
        private Uri _rootUrl;

        private IWebDriver _browser;
        public IWebDriver Browser { get { return _browser; } }

        private Page _page;
        public Page Page { get { return _page; } }

        public Chinchilla(IWebDriver browser, string rootUrl)
        {
            _browser = browser;
            _rootUrl = new Uri(rootUrl);
            _page = new Page(_browser);
        }


        public void ClickLink(string locator)
        {
            ClickLink(locator, locator);
        }
        public void ClickLink(string text = null, string id = null)
        {
            var results = new List<IWebElement>();

            if(id != null)
            {
                results = results.Union(_browser.FindElements(By.CssSelector(string.Format("a#{0}", id)))).ToList();
            }

            if (text != null)
            {
                results = results.Union(_browser.FindElements(By.CssSelector("a")).Where(el => el.Text == text)).ToList();
            }

            if(results.Count > 1)
            {
                throw new MoreThanOneElementFoundException();
            }
            if (results.Count == 0)
            {
                throw new NoElementsFoundException();
            } 
            results.First().Click();
        }

        public void ClickButton(string locator)
        {
            ClickButton(locator, locator);
        }
        public void ClickButton(string text = null, string id = null)
        {
            var results = new List<IWebElement>();

            if (id != null)
            {
                results = results.Union(_browser.FindElements(By.CssSelector(string.Format("button#{0}, input[type='submit']#{0}, input[type='button']#{0}", id)))).ToList();
            }

            if (text != null)
            {
                results = results.Union(_browser.FindElements(By.CssSelector("button, input[type='submit'], input[type='button']")).Where(el => el.Text == text)).ToList();
            }

            if (results.Count > 1)
            {
                throw new MoreThanOneElementFoundException();
            }
            results.First().Click();
        }

        public void ClickOn()
        {
            throw new NotImplementedException();
        }

        public void FillIn()
        {

            throw new NotImplementedException();
        }

        public void FillIn(string value, string labelText)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
