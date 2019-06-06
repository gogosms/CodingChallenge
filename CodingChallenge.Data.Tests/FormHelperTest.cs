using System.Linq;
using CodingChallenge.Data.Forms;
using NUnit.Framework;

namespace CodingChallenge.Data.Tests
{
    [TestFixture]
    public class FormHelperTest
    {
        [Test]
        public void Must_Get_Support_Forms()
        {
            var forms = FormHelper.GetSupportForms();
            Assert.IsNotNull(forms);
            CollectionAssert.IsNotEmpty(forms);
            Assert.That(forms.Length, Is.EqualTo(5));
            Assert.That(forms.Any(f => f.Equals(nameof(EquilateralTriangle))));
            Assert.That(forms.Any(f => f.Equals(nameof(Circle))));
            Assert.That(forms.Any(f => f.Equals(nameof(Square))));
            Assert.That(forms.Any(f => f.Equals(nameof(Rectangle))));
            Assert.That(forms.Any(f => f.Equals(nameof(Trapeze))));
        }
    }
}