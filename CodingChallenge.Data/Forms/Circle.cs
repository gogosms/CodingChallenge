using System;

namespace CodingChallenge.Data.Forms
{
    public class Circle : Form
    {
        private readonly decimal _radio;

        public Circle(decimal radio)
        {
            _radio = radio;
        }

        public override decimal GetArea()
        {
            return (decimal) Math.PI * (_radio / 2) * (_radio / 2);
        }

        public override decimal GetPerimeter()
        {
            return (decimal) Math.PI * _radio;
        }
    }
}