using abcde.Model;
using abcde.Model.Constants;

namespace abcde.Test.Data
{
	public class LocalisationData
	{
		public static IEnumerable<Translation> GetTranslations()
		{
			//Generate List<Translation> in these languages {"en-GB","en-US","fr-FR", "de-CH" ,"fr-CH", "it-CH","rm-CH"} for keys {"About","Home","Organisations"} without fakes but real translations
			var translations = new List<Translation>
			{

				new Translation { Key = LocalisationKeys.Email, LanguageCode = "en-GB", Value = "email" },
				 new Translation { Key = LocalisationKeys.Email, LanguageCode = "de-CH", Value = "E-Mail" },
				 //Password
             new Translation { Key = LocalisationKeys.Password, LanguageCode = "en-GB", Value = "Password" },	
			 new Translation { Key = LocalisationKeys.Password, LanguageCode = "de-CH", Value = "Passwort" },
				//RememberMe
              new Translation { Key = LocalisationKeys.RememberMe, LanguageCode = "en-GB", Value = "Remember Me" },

			  new Translation { Key = LocalisationKeys.RememberMe, LanguageCode = "de-CH", Value = "erinnere dich an mich" },
			  //LoginTaskManagement
			  new Translation{ Key=LocalisationKeys.LoginTaskManagement, LanguageCode ="en-GB",Value="Login Task Management"},
	         new Translation { Key = LocalisationKeys.LoginTaskManagement, LanguageCode = "de-CH", Value = "Anmeldungs-Aufgabenverwaltung" },
			 //Home
			   new Translation{ Key=LocalisationKeys.Home, LanguageCode ="en-GB",Value="Home"},
              new Translation { Key = LocalisationKeys.Home, LanguageCode = "de-CH", Value = "Zuhause" },
			  //LogOut
	          new Translation{ Key = LocalisationKeys.LogOut, LanguageCode = "en-GB", Value = "Log Out" },
                new Translation { Key = LocalisationKeys.LogOut, LanguageCode = "de-CH", Value = "abmeldung" },
				//TaskManagementPortal
			  new Translation{ Key = LocalisationKeys.TaskManagementPortal, LanguageCode = "en-GB",  Value = "Task Management Portal" },
              new Translation{ Key=LocalisationKeys.TaskManagementPortal, LanguageCode ="de-CH"
			  ,Value="Aufgabenmanagement-Portal"},
			  //LoginUser
               new Translation { Key = LocalisationKeys.LoginUser, LanguageCode = "en-GB", Value = "Login User" },
               new Translation { Key = LocalisationKeys.LoginUser, LanguageCode = "de-CH", Value = "Benutzer anmelden" },
			   //ForgotPassword
			   new Translation{ Key = LocalisationKeys.ForgotPassword,LanguageCode = "en-GB", Value = "Forgot Password"},
             new Translation{Key = LocalisationKeys.ForgotPassword,LanguageCode = "de-CH",Value = "Passwort vergessen"}
            };

            foreach (var item in translations)
			{
				item.Created = DateTime.Now;
				item.Datestamp = DateTime.Now;
				item.IsActive = true;
				item.LastModifiedBy = "Admin";
			}
			return translations;
		}
	}
}
