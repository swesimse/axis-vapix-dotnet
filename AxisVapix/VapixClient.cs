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

        public BasicDeviceInfoResponse GetAllDeviceProperties()
        {
            string jsonBody = JsonConvert.SerializeObject(new BasicDeviceRequest(1.0, "Client defined request ID", "getAllProperties"));
            string result = SendVapixHttpRequest("basicdeviceinfo.cgi", HttpMethod.POST, null, jsonBody);
            var resultObject = JsonConvert.DeserializeObject<BasicDeviceInfoResponse>(result);
            return resultObject;
        }

        public RecordingListByIdResponse ListRecordings(int numberOfRecordings = 10, int offset = -1)
        {
            string parameters = "recordingid=all&maxnumberofresults=" + numberOfRecordings;
            if (offset > 0)
                parameters += "&startatresultnumber=" + offset;
            string result = SendVapixHttpRequest("record/list.cgi", HttpMethod.GET, parameters);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(RecordingListByIdResponse));
            RecordingListByIdResponse recordingList = (RecordingListByIdResponse)xmlSerializer.Deserialize(new StringReader(result));
            return recordingList;
        }

        public RecordingListByIdResponse GetRecording(string recordingId)
        {
            string parameters = "recordingid=" + recordingId;
            string result = SendVapixHttpRequest("record/list.cgi", HttpMethod.GET, parameters);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(RecordingListByIdResponse));
            RecordingListByIdResponse recordingList = (RecordingListByIdResponse)xmlSerializer.Deserialize(new StringReader(result));
            return recordingList;
        }

        public StartRecordingResponse StartRecording(string storageDiskId)
        {
            string result = SendVapixHttpRequest("record/record.cgi", HttpMethod.GET, "diskid=" + storageDiskId);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(StartRecordingResponse));
            StartRecordingResponse recordingResponse = (StartRecordingResponse)xmlSerializer.Deserialize(new StringReader(result));
            return recordingResponse;
        }

        public StopRecordingResponse StopRecording(string recordingId)
        {
            string result = SendVapixHttpRequest("record/stop.cgi", HttpMethod.GET, "recordingid=" + recordingId);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(StopRecordingResponse));
            StopRecordingResponse recordingResponse = (StopRecordingResponse)xmlSerializer.Deserialize(new StringReader(result));
            return recordingResponse;
        }

        public string DownloadRecording(string recordingId, string diskId)
        {
            string endpoint = "record/export/exportrecording.cgi";
            string parameters = "?schemaversion=1&recordingid=" + recordingId + "&diskid=" + diskId  + "&exportformat=matroska";
            string fileName = recordingId + ".mkv";
            using (var client = new WebClient())
            {
                client.Credentials = new NetworkCredential(username, password);
                client.DownloadFile(httpClient.BaseAddress + endpoint + parameters, fileName);
            }
            return fileName;
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