namespace CodingChallenge.Localization
{
    public interface ILocalizationManager
    {
        string Get(string resourceId, bool isShouldThrowResourceNotFound = false);
    }
}