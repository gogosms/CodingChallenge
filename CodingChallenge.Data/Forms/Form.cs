namespace CodingChallenge.Data.Forms
{
    public abstract class Form : IForm
    {
        /// <summary>
        /// this method returns the calculation Area
        /// </summary>
        /// <returns></returns>
        public abstract decimal GetArea();
        /// <summary>
        /// This method returns the calculation perimeter
        /// </summary>
        /// <returns></returns>
        public abstract decimal GetPerimeter();

        /// <summary>
        /// Name form
        /// <example>Square, EquilateralTriangle, others. </example>
        /// </summary>
        public string Name => GetType().Name;
    }
}