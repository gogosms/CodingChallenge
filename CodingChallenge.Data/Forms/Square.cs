namespace CodingChallenge.Data.Forms
{
    public class Square : Form
    {
        private readonly int _side;

        public Square(int side)
        {
            _side = side;
        }

        public override decimal GetArea()
        {
            return _side * _side;
        }

        public override decimal GetPerimeter()
        {
            return _side * 4;
        }
    }
}