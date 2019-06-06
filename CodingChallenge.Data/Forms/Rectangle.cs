﻿namespace CodingChallenge.Data.Forms
{
    public class Rectangle : Form
    {
        private readonly decimal _based;
        private readonly decimal _height;

        public Rectangle(decimal based, decimal height)
        {
            _based = based;
            _height = height;
        }

        public override decimal CalcularArea()
        {
            return _based * _height;
        }

        public override decimal CalcularPerimetro()
        {
            return 2 * _based + 2 * _height;
        }
    }
}