using System;

namespace CodingChallenge.Localization
{
    public class NotSupportLanguageException : Exception
    {
        public NotSupportLanguageException(string languageId) : base($"Unsupported language: '{languageId}'")
        {
        }
    }
}