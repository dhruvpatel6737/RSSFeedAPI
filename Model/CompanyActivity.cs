using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSSFeedAPI.Model
{
    public class CompanyActivity
    {
        private int _NoActivityDays;
        public CompanyActivity(string _CompanyName, IEnumerable<string> _FeedURLs, DateTime _LastActivityDate)
        {
            this.CompanyName = _CompanyName;
            this.FeedURLs = _FeedURLs;
            this.LastActivityDate = _LastActivityDate;
            //this._NoActivityDays = _LastActivityDate != DateTime.MinValue ? (DateTime.Now - _LastActivityDate).Days : int.MaxValue;
            this._NoActivityDays = (DateTime.Now - _LastActivityDate).Days;
        }
        public string CompanyName { get; set; }
        public IEnumerable<string> FeedURLs { get; set; }
        public DateTime LastActivityDate { get; set; }
        public int NoActivityDays
        {
            get
            {
                return _NoActivityDays;
            }
        }
    }
}
