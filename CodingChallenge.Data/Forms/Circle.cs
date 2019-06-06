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

        public override decimal CalcularArea()
        {
            return (decimal) Math.PI * (_radio / 2) * (_radio / 2);
        }

        public override decimal CalcularPerimetro()
        {
            return (decimal) Math.PI * _radio;
        }
    }
}