using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading;
using CodingChallenge.Localization.Resources;

namespace CodingChallenge.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        private readonly ResourceManager _resourceManager;
        private readonly Lazy<List<string>> _supportLanguages;

        public LocalizationManager()
        {
            _resourceManager = new ResourceManager(typeof(FormaResource));
            _supportLanguages = new Lazy<List<string>>(Initialize);
        }

        public string Get(string resourceId)
        {
            var currentThreadCurrentCulture = Thread.CurrentThread.CurrentCulture;
            var twoLetterIsoLanguageName = currentThreadCurrentCulture.TwoLetterISOLanguageName;
            if (!_supportLanguages.Value.Any(l => l.Contains(twoLetterIsoLanguageName)))
                throw new NotSupportLanguageException(twoLetterIsoLanguageName);
            return _resourceManager.GetString(resourceId, currentThreadCurrentCulture);
        }

        private List<string> Initialize()
        {
            return new List<string> {"es", "en"};
        }
    }
}