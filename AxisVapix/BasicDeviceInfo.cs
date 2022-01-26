using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;

namespace Swesim.Axis.Vapix
{
    internal class BasicDeviceRequest
    {
        [JsonProperty("apiVersion")]
        public string ApiVersion;

        [JsonProperty("context")]
        public string Context;

        [JsonProperty("method")]
        public string Method;

        public BasicDeviceRequest(double apiVersion, string context, string method)
        {
            this.ApiVersion = apiVersion.ToString("F1", CultureInfo.CreateSpecificCulture("en-US"));
            this.Context = context;
            this.Method = method;
        }
    }

    public class BasicDeviceInfo
    {
        [JsonProperty("apiVersion")]
        public double ApiVersion;

        [JsonProperty("context")]
        public string Context;

        [JsonProperty("data")]
        public DeviceData Data;

        public class DeviceData
        {
            [JsonProperty("propertyList")]
            public DeviceProperties Properties;

            public class DeviceProperties
            {
                public string Architecture;
                public string Brand;
                public string BuildDate;
                public string HardwareID;
                public string ProdFullName;
                public string ProdNbr;
                public string ProdShortName;
                public string ProdType;
                public string ProdVariant;
                public string SerialNumber;
                public string Soc;
                public string SocSerialNumber;
                public string Version;
                public string WebURL;
            }
        }
    }
}
