using System;

namespace CodingChallenge.Localization
{
    public class NotFountResourceException : Exception
    {
        public NotFountResourceException(string resourceId) : base($"Resource: '{resourceId}' not found.")
        {
        }
    }
}