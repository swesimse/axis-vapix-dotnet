using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;

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
            string result = SendVapixHttpRequest("basicdeviceinfo.cgi", HttpMethod.POST, null, jsonBody);
            var resultObject = JsonConvert.DeserializeObject<BasicDeviceInfo>(result);
            return resultObject;
        }

        public RecodingListById ListRecordings(int numberOfRecordings = 10, int offset = -1)
        {
            string parameters = "recordingid=all&maxnumberofresults=" + numberOfRecordings;
            if (offset > 0)
                parameters += "&startatresultnumber=" + offset;
            string result = SendVapixHttpRequest("record/list.cgi", HttpMethod.GET, parameters);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(RecodingListById));
            RecodingListById recordingList = (RecodingListById)xmlSerializer.Deserialize(new StringReader(result));
            return recordingList;
        }

        private string SendVapixHttpRequest(string endPoint, HttpMethod method, string queryStringParameters = null, string body = null)
        {
            if (!endPoint.EndsWith(".cgi"))
                endPoint += ".cgi";

            if (queryStringParameters != null && !queryStringParameters.StartsWith("?"))
                queryStringParameters = "?" + queryStringParameters;

            switch (method)
            {
                case HttpMethod.POST:
                    return Encoding.UTF8.GetString(httpClient.PostAsync(endPoint + queryStringParameters, new StringContent(body, System.Text.Encoding.UTF8)).Result.Content.ReadAsByteArrayAsync().Result);
                case HttpMethod.GET:
                    return Encoding.UTF8.GetString(httpClient.GetAsync(endPoint + queryStringParameters).Result.Content.ReadAsByteArrayAsync().Result);
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