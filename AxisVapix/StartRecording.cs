using System.Xml.Serialization;

namespace Swesim.Axis.Vapix
{
	[XmlRoot(ElementName = "root")]
	public class StartRecordingResponse
    {

		[XmlElement(ElementName = "record")]
		public Record RecordInfo { get; set; }

		[XmlRoot(ElementName = "record")]
		public class Record
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
