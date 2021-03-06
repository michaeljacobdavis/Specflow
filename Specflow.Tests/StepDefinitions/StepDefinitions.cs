﻿using System.Configuration;
using Chinchilla;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CLC.Web.UiTests.StepDefinitions
{
    [Binding]
    public class StepDefinitions : Chinchilla.Chinchilla
    {
        public StepDefinitions() :base(ConfigurationManager.AppSettings["RootUrl"]){}

        #region Givens

        [Given(@"I'm on (.*)")]
        public void GivenImOn(string relativeUrl)
        {
            Visit(relativeUrl);
        }

        [Given(@"This is my first time on the site")]
        public void GivenThisIsMyFirstTimeOnTheSite()
        {
        }
        #endregion

        #region Whens
        [When(@"(?:I)? click on (?:the)? (.*) link")]
        public void WhenIClickOnLink(string link)
        {
            ClickLink(text: link);
        }

        [When(@"(?:I)? click on (?:the)? (.*) button")]
        public void WhenIClickOnButton(string button)
        {
            ClickButton(text : button);
        }

        [When(@"(?:I)? fill in (?:the)? (.*) field with ""(.*)""")]
        public void WhenIFillInTheFieldWith(string field, string value)
        {
            FillIn(value, labelText: field);
        }

        [When(@"(?:I)? select (.*) from (.*)")]
        public void WhenISelectFrom(string option, string field)
        {
            Select(field, option);
        }

        [When(@"(?:I)? fill in the following:")]
        public void WhenIFillInTheFollowingFields(Table table)
        {
            var fields = table.CreateSet<Field>();
            foreach(var field in fields)
            {
                FillIn(field.Value, labelText: field.Name);
            }
        }
        #endregion

        #region Thens
        [Then(@"(?:I)? should be on (.*)")]
        public void ThenIShouldBeOn(string relativeUrl)
        {
            Assert.AreEqual(relativeUrl, CurrentPath);
        }


        [Then(@"I should see ""(.*)""")]
        public void ThenIShouldSee(string text)
        {
            Assert.IsTrue(Page.HasContent(text));
        }

        #endregion

        #region Tags
        [BeforeScenario("SignIn")]
        public void BeforeWebScenario()
        {
            Visit("/Authentication/SignIn");
        }
        #endregion
    }


}
