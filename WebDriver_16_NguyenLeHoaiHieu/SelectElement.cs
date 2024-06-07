using OpenQA.Selenium;
using System;

namespace WebDriver_16_NguyenLeHoaiHieu
{
    internal class SelectElement
    {
        private IWebElement webElement;

        public SelectElement(IWebElement webElement)
        {
            this.webElement = webElement;
        }

        internal void SelectByValue(string v)
        {
            throw new NotImplementedException();
        }
    }
}