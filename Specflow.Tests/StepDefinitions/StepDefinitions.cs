using Microsoft.VisualStudio.TestTools.UnitTesting;
using Specflow.Tests.StepDefinitions;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CLC.Web.UiTests.StepDefinitions
{
    [Binding]
    public class StepDefinitions : Chinchilla
    {
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
            ClickLink(link);
        }

        [When(@"(?:I)? click on (?:the)? (.*) button")]
        public void WhenIClickOnButton(string button)
        {
            ClickButton(button);
        }

        [When(@"(?:I)? fill in (?:the)? (.*) field with ""(.*)""")]
        public void WhenIFillInTheFieldWith(string field, string value)
        {
            FillIn(field, value);
        }

        [When(@"(?:I)? fill in the following:")]
        public void WhenIFillInTheFollowingFields(Table table)
        {
            var fields = table.CreateSet<Field>();
            foreach(var field in fields)
            {
                FillIn(field.Name, field.Value);
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
