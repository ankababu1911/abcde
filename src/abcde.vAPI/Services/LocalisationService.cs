using abcde.Data.Interfaces;
using abcde.Model;
using Microsoft.Extensions.Caching.Memory;

namespace abcde.vAPI.Services
{
    public interface ILocalizationService
    {
        string GetTranslation(string key, string language = null);
    }
    public class LocalisationService : ILocalizationService
    {
		private readonly ILocalisationRepository _localisationRepository;
		private readonly IMemoryCache _cache;
		public LocalisationService(ILocalisationRepository localisationRepository, IMemoryCache cache)
		{
			_localisationRepository = localisationRepository;
		_cache = cache;
		}

		public string GetTranslation(string key, string? language = null)
		{
			language ??= "en-GB";
			// Construct a cache key based on language
			string cacheKey = $"{language}Translations";

			// Try to get the translation from the cache
			if (_cache.TryGetValue(cacheKey, out List<Translation> cachedTranslations))
			{
				var cachedTranslation = cachedTranslations?.FirstOrDefault(t => t.Key == key && t.LanguageCode == language);
				if (cachedTranslation != null)
				{
					return cachedTranslation.Value;
				}
			}

			// If not found in the cache, fetch it from the database
			var translations = _localisationRepository.GetAll();
			if (translations != null && translations.Any())
			{
				_cache.Set(cacheKey, translations, TimeSpan.FromMinutes(5));
				var translation = translations.FirstOrDefault(t => t.Key == key && t.LanguageCode == language);
				if (translation != null)
				{
					return translation.Value;
				}
			}

			return key; // Return the original key if no translation is found
		}
	}
}
