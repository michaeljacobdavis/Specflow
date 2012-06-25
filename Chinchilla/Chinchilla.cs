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

        private int timeout = 100;

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
                results = results.Union(_browser.FindElements(By.CssSelector(string.Format("a#{0}", id)), timeout)).ToList();
            }

            if (text != null)
            {
                results = results.Union(_browser.FindElements(By.CssSelector("a"), timeout).Where(el => el.Text == text)).ToList();
            }

            if(results.Count > 1)
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
                results = results.Union(_browser.FindElements(By.CssSelector(string.Format("button#{0}, input[type='submit']#{0}, input[type='button']#{0}", id)), timeout)).ToList();
            }

            if (text != null)
            {
                results = results.Union(_browser.FindElements(By.CssSelector("button, input[type='submit'], input[type='button']"), timeout).Where(el => el.Text == text)).ToList();
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
                results = results.Union(_browser.FindElements(By.CssSelector(string.Format("a#{0}, button#{0}, input[type='submit']#{0}, input[type='button']#{0}", id)), timeout)).ToList();
            }

            if (text != null)
            {
                results = results.Union(_browser.FindElements(By.CssSelector("a, button, input[type='submit'], input[type='button']"), timeout).Where(el => el.Text == text)).ToList();
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
                results = results.Union(_browser.FindElements(By.CssSelector(string.Format("input#{0}", id)), timeout)).ToList();
            }

            if (name != null)
            {
                results = results.Union(_browser.FindElements(By.CssSelector(string.Format("input[name='{0}']", name)), timeout)).ToList();
            }

            if (labelText != null)
            {
                var matchingLabels = results.Union(_browser.FindElements(By.CssSelector("label"), timeout).Where(el => el.Text == labelText)).ToList();
                results = results.Union(
                    from label 
                    in matchingLabels 
                    select label.GetAttribute("for") 
                    into inputId where inputId != null 
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
                results = results.Union(_browser.FindElements(By.CssSelector(string.Format("input[type=checkbox]#{0}", id)), timeout)).ToList();
            }

            if (name != null)
            {
                results = results.Union(_browser.FindElements(By.CssSelector(string.Format("input[type='checkbox'][name='{0}']", name)), timeout)).ToList();
            }

            if (labelText != null)
            {
                var matchingLabels = results.Union(_browser.FindElements(By.CssSelector("label"), timeout).Where(el => el.Text == labelText)).ToList();
                results = results.Union(
                    from label
                    in matchingLabels
                    select label.GetAttribute("for")
                        into inputId
                        where inputId != null
                        select _browser.FindElement(By.CssSelector((string.Format("input[type='checkbox']#{0}", inputId))))).ToList();

                //foreach (var label in matchingLabels)
                //{
                //    var inputId = label.GetAttribute("for");
                //    if (inputId != null)
                //    {
                //        matchingInputs.Add(_browser.FindElement(By.CssSelector((string.Format("input#{0}", inputId)))));
                //    }

                //}
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
                results = results.Union(_browser.FindElements(By.CssSelector(string.Format("input[type=checkbox]#{0}", id)), timeout)).ToList();
            }

            if (name != null)
            {
                results = results.Union(_browser.FindElements(By.CssSelector(string.Format("input[type='checkbox'][name='{0}']", name)), timeout)).ToList();
            }

            if (labelText != null)
            {
                var matchingLabels = results.Union(_browser.FindElements(By.CssSelector("label"), timeout).Where(el => el.Text == labelText)).ToList();
                results = results.Union(
                    from label
                    in matchingLabels
                    select label.GetAttribute("for")
                        into inputId
                        where inputId != null
                        select _browser.FindElement(By.CssSelector((string.Format("input[type='checkbox']#{0}", inputId))))).ToList();

                //foreach (var label in matchingLabels)
                //{
                //    var inputId = label.GetAttribute("for");
                //    if (inputId != null)
                //    {
                //        matchingInputs.Add(_browser.FindElement(By.CssSelector((string.Format("input#{0}", inputId)))));
                //    }

                //}
            }
            var element = results.Validate().First();
            if (element.Selected)
            {
                element.Click();
            }
        }



        public void Choose(string text)
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
