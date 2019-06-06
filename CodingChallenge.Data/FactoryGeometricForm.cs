using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CodingChallenge.Data.Forms;
using CodingChallenge.Localization;
using CodingChallenge.Localization.Resources;

namespace CodingChallenge.Data
{
    public class FactoryGeometricForm : IFactoryGeometricForm
    {
        private readonly ILocalizationManager _localizationManager;

        public FactoryGeometricForm(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        public (string message, Dictionary<string, (decimal resultareas, decimal resultPerimeters, int iteration)>
            formModels) Print(IForm[] expectedForms)
        {
            Debug.Assert(_localizationManager != null);
            var sb = new StringBuilder();
            if (!expectedForms.Any())
            {
                sb.Append($"<h1>{_localizationManager.Get(nameof(FormaResource.EmptyForm))}!</h1>");
                return (sb.ToString(), null);
            }

            sb.Append($"<h1>{_localizationManager.Get(nameof(FormaResource.Header))}</h1>");

            var formModels = new Dictionary<string, (decimal resultareas, decimal resultPerimeters, int iteration)>();

            foreach (var form in expectedForms)
            {
                Debug.Assert(form != null);
                var setArea = form.GetArea();
                var setPer = form.GetPerimeter();
                if (!formModels.ContainsKey(form.Name))
                {
                    formModels.Add(form.Name, (setArea, setPer, 1));
                }
                else
                {
                    var valueTuple = formModels[form.Name];
                    valueTuple.resultPerimeters += setPer;
                    valueTuple.resultareas += setArea;
                    valueTuple.iteration += 1;
                    formModels[form.Name] = valueTuple;
                }
            }

            foreach (var model in formModels) ShowLine(model, sb);
            return (sb.ToString(), formModels);
        }

        private void ShowLine(
            KeyValuePair<string, (decimal resultareas, decimal resultPerimeters, int iteration)> model,
            StringBuilder sb)
        {
            var iterations = GetIterations(model, out var pluralForms);
            var message = $"{_localizationManager.Get(nameof(FormaResource.IteractionMessage))}:{iterations} | " +
                          $"{_localizationManager.Get(nameof(FormaResource.Form))}: '{_localizationManager.Get(model.Key)}{pluralForms}'" +
                          $"| {_localizationManager.Get(nameof(FormaResource.Area))}: {model.Value.resultareas:#.##} " +
                          $"| {_localizationManager.Get(nameof(FormaResource.Perimeter))}: {model.Value.resultPerimeters:#.##} <br/>";

            sb.Append(message);
        }

        private static int GetIterations(
            KeyValuePair<string, (decimal resultareas, decimal resultPerimeters, int iteration)> model,
            out string pluralForms)
        {
            var iterations = model.Value.iteration;
            pluralForms = iterations > 1 ? "s" : string.Empty;
            return iterations;
        }
    }
}