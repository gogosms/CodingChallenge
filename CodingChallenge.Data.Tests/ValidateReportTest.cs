using System.Linq;
using HtmlAgilityPack;
using NUnit.Framework;

namespace CodingChallenge.Data.Tests
{
    [TestFixture]
    public class ValidateReportTest
    {
        [TestCase("<span>Test", false)]
        [TestCase("<span>Test</sspan>", false)]
        [TestCase("<span>Test</span>", true)]
        public void Must_Indicate_That_Html_Is_Wrong(string html, bool isValid)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            if (isValid)
                Assert.That(!htmlDocument.ParseErrors.Any());
            else
                Assert.That(htmlDocument.ParseErrors.Any());
        }
    }
}