using System;
using System.IO;
using HomeGardenShop.Helps;

[assembly: Xamarin.Forms.DependencyAttribute(typeof(HomeGardenShop.iOS.Helpers.Path))]
namespace HomeGardenShop.iOS.Helpers
{
    class Path : IPath
    {
        public string GetPath(string fileName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = System.IO.Path.Combine(documentsPath, "..", "Library");
            string path = System.IO.Path.Combine(libraryPath, fileName);

            try
            {
                if (!File.Exists(path))
                    File.Copy(fileName, path);
            }
            catch { path = null; }

            return path;
        }

        public string GetPathDocReport(string fileName)
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

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

