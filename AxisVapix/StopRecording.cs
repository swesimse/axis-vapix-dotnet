using System.Xml.Serialization;

namespace Swesim.Axis.Vapix
{
	[XmlRoot(ElementName = "root")]
	public class StopRecordingResponse
    {
		[XmlElement(ElementName = "stop")]
		public Stop StopRecordInfo { get; set; }

		[XmlRoot(ElementName = "stop")]
		public class Stop
		{

			[XmlAttribute(AttributeName = "recordingid")]
			public string Recordingid { get; set; }

			[XmlAttribute(AttributeName = "result")]
			public string Result { get; set; }

			[XmlAttribute(AttributeName = "errormsg")]
			public string Errormsg { get; set; }
		}
	}
}
