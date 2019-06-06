using System.Globalization;
using System.Linq;
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CodingChallenge.Localization;
using HtmlAgilityPack;
using NUnit.Framework;

namespace CodingChallenge.Data.Tests
{
    [TestFixture]
    public class ValidateReportTest
    {
        [SetUp]
        public void Setup()
        {
            _container = new WindsorContainer();
            _container.Register(Component.For<ILocalizationManager>().ImplementedBy<LocalizationManager>());
            _localizationManager = _container.Resolve<ILocalizationManager>();
            var ci = new CultureInfo("es");
            Thread.CurrentThread.CurrentCulture = ci;
        }

        [TearDown]
        public void TearDown()
        {
            _container.Release(_localizationManager);
        }

        private WindsorContainer _container;
        private ILocalizationManager _localizationManager;

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