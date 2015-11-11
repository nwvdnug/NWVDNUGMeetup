﻿using System;
using System.Net.Http;
using System.Xml.Linq;
using System.Linq;

namespace NWVDNUGMeetup
{
	/// <summary>
	/// Used to get the Meetup data.
	/// ical: webcal://www.meetup.com/NWVDNUG/events/ical/
	/// outlook: http://www.meetup.com/NWVDNUG/events/ical/
	/// atom: http://www.meetup.com/NWVDNUG/events/atom/
	/// rss: http://www.meetup.com/NWVDNUG/events/rss/
	/// </summary>
	public class MeetupData
	{
		private const string uri_rss = "http://www.meetup.com/NWVDNUG/events/rss/";
		private const string uri_atom = "http://www.meetup.com/NWVDNUG/events/atom/";
		private const string uri_outlook = "http://www.meetup.com/NWVDNUG/events/ical/";
		private const string uri_ical = "webcal://www.meetup.com/NWVDNUG/events/ical/";

		private HttpClient _http;

		/// <summary>
		/// Initializes a new instance of the <see cref="NWVDNUGMeetup.MeetupData"/> class.
		/// </summary>
		public MeetupData ()
		{
			_http = new HttpClient();
		}

		public string GetRssDataAsString()
		{
			var results = _http.GetStringAsync (uri_rss);
			return results.Result;
		}

		public MeetingListInfo GetMeetingData()
		{
			var rssString = GetRssDataAsString ();
			var dataDoc = XDocument.Parse(rssString);

			var result = new MeetingListInfo ();
			var channelNode = dataDoc.Descendants ().First (n =>
				n.Name.ToString () == "channel");

			result.Title = GetNodeValueAsString (channelNode, "title");
			result.Description = GetNodeValueAsString (channelNode, "description");
			result.Link = GetNodeValueAsString (channelNode, "link");

			return result;
		}

		private string GetNodeValueAsString(XElement searchRootNode, string childNodeName)
		{
			var foundNode =  searchRootNode.Descendants ().FirstOrDefault (n =>
				n.Name.ToString () == childNodeName);

			if (foundNode!=null) {
				return  foundNode.Value;
			} 
			else {
				return string.Empty;
			}
		}
	}
}
