using System;
using System.IO;
using System.Reflection;

namespace MinutoSeguro.Test
{
    internal static class Util
    {
        internal static string GetFullPath(string relativePath)
        {
            var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().CodeBase);
            var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
            var dirPath = Path.GetDirectoryName(codeBasePath);
            return Path.Combine(dirPath, relativePath);
        }

    }
}
