using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ZolaClient.Helpers
{
    public static class AvatarHelper
    {
        private static readonly string AVATAR_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Zola\avatar\";
        private static readonly string[] SUPPORT_AVATAR_EXTENSIONS = { "png", "jpg", "jpeg" };

        /// <summary>
        /// Get avatar extension if exist
        /// if not exist, return null
        /// Avatar extention support: png, jpg, jpeg
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string GetAvatarExtension(string username)
        {
            string res = null;

            FileInfo file;
            for (int i = 0; i < SUPPORT_AVATAR_EXTENSIONS.Length; i++)
            {
                file = new FileInfo(AVATAR_PATH + username + "." + SUPPORT_AVATAR_EXTENSIONS[i]);
                if (file.Exists)
                {
                    res = SUPPORT_AVATAR_EXTENSIONS[i];
                    break;
                }
            }
            return res;
        }

        /// <summary>
        /// Save Avatar image into avatar folder
        /// If exist avatar file, it will be deleted
        /// </summary>
        /// <param name="username"></param>
        /// <param name="avatarFile"></param>
        public static void SaveAvatar(string username, ZolaService.DataFile avatarFile)
        {
            FileInfo file;
            for (int i = 0; i < SUPPORT_AVATAR_EXTENSIONS.Length; i++)
            {
                file = new FileInfo(AVATAR_PATH + username + "." + SUPPORT_AVATAR_EXTENSIONS[i]);
                if (file.Exists)
                {
                    file.Delete();
                }
            }

            int wildcardPos = avatarFile.FileName.LastIndexOf('.');
            string filename = username + avatarFile.FileName.Substring(wildcardPos);

            File.WriteAllBytes(AVATAR_PATH + filename, avatarFile.Data);
        }

        /// <summary>
        /// Get absolute avatar path if exist
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string GetAvatarPath(string username)
        {
            string res = null;
            string extension = GetAvatarExtension(username);
            if(extension != null)
            {
                res = AVATAR_PATH + username + "." + extension;
            }
            return res;
        }

        /// <summary>
        /// Load Avatar function for a specific user
        /// whose username is pass into this method
        /// </summary>
        /// <param name="imgControl"></param>
        /// <param name="username"></param>
        public static void LoadAvatar(Image imgControl, string username)
        {
            string avatarExtension = AvatarHelper.GetAvatarExtension(username);
            if (avatarExtension == null)
            {
                if (App.Proxy.IsUserHasAvatar(username))
                {
                    ZolaService.DataFile avatarFile = App.Proxy.GetAvatarFile(username);
                    AvatarHelper.SaveAvatar(username, avatarFile);
                    imgControl.Source = new BitmapImage(new Uri(AvatarHelper.GetAvatarPath(username), UriKind.Absolute));
                }
            }
            else
            {
                imgControl.Source = new BitmapImage(new Uri(AvatarHelper.GetAvatarPath(username), UriKind.Absolute));
            }
        }
    }
}
