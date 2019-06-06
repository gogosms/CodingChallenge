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
        
        private List<string> Initialize()
        {
            return new List<string> {"es", "en"};
        }

        public string Get(string resourceId, bool isShouldThrowResourceNotFound = false)
        {
            var currentThreadCurrentCulture = Thread.CurrentThread.CurrentCulture;
            var twoLetterIsoLanguageName = currentThreadCurrentCulture.TwoLetterISOLanguageName;
            if (!_supportLanguages.Value.Any(l => l.Contains(twoLetterIsoLanguageName)))
                throw new NotSupportLanguageException(twoLetterIsoLanguageName);
            var message = _resourceManager.GetString(resourceId, currentThreadCurrentCulture);

            if (isShouldThrowResourceNotFound && string.IsNullOrEmpty(message))
            {
                throw new NotFountResourceException(resourceId);
            }
            return message;
        }
    }
}