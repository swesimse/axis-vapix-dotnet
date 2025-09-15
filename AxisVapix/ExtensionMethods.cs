using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Swesim.Axis.Vapix.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static async Task DownloadFileTaskAsync(this HttpClient client, Uri uri, string FileName)
        {
            using var s = await client.GetStreamAsync(uri);
            using var fs = new FileStream(FileName, FileMode.CreateNew);
            await s.CopyToAsync(fs);
        }
    }
}
