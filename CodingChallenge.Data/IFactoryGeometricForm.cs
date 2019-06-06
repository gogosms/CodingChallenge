using System.Collections.Generic;
using CodingChallenge.Data.Forms;

namespace CodingChallenge.Data
{
    public interface IFactoryGeometricForm
    {
        (string message, Dictionary<string, (decimal resultareas, decimal resultPerimeters, int iteration)>
            formModels) Print(IForm [] forms);
    }
}