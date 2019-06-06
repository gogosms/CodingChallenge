using System.Collections.Generic;
using CodingChallenge.Data.Forms;

namespace CodingChallenge.Data
{
    public interface IFactoryGeometricForm
    {
        /// <summary>
        /// This method is to return the report in the appropriate language.
        /// </summary>
        /// <param name="forms"></param>
        /// <returns></returns>
        (string message, Dictionary<string, (decimal resultareas, decimal resultPerimeters, int iteration)>
            formModels) Print(IForm [] forms);
    }
}