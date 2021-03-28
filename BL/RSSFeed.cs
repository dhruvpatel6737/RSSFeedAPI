using RSSFeedAPI.Model;
using RSSFeedAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace RSSFeedAPI.BL
{
    public static class RSSFeed
    {
        public static async Task<Dictionary<string, IEnumerable<string>>> FilterNotActiveCompanies(Dictionary<string, IEnumerable<string>> companies, int numOfDays)
        {
            var companyActivities = await GetCompanyActivityData(companies);
            var notActiveCompanies = companyActivities.Where(c => c.NoActivityDays > numOfDays);
            var retVal = Utilities.ToDictionary<CompanyActivity, string, IEnumerable<string>>(notActiveCompanies,
        delegate (CompanyActivity company) { return company.CompanyName; },
        delegate (CompanyActivity company) { return company.FeedURLs; });
            return retVal;
        }

        public static async Task<CompanyActivity[]> GetCompanyActivityData(Dictionary<string, IEnumerable<string>> companies)
        {
            // create a tasks for each keyvalue pair to check company activity date
            var tasks = companies.Select(async c =>
            {
                try
                {
                    return await CheckCompanyActivity(c);
                }
                catch (Exception ex)
                {
                    return new CompanyActivity(c.Key, c.Value, DateTime.MinValue);
                }
            });
            return await Task.WhenAll(tasks);
        }

        private static Task<CompanyActivity> CheckCompanyActivity(KeyValuePair<string, IEnumerable<string>> companyKeyValue)
        {
            var lastActivityDate = DateTime.MinValue;
            var urls = companyKeyValue.Value != null ? companyKeyValue.Value : new List<string>();
            foreach (var url in urls)
            {
                try
                {
                    var feed = SyndicationFeed.Load(XmlReader.Create(url));
                    if (feed != null)
                    {
                        lastActivityDate = feed.LastUpdatedTime.DateTime > lastActivityDate ? feed.LastUpdatedTime.DateTime : lastActivityDate;
                    }
                }
                catch (Exception ex)
                {
                    //Handle Exception
                }
            }
            return Task.FromResult(new CompanyActivity(companyKeyValue.Key, companyKeyValue.Value, lastActivityDate));
        }
    }
}
