public class ISN_Locale
{
	private string _CountryCode;

	private string _DisplayCountry;

	private string _LanguageCode;

	private string _DisplayLanguage;

	public string CountryCode
	{
		get
		{
			return _CountryCode;
		}
	}

	public string DisplayCountry
	{
		get
		{
			return _DisplayCountry;
		}
	}

	public string LanguageCode
	{
		get
		{
			return _LanguageCode;
		}
	}

	public string DisplayLanguage
	{
		get
		{
			return _DisplayLanguage;
		}
	}

	public ISN_Locale(string countryCode, string contryName, string languageCode, string languageName)
	{
		_CountryCode = countryCode;
		_DisplayCountry = contryName;
		_LanguageCode = languageCode;
		_DisplayLanguage = languageName;
	}
}
