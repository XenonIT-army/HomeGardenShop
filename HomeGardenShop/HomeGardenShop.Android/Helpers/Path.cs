using System;
using System.IO;
using HomeGardenShop.Helps;

[assembly: Xamarin.Forms.DependencyAttribute(typeof(HomeGardenShop.Droid.Helpers.Path))]
namespace HomeGardenShop.Droid.Helpers
{
    class Path : IPath
    {
        public string GetPath(string fileName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = System.IO.Path.Combine(documentsPath, fileName);
            try
            {
                if (!File.Exists(path))
                {
                    var context = Android.App.Application.Context;
                    var dbAssetStream = context.Assets.Open(fileName);

                    var dbFileStream = new FileStream(path, FileMode.OpenOrCreate);
                    var buffer = new byte[1024];

                    int b = buffer.Length;
                    int length;

                    while ((length = dbAssetStream.Read(buffer, 0, b)) > 0)
                    {
                        dbFileStream.Write(buffer, 0, length);
                    }

                    dbFileStream.Flush();
                    dbFileStream.Close();
                    dbAssetStream.Close();
                }
            }
            catch { path = null; }
            return path;
        }

        [Obsolete]
        public string GetPathDocReport(string fileName)
        {
            string path = System.IO.Path.Combine(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath, fileName);
            //string path = System.IO.Path.Combine(MediaStore.Downloads.ExternalContentUri.ToString(), fileName);
            return path;
        }

        public string GetPathToLog(string fileName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = System.IO.Path.Combine(documentsPath, fileName);
            return path;
        }
    }
}

