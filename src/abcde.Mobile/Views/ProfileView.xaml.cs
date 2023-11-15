using abcde.Mobile.Localization;
using abcde.Mobile.Resx;
using System.Globalization;

namespace abcde.Mobile.Views;

public partial class ProfileView : ContentPageBase
{
    public LocalizationResourceManager LocalizationResourceManager
        => LocalizationResourceManager.Instance;

    public ProfileView()
	{
		InitializeComponent();
        BindingContext = this;
	}

    private void ChangeLanguage_Clicked(object sender, EventArgs e)
    {
        var switchToCulture = AppResources.Culture.TwoLetterISOLanguageName
            .Equals("de", StringComparison.InvariantCultureIgnoreCase) ?
            new CultureInfo("en-US") : new CultureInfo("de-CH");

        LocalizationResourceManager.Instance.SetCulture(switchToCulture);
    }
}