namespace CodingChallenge.Data.Forms
{
    public class Square : Form
    {
        private readonly int _side;

        public Square(int side)
        {
            _side = side;
        }

        public override decimal CalcularArea()
        {
            return _side * _side;
        }

        public override decimal CalcularPerimetro()
        {
            return _side * 4;
        }
    }
}