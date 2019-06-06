using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CodingChallenge.Data.Forms;
using CodingChallenge.Localization;
using HtmlAgilityPack;
using NUnit.Framework;

namespace CodingChallenge.Data.Tests
{
    [TestFixture]
    public class GeometricFormRefactorTest
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
        
        [Test]
        public void Must_Empty_Forms()
        {
            var geometricForm = new FactoryGeometricForm(_localizationManager);
            var report = geometricForm.Print(Array.Empty<IForm>());
            Assert.IsNotEmpty(report.message);
            Assert.That(report.message, Is.EqualTo("<h1>Lista vacía de formas!</h1>"));
            Assert.IsNull(report.formModels);
            AssertHtml(report);
        }

        [Test]
        public void Test_Resumen_Lista_Con_Dos_Cuadrados()
        {
            var square = new Square(4);
            var otherSquare = new Square(2);
            var geometricForm = new FactoryGeometricForm(_localizationManager);
            var report = geometricForm.Print(new IForm[] { square, otherSquare });
            Assert.IsNotEmpty(report.message);
            Assert.That(report.formModels.Values.Count, Is.EqualTo(1));
            var expectedSquare = report.formModels.Values.FirstOrDefault();
            var squareType = report.formModels.Keys.FirstOrDefault();
            Assert.IsNotNull(squareType);
            Assert.That(squareType, Is.EqualTo(nameof(Square)));
            Assert.That(expectedSquare.iteration, Is.EqualTo(2));
            Assert.That(expectedSquare.resultPerimeters,
                Is.EqualTo(square.GetPerimeter() + otherSquare.GetPerimeter()));
            Assert.That(expectedSquare.resultareas, Is.EqualTo(square.GetArea() + otherSquare.GetArea()));
            Assert.That(report.message, Is.EqualTo("<h1>Reporte de Formas</h1>Cantidad de interacciones:2 " +
                                                   "| Forma: 'Cuadrados'| Area: 20 | Perimetro: 24 <br/>"));

            AssertHtml(report);
        }

        [Test]
        public void Test_Resumen_Lista_Con_Dos_Triangulos()
        {
            var equilateralTriangle = new EquilateralTriangle(4);
            var squares = new IForm[] {equilateralTriangle, equilateralTriangle};
            var geometricForm = new FactoryGeometricForm(_localizationManager);
            var report = geometricForm.Print(squares);
            Assert.IsNotEmpty(report.message);
            Assert.That(report.formModels.Values.Count, Is.EqualTo(1));
            var square = report.formModels.Values.FirstOrDefault();
            var squareType = report.formModels.Keys.FirstOrDefault();
            Assert.IsNotNull(squareType);
            Assert.That(squareType, Is.EqualTo(nameof(EquilateralTriangle)));
            Assert.That(square.iteration, Is.EqualTo(2));
            Assert.That(square.resultPerimeters,
                Is.EqualTo(equilateralTriangle.GetPerimeter() + equilateralTriangle.GetPerimeter()));
            Assert.That(square.resultareas,
                Is.EqualTo(equilateralTriangle.GetArea() + equilateralTriangle.GetArea()));
            Assert.That(report.message, Is.EqualTo("<h1>Reporte de Formas</h1>Cantidad de interacciones:2 " +
                                                   "| Forma: 'Tringulo Equilateros'| Area: 13,86 | Perimetro: 24 <br/>"));

            AssertHtml(report);
        }

