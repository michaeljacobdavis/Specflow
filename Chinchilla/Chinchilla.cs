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

        protected ISearchContext SearchContext { get; set; }

        private Page _page;
        public Page Page { get { return _page; } }

        public Chinchilla(IWebDriver browser, string rootUrl)
        {
            _browser = browser;
            _rootUrl = new Uri(rootUrl);
            _page = new Page(_browser);
            SearchContext = browser;

            Browser.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
        }

        #region Actions

        public void AttachFile(string value, string locator)
        {
            AttachFile(value, locator, locator, locator);
        }
        public void AttachFile(string value, string labelText = null, string id = null, string name = null)
        {
            throw new NotImplementedException();
        }

        public void ClickLink(string locator)
        {
            ClickLink(locator, locator);
        }
        public void ClickLink(string text = null, string id = null)
        {
            var results = new List<IWebElement>();

            if (id != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("a#{0}", id)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (text != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector("a")).Where(el => el.Text == text)).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (results.Count > 1)
            {
                throw new MoreThanOneElementFoundException();
            }
            if (results.Count == 0)
            {
                throw new NoElementsFoundException();
            }
            results.Validate().First().Click();
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
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("button#{0}, input[type='submit']#{0}, input[type='button']#{0}", id)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (text != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector("button, input[type='submit'], input[type='button']")).Where(el => el.Text == text || el.GetAttribute("value") == text)).ToList();
                }
                catch (InvalidOperationException) { }
            }
            results.Validate().First().Click();
        }

        public void ClickOn(string locator)
        {
            ClickOn(locator, locator);
        }
        public void ClickOn(string text = null, string id = null)
        {
            var results = new List<IWebElement>();

            if (id != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("a#{0}, button#{0}, input[type='submit']#{0}, input[type='button']#{0}", id)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (text != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector("a, button, input[type='submit'], input[type='button']")).Where(el => el.Text == text)).ToList();

                }
                catch (InvalidOperationException) { }
            }

            results.Validate().First().Click();
        }

        public void FillIn(string value, string locator)
        {
            FillIn(value, locator, locator, locator);
        }
        public void FillIn(string value, string labelText = null, string id = null, string name = null)
        {
            var results = new List<IWebElement>();

            if (id != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("input#{0}", id)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (name != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("input[name='{0}']", name)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (labelText != null)
            {
                var matchingLabels = results.Union(SearchContext.FindElements(By.CssSelector("label")).Where(el => el.Text == labelText)).ToList();
                results = results.Union(
                    from label
                    in matchingLabels
                    select label.GetAttribute("for")
                        into inputId
                        where inputId != null
                        select _browser.FindElement(By.CssSelector((string.Format("input#{0}", inputId))))).ToList();

                //foreach (var label in matchingLabels)
                //{
                //    var inputId = label.GetAttribute("for");
                //    if (inputId != null)
                //    {
                //        matchingInputs.Add(_browser.FindElement(By.CssSelector((string.Format("input#{0}", inputId)))));
                //    }

                //}
            }
            results.Validate().First().SendKeys(value);
        }

        public void Check(string locator)
        {
            Check(locator, locator, locator);
        }
        public void Check(string labelText = null, string id = null, string name = null)
        {
            var results = new List<IWebElement>();

            if (id != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("input[type=checkbox]#{0}", id)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (name != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("input[type='checkbox'][name='{0}']", name)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (labelText != null)
            {
                var matchingLabels = results.Union(SearchContext.FindElements(By.CssSelector("label")).Where(el => el.Text == labelText)).ToList();
                results = results.Union(
                    from label
                    in matchingLabels
                    select label.GetAttribute("for")
                        into inputId
                        where inputId != null
                        select _browser.FindElement(By.CssSelector((string.Format("input[type='checkbox']#{0}", inputId))))).ToList();
            }
            var element = results.Validate().First();
            if (!element.Selected)
            {
                element.Click();
            }
        }

        public void UnCheck(string locator)
        {
            UnCheck(locator, locator, locator);
        }
        public void UnCheck(string labelText = null, string id = null, string name = null)
        {
            var results = new List<IWebElement>();

            if (id != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("input[type=checkbox]#{0}", id)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (name != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("input[type='checkbox'][name='{0}']", name)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (labelText != null)
            {
                var matchingLabels = results.Union(SearchContext.FindElements(By.CssSelector("label")).Where(el => el.Text == labelText)).ToList();
                results = results.Union(
                    from label
                    in matchingLabels
                    select label.GetAttribute("for")
                        into inputId
                        where inputId != null
                        select _browser.FindElement(By.CssSelector((string.Format("input[type='checkbox']#{0}", inputId))))).ToList();

            }
            var element = results.Validate().First();
            if (element.Selected)
            {
                element.Click();
            }
        }

        public void Choose(string locator)
        {
            Choose(locator, locator, locator);
        }
        public void Choose(string labelText = null, string id = null, string name = null)
        {
            var results = new List<IWebElement>();

            if (id != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("input[type=radio]#{0}", id)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (name != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("input[type='radio'][name='{0}']", name)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (labelText != null)
            {
                var matchingLabels = results.Union(SearchContext.FindElements(By.CssSelector("label")).Where(el => el.Text == labelText)).ToList();
                results = results.Union(
                    from label in matchingLabels
                    select label.GetAttribute("for")
                        into inputId
                        where inputId != null
                        select _browser.FindElement(By.CssSelector((string.Format("input[type='radio']#{0}", inputId))))).ToList();
            }
            var element = results.Validate().First();
            element.Click();
        }

        public void Select(string value, string locator)
        {
            Select(value, locator, locator, locator);
        }
        public void Select(string value, string labelText = null, string id = null, string name = null)
        {
            var results = new List<IWebElement>();

            if (id != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("select#{0}", id)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (name != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("select[name='{0}']", name)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (labelText != null)
            {
                var matchingLabels = results.Union(SearchContext.FindElements(By.CssSelector("label")).Where(el => el.Text == labelText)).ToList();
                results = results.Union(
                    from label
                    in matchingLabels
                    select label.GetAttribute("for")
                        into inputId
                        where inputId != null
                        select _browser.FindElement(By.CssSelector((string.Format("select#{0}", inputId))))).ToList();
            }

            new SelectElement(results.Validate().First()).SelectByText(value);
        }

        public void UnSelect(string value, string locator)
        {
            UnSelect(value, locator, locator, locator);
        }
        public void UnSelect(string value, string labelText = null, string id = null, string name = null)
        {
            var results = new List<IWebElement>();

            if (id != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("select#{0}", id)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (name != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("select[name='{0}']", name)))).ToList();
                }
                catch (InvalidOperationException) { }
            }

            if (labelText != null)
            {
                var matchingLabels = results.Union(SearchContext.FindElements(By.CssSelector("label")).Where(el => el.Text == labelText)).ToList();
                results = results.Union(
                    from label
                    in matchingLabels
                    select label.GetAttribute("for")
                        into inputId
                        where inputId != null
                        select _browser.FindElement(By.CssSelector((string.Format("select#{0}", inputId))))).ToList();
            }

            new SelectElement(results.Validate().First()).DeselectByText(value);
        }
        #endregion

        #region Querying
        public bool HasButton()
        {
            throw new NotImplementedException();
        }
        public bool HasCheckedField()
        {
            throw new NotImplementedException();
        }
        public bool HasCss()
        {
            throw new NotImplementedException();
        }
        public bool HasField()
        {
            throw new NotImplementedException();
        }
        public bool HasLink()
        {
            throw new NotImplementedException();
        }
        public bool HasNoButton()
        {
            throw new NotImplementedException();
        }
        public bool HasNoCheckedField()
        {
            throw new NotImplementedException();
        }
        public bool HasNoCss()
        {
            throw new NotImplementedException();
        }
        public bool HasNoField()
        {
            throw new NotImplementedException();
        }
        public bool HasNoLink()
        {
            throw new NotImplementedException();
        }
        public bool HasNoSelect()
        {
            throw new NotImplementedException();
        }
        public bool HasNoSelector()
        {
            throw new NotImplementedException();
        }
        public bool HasNoTable()
        {
            throw new NotImplementedException();
        }
        public bool HasNoText()
        {
            throw new NotImplementedException();
        }
        public bool HasNoUncheckedField()
        {
            throw new NotImplementedException();
        }
        public bool HasNoXPath()
        {
            throw new NotImplementedException();
        }
        public bool HasSelect()
        {
            throw new NotImplementedException();
        }
        public bool HasSelector()
        {
            throw new NotImplementedException();
        }
        public bool HasTable()
        {
            throw new NotImplementedException();
        }
        public bool HasText()
        {
            throw new NotImplementedException();
        }
        public bool HasUncheckedField()
        {
            throw new NotImplementedException();
        }
        public bool HasXPath()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Navigation

        public void Visit(string relativeUrl)
        {
            var absoluteUrl = new Uri(_rootUrl, relativeUrl);
            _browser.Navigate().GoToUrl(absoluteUrl);
        }
        #endregion
        
        #region Scoping

        public void Within(string id, Action action)
        {
            // Todo: add css and xpath selectors. Add driver support
            var originalSearchContext = SearchContext;
            var results = new List<IWebElement>();
            if (id != null)
            {
                try
                {
                    results = results.Union(SearchContext.FindElements(By.CssSelector(string.Format("#{0}", id)))).ToList();
                }
                catch (InvalidOperationException) { }
            }
            SearchContext = results.Validate().First();
            action();
            SearchContext = originalSearchContext;

        }
        #endregion

        #region Current Page
        public string CurrentPath
        {
            get
            {
                return new Uri(_browser.Url).AbsolutePath;
            }
        }


        #endregion
        
    }
}
