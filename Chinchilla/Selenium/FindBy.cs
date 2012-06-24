using OpenQA.Selenium;

namespace Chinchilla.Selenium
{
    public class FindBy : By
    {
        public static By LabelText(string text)
        {
            return By.XPath(string.Format("//label[text()='{0}']", text));
        }

        public static By Text(string text)
        {
            return By.XPath(string.Format("//*[text()='{0}']", text));
        }

        public static By ButtonText(string text)
        {
            return By.XPath(string.Format("//input[@value='{0}' and @type='submit']", text));
        }
    }
}