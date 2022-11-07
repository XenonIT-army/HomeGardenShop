using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

namespace HomeGardenShop.Helps.AppLocalizer
{
    public class Translator : INotifyPropertyChanged
    {
        private const string ResourceId = "HomeGardenShop.Properties.Resource";
        private static CultureInfo CultureInfoApp { get; set; }
        private static readonly Translator _uniqueInstance = new Translator();
        public static Translator TranslatorInstance => _uniqueInstance;

        public static readonly Lazy<ResourceManager> RessourceManagerLanguage = new Lazy<ResourceManager>(() =>
        new ResourceManager(ResourceId, IntrospectionExtensions.GetTypeInfo(typeof(TranslateExtension)).Assembly));

        public string this[string text]
        {
            get
            {
                string value;
                value = RessourceManagerLanguage.Value.GetString(text, CultureInfoApp);
                return value;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void Invalidate()
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventHandler tmp = Interlocked.CompareExchange(ref PropertyChanged, null, null);
                if (tmp != null) { tmp(this, new PropertyChangedEventArgs(null)); }
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }

        }
    }
}
