using System;
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
			var channelNode = GetChildNode( dataDoc.Root, "channel");

			result.Title = GetNodeValueAsString (channelNode, "title");
			result.Description = GetNodeValueAsString (channelNode, "description");
			result.Link = GetNodeValueAsString (channelNode, "link");
			result.Meetings = GetMeetings (channelNode);
			return result;
		}

		private XElement GetChildNode(XElement searchRootNode, string childNodeName)
		{
			var foundNode =  searchRootNode.Descendants ().FirstOrDefault (n =>
				n.Name.ToString () == childNodeName);

			if (foundNode!=null) {
				return  foundNode;
			} 
			else {
				return null;
			}
		}

		private string GetNodeValueAsString(XElement searchRootNode, string childNodeName)
		{
			var foundNode = GetChildNode (searchRootNode, childNodeName);

			if (foundNode!=null) {
				return foundNode.Value;
			} 
			else {
				return string.Empty;
			}
		}

		private MeetingInfo[] GetMeetings(XElement rootNode)
		{
			if (rootNode.Descendants().Any(d=>d.Name=="item")) {
				return rootNode.Descendants ()
					.Where (d => d.Name == "item")
					.Select (i => {
						var mi = new MeetingInfo ();
						mi.Title = GetNodeValueAsString (i, "title");
						mi.Description = GetNodeValueAsString(i, "description");
						mi.PermaLink = GetNodeValueAsString(i, "guid");
						return mi;
				}).ToArray ();
						
			} else {
				return new MeetingInfo[0];
			}
		}
	}
}

