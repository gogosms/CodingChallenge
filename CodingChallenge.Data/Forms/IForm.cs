namespace CodingChallenge.Data.Forms
{
    public interface IForm
    {
        string Name { get; }
        decimal GetArea();
        decimal GetPerimeter();
    }
}