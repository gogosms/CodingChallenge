namespace CodingChallenge.Data.Forms
{
    public abstract class Form : IForm
    {
        public abstract decimal CalcularArea();
        public abstract decimal CalcularPerimetro();

        public string Name => GetType().Name;
    }
}