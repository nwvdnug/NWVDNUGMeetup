using System;

namespace NWVDNUGMeetup
{
	public class MeetingListInfo
	{
		public string Title {
			get;
			set;
		}
		public string Link {
			get;
			set;
		}
		public string Description {
			get;
			set;
		}
		public DateTime LastBuildDate {
			get;
			set;
		}
		public MeetingInfo[] Meetings {
			get;
			set;
		}

		public MeetingListInfo ()
		{
			Meetings = new MeetingInfo[0];
		}
	}
}

