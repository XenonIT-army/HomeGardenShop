using System;
namespace HomeGardenShop.Helps.DataContainer
{
    public interface IDataContainer
    {
        #region Methods

        public void AddValue(string key, object value);

        public object GetValue(string key);

        public void RemoveValue(string key);

        public void AddValueXML(string key, string recipeXML);

        public string GetValueXML(string key);

        #endregion
    }
}

