using System;
using System.Globalization;

namespace WinToolLogic
{
    public class LanguageUtil
    {
        /// <summary>
        /// Returns the current language code
        /// </summary>
        /// <returns></returns>
        public static String GetLanguageCode()
        {
            return CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        }

    }
}
