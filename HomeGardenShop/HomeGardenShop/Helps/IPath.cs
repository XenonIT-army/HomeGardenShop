using System;
namespace HomeGardenShop.Helps
{
    public interface IPath
    {
        string GetPath(string fileName);

        string GetPathDocReport(string fileName);

        string GetPathToLog(string fileName);
    }
}

