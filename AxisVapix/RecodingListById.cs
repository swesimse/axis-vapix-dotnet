using System.Collections.Generic;
using System.Xml.Serialization;

namespace Swesim.Axis.Vapix
{
	// Generated using https://json2csharp.com/xml-to-csharp

	[XmlRoot(ElementName = "root")]
	public class RecodingListById
	{

		[XmlElement(ElementName = "recordings")]
		public Recordings RecordingList { get; set; }

		[XmlAttribute(AttributeName = "xsi")]
		public string Xsi { get; set; }

		[XmlAttribute(AttributeName = "noNamespaceSchemaLocation")]
		public string NoNamespaceSchemaLocation { get; set; }

		[XmlRoot(ElementName = "video")]
		public class Video
		{

			[XmlAttribute(AttributeName = "mimetype")]
			public string Mimetype { get; set; }

			[XmlAttribute(AttributeName = "source")]
			public string Source { get; set; }

			[XmlAttribute(AttributeName = "framerate")]
			public string Framerate { get; set; }

			[XmlAttribute(AttributeName = "resolution")]
			public string Resolution { get; set; }

			[XmlAttribute(AttributeName = "width")]
			public string Width { get; set; }

			[XmlAttribute(AttributeName = "height")]
			public string Height { get; set; }
		}

		[XmlRoot(ElementName = "audio")]
		public class Audio
		{

			[XmlAttribute(AttributeName = "mimetype")]
			public string Mimetype { get; set; }

			[XmlAttribute(AttributeName = "source")]
			public string Source { get; set; }

			[XmlAttribute(AttributeName = "bitrate")]
			public string Bitrate { get; set; }

			[XmlAttribute(AttributeName = "samplerate")]
			public string Samplerate { get; set; }
		}

		[XmlRoot(ElementName = "recording")]
		public class Recording
		{

			[XmlElement(ElementName = "video")]
			public Video Video { get; set; }

			[XmlElement(ElementName = "audio")]
			public Audio Audio { get; set; }

			[XmlAttribute(AttributeName = "diskid")]
			public string Diskid { get; set; }

			[XmlAttribute(AttributeName = "recordingid")]
			public string Recordingid { get; set; }

			[XmlAttribute(AttributeName = "starttime")]
			public string Starttime { get; set; }

			[XmlAttribute(AttributeName = "starttimelocal")]
			public string Starttimelocal { get; set; }

			[XmlAttribute(AttributeName = "stoptime")]
			public string Stoptime { get; set; }

			[XmlAttribute(AttributeName = "stoptimelocal")]
			public string Stoptimelocal { get; set; }

			[XmlAttribute(AttributeName = "recordingtype")]
			public string Recordingtype { get; set; }

			[XmlAttribute(AttributeName = "eventid")]
			public string Eventid { get; set; }

			[XmlAttribute(AttributeName = "eventtrigger")]
			public string Eventtrigger { get; set; }

			[XmlAttribute(AttributeName = "recordingstatus")]
			public string Recordingstatus { get; set; }

			[XmlAttribute(AttributeName = "source")]
			public string Source { get; set; }
		}

		[XmlRoot(ElementName = "recordings")]
		public class Recordings
		{

			[XmlElement(ElementName = "recording")]
			public List<Recording> Recording { get; set; }

			[XmlAttribute(AttributeName = "totalnumberofrecordings")]
			public string Totalnumberofrecordings { get; set; }

			[XmlAttribute(AttributeName = "numberofrecordings")]
			public string Numberofrecordings { get; set; }
		}
	}
}
