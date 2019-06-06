using System.Globalization;
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CodingChallenge.Localization;
using NUnit.Framework;

namespace CodingChallenge.Data.Tests
{
    [TestFixture]
    public class LocalizationManagerTest
    {
        [SetUp]
        public void Setup()
        {
            _container = new WindsorContainer();
            _container.Register(Component.For<ILocalizationManager>().ImplementedBy<LocalizationManager>());
            _localizationManager = _container.Resolve<ILocalizationManager>();
        }

        [TearDown]
        public void TearDown()
        {
            _container.Release(_localizationManager);
        }

        private WindsorContainer _container;
        private ILocalizationManager _localizationManager;

        [Test]
        public void Must_Throw_If_Language_Not_Supported()
        {
            var ci = new CultureInfo("de");
            Thread.CurrentThread.CurrentCulture = ci;
            Assert.Throws<NotSupportLanguageException>(() => _localizationManager.Get("EmptyForm"));
        }

        [Test]
        public void Must_Valid_Language()
        {
            var ci = new CultureInfo("es");
            Thread.CurrentThread.CurrentCulture = ci;
            Assert.That(_localizationManager.Get("EmptyForm"), Is.EqualTo("Lista vacía de formas"));
            ci = new CultureInfo("en");
            Thread.CurrentThread.CurrentCulture = ci;
            Assert.That(_localizationManager.Get("EmptyForm"), Is.EqualTo("Empty list of forms"));
        }
    }
}