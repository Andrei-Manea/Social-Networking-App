using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace XamarinFormsAcademy.Utils
{
    public static class Settings
    {
        public const string DefaultPassword = "062++5952asmDasdggk662!@#!";
        public static string ToCoolString(this uint var)
        {
            return $"I am the cool String {var}";
        }

        public static string Username
        {
            get
            {
                return Preferences.Get("USERNAME", "");
            }
            set
            {
                Preferences.Set("USERNAME", value);
            }
        }
        public static string Password
        {
            get
            {
                return Preferences.Get($"{Username}_PASSWORD", DefaultPassword);
            }
            set
            {
                Preferences.Set($"{Username}_PASSWORD", value);
            }
        }
        public static bool RememberMe
        {
            get
            {
                return Preferences.Get("REMEMBER", false);
            }
            set
            {
                Preferences.Set("REMEMBER", value);
            }
        }

        public static int UserPersonalNumber
        {
            get
            {
                if (string.IsNullOrEmpty(Username))
                {
                    return 0;
                }
                return Preferences.Get($"{Username}_number", 0);
            }
            set
            {
                if (string.IsNullOrEmpty(Username))
                {
                    return;
                }
                Preferences.Set($"{Username}_number", value);
            }

            
        }
    }

}
