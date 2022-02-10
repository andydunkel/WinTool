using System;
using Microsoft.Win32;

namespace WinToolLogic.Registry
{
    public class RegistryUtil
    {
        /// <summary>
        /// Get a registry path from the HKEY_CURRENTUSER
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public RegistryKey GetRegistryKeyUser(string path)
        {
            var baseKey = GetUserRegistryKey();
            var reg = baseKey.OpenSubKey(path, true);

            return reg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void DeleteUserKey(string path)
        {
            var baseKey = GetUserRegistryKey();
            var reg = baseKey.OpenSubKey(path, true);
            if (reg != null)
            {
                baseKey.DeleteSubKeyTree(path);
            }
        }

        /// <summary>
        /// Create a registry key
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void CreateRegistryKeyUser(string path)
        {
            var baseKey = GetUserRegistryKey();
            var key = baseKey.CreateSubKey(path);
            key.SetValue("", "");
        }

        private static RegistryKey GetUserRegistryKey()
        {
            RegistryKey baseKey;

            if (Environment.Is64BitOperatingSystem)
            {
                baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            }
            else
            {
                baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            }

            return baseKey;
        }

    }
}
