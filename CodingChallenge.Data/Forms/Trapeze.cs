namespace CodingChallenge.Data.Forms
{
    public class Trapeze : Form
    {
        private readonly decimal _sizeDown;
        private readonly decimal _sizeLeft;
        private readonly decimal _sizeRight;
        private readonly decimal _sizeTop;

        public Trapeze(decimal sizeLeft, decimal sizeRight, decimal sizeTop, decimal sizeDown)
        {
            _sizeLeft = sizeLeft;
            _sizeRight = sizeRight;
            _sizeTop = sizeTop;
            _sizeDown = sizeDown;
        }

        public override decimal GetArea()
        {
            return _sizeLeft * ((_sizeDown + _sizeTop) / 2);
        }

        public override decimal GetPerimeter()
        {
            return _sizeDown + _sizeTop + _sizeLeft + _sizeRight;
        }
    }
}