using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MJD.Exceptions;
using OpenQA.Selenium;

namespace MJD.Extensions
{
    public static class WebElementsExtensions
    {
        public static List<IWebElement> Validate(this List<IWebElement> results)
        {
            if (results.Count > 1)
            {
                throw new MoreThanOneElementFoundException();
            }
            if (results.Count == 0)
            {
                throw new NoElementsFoundException();
            }
            return results;
        }
    }
}
