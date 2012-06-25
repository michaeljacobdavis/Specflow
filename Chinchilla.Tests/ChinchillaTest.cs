using System.Collections.Generic;
using System.Collections.ObjectModel;
using MJD;
using MJD.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using OpenQA.Selenium;

namespace MJD.Tests
{
    
[TestClass]
    public class ChinchillaTest
    {
        [TestMethod]
        [ExpectedException(typeof(MoreThanOneElementFoundException))]
        public void ClickLinkMultipleTest()
        {
            var browser = new Mock<IWebDriver>();
            var found = new List<IWebElement>();
            browser.Setup(b => b.FindElements(It.IsAny<By>())).Returns(new ReadOnlyCollection<IWebElement>(found));

            var element = new Mock<IWebElement>();
            element.Setup(e => e.Text).Returns("Test");

            var element2 = new Mock<IWebElement>();
            element2.Setup(e => e.Text).Returns("Test2");

            found.Add(element.Object);
            found.Add(element2.Object);


            var chinchilla = new Chinchilla(browser.Object, "http://localhost.com");

            chinchilla.ClickLink("test");
        }

        [TestMethod]
        [ExpectedException(typeof(NoElementsFoundException))]
        public void ClickLinkNoneTest()
        {
            var browser = new Mock<IWebDriver>();
            var found = new List<IWebElement>();
            browser.Setup(b => b.FindElements(It.IsAny<By>())).Returns(new ReadOnlyCollection<IWebElement>(found));

            var chinchilla = new Chinchilla(browser.Object, "http://localhost.com");

            chinchilla.ClickLink("test");
        }

        [TestMethod]
        public void ClickLinkTextTest()
        {
            var browser = new Mock<IWebDriver>();
            var found = new List<IWebElement>();
            browser.Setup(b => b.FindElements(It.IsAny<By>())).Returns(new ReadOnlyCollection<IWebElement>(found));

            var element = new Mock<IWebElement>();
            element.Setup(e => e.Text).Returns("Test");

            var element2 = new Mock<IWebElement>();
            element2.Setup(e => e.Text).Returns("Test2");

            found.Add(element.Object);
            found.Add(element2.Object);


            var chinchilla = new Chinchilla(browser.Object, "http://localhost.com");

            chinchilla.ClickLink(text:"Test");
        }

    }
}
