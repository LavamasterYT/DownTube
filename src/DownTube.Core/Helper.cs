using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DownTube.Core
{
    internal static class Helper
    {
        public static string RemoveInvalidChars(this string input)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                input = input.Replace(c.ToString(), "");
            }

            return input;
        }
    }
}
