using System;

namespace CodingChallenge.Data.Forms
{
    public class EquilateralTriangle : Triangle
    {
        private readonly decimal _size;

        public EquilateralTriangle(decimal size)
        {
            _size = size;
        }

        public override decimal CalcularArea()
        {
            return (decimal) Math.Sqrt(3) / 4 * _size * _size;
        }

        public override decimal CalcularPerimetro()
        {
            return _size * 3;
        }
    }
}