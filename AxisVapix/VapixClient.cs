using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Swesim.Axis.Vapix
{
    public class VapixClient
    {
        private IPAddress deviceIp;
        private string username;
        private string password;
        private HttpClient httpClient;

        public VapixClient(IPAddress deviceIp, string username, string password)
        {
            this.deviceIp = deviceIp;
            this.username = username;
            this.password = password;
            var baseUri = new Uri($"http://{deviceIp}/axis-cgi/");
            var credentialsCache = new CredentialCache();
            credentialsCache.Add(baseUri, "Digest", new NetworkCredential(this.username, this.password));
            var handler = new HttpClientHandler() { Credentials = credentialsCache, PreAuthenticate = true };
            this.httpClient = new HttpClient(handler);
            this.httpClient.BaseAddress = baseUri;
        }

        public BasicDeviceInfo GetAllDeviceProperties()
        {
            string jsonBody = JsonConvert.SerializeObject(new BasicDeviceRequest(1.0, "Client defined request ID", "getAllProperties"));
            string result = SendVapixHttpRequest("basicdeviceinfo.cgi", HttpMethod.POST, jsonBody);
            var resultObject = JsonConvert.DeserializeObject<BasicDeviceInfo>(result);
            return resultObject;
        }

        private string SendVapixHttpRequest(string endPoint, HttpMethod method, string body = null)
        {
            if (!endPoint.EndsWith(".cgi"))
                endPoint += ".cgi";

            switch (method)
            {
                case HttpMethod.POST:
                    return System.Text.Encoding.UTF8.GetString(httpClient.PostAsync(endPoint, new StringContent(body, System.Text.Encoding.UTF8)).Result.Content.ReadAsByteArrayAsync().Result);
                case HttpMethod.GET:
                    return httpClient.GetAsync(endPoint).Result.ToString();
            }

            throw new NotSupportedException();
        }

        public enum HttpMethod
        {
            GET,
            POST
        }
    }
}