        [Test]
        public void Test_Resumen_Lista_Con_Mas_Tipos()
        {
            var forms = new IForm[]
            {
                new Square(5),
                new Circle(3),
                new EquilateralTriangle(4),
                new Square(2),
                new EquilateralTriangle(9),
                new Circle(2.75m),
                new EquilateralTriangle(4.2m)
            };
            var squaresCount =
                forms.Count(f => f.Name.Equals(nameof(Square), StringComparison.InvariantCultureIgnoreCase));
            var circleCount =
                forms.Count(f => f.Name.Equals(nameof(Circle), StringComparison.InvariantCultureIgnoreCase));
            var triangleCount = forms.Count(f =>
                f.Name.Equals(nameof(EquilateralTriangle), StringComparison.InvariantCultureIgnoreCase));
            var geometricFormRefactor = new FactoryGeometricForm(_localizationManager);
            var report = geometricFormRefactor.Print(forms);

            Assert.IsNotNull(report);
            Assert.IsNotNull(report.formModels);
            Assert.IsNotNull(report.message);
            Assert.That(report.formModels[nameof(Square)].iteration, Is.EqualTo(squaresCount));
            Assert.That(report.formModels[nameof(Circle)].iteration, Is.EqualTo(circleCount));
            Assert.That(report.formModels[nameof(EquilateralTriangle)].iteration, Is.EqualTo(triangleCount));
            Assert.That(report.message, Is.EqualTo(
                "<h1>Reporte de Formas</h1>Cantidad de interacciones:2 | Forma: 'Cuadrados'| Area: 29 | Perimetro: 28 <br/>" +
                "Cantidad de interacciones:2 | Forma: 'Circulos'| Area: 13,01 | Perimetro: 18,06 <br/>" +
                "Cantidad de interacciones:3 | Forma: 'Tringulo Equilateros'| Area: 49,64 | Perimetro: 51,6 <br/>"));

            AssertHtml(report);
        }

        [Test]
        public void Test_Resumen_Lista_Con_Un_Cuadrado()
        {
            var squares = new IForm[] {new Square(4)};
            var geometricForm = new FactoryGeometricForm(_localizationManager);
            var report = geometricForm.Print(squares);
            Assert.IsNotEmpty(report.message);

            Assert.That(report.formModels.Values.Count, Is.EqualTo(1));
            var square = report.formModels.Values.FirstOrDefault();
            var squareType = report.formModels.Keys.FirstOrDefault();
            Assert.IsNotNull(squareType);
            Assert.That(squareType, Is.EqualTo(nameof(Square)));
            Assert.That(square.iteration, Is.EqualTo(1));
            Assert.That(square.resultPerimeters, Is.EqualTo(16));
            Assert.That(square.resultareas, Is.EqualTo(16));
            Assert.That(report.message, Is.EqualTo("<h1>Reporte de Formas</h1>Cantidad de interacciones:1 | " +
                                                   "Forma: 'Cuadrado'| Area: 16 | Perimetro: 16 <br/>"));

            AssertHtml(report);
        }

        [Test]
        public void Test_Resumen_Lista_Con_Un_Trapecio_Y_Un_Cuadrado_En_Ingles()
        {
            var ci = new CultureInfo("en");
            Thread.CurrentThread.CurrentCulture = ci;
            var forms = new IForm[]
            {
                new Trapeze(5, 4, 3, 6),
                new Square(4)
            };
            var geometricForm = new FactoryGeometricForm(_localizationManager);
            var report = geometricForm.Print(forms);
            Assert.IsNotNull(report);
            Assert.IsNotNull(report.message);
            Assert.IsNotNull(report.formModels);
            Assert.That(report.formModels[nameof(Trapeze)].iteration, Is.EqualTo(1));
            Assert.That(report.formModels[nameof(Square)].iteration, Is.EqualTo(1));
            Assert.That(report.message, Is.EqualTo(
                "<h1>Report Forms</h1>Number of interactions:1 | Form: 'Trapeze'| Area: 22.5 | Perimeter: 18 <br/>" +
                "Number of interactions:1 | Form: 'Square'| Area: 16 | Perimeter: 16 <br/>"));
            AssertHtml(report);
        }

        private static void AssertHtml(
            (string message, Dictionary<string, (decimal resultareas, decimal resultPerimeters, int iteration)>
                formModels) report)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(report.message);
            Assert.That(htmlDocument.ParseErrors.Count(), Is.EqualTo(0));
        }
    }
}