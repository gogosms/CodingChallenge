namespace CodingChallenge.Data.Forms
{
    public interface IForm
    {
        string Name { get; }
        decimal CalcularArea();
        decimal CalcularPerimetro();
    }
}