using Microsoft.AspNetCore.Mvc;
using RSSFeedAPI.BL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSSFeedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpGet]
        [Route("api/Companies/NotActive")]
        public async Task<IActionResult> Get(int numDays)
        {
            Dictionary<string, IEnumerable<string>> companies = new Dictionary<string, IEnumerable<string>>();
            companies = companies == null ? new Dictionary<string, IEnumerable<string>>() : companies;
            companies.Add("Newyork Times", new string[] { "https://rss.nytimes.com/services/xml/rss/nyt/HomePage.xml", "https://rss.nytimes.com/services/xml/rss/nyt/HomePage.xml" });
            companies.Add("NBC News", new string[] { "http://podcastfeeds.nbcnews.com/HL4TzgYC" });
            companies.Add("Libsyn", new string[] { "https://lincolnproject.libsyn.com/rss" });
            companies.Add("Apology line", new string[] { "https://rss.art19.com/apology-line" });
            companies.Add("Unraveled", new string[] { "https://rss.acast.com/unraveled" });
            return Ok(await RSSFeed.FilterNotActiveCompanies(companies, numDays));
        }
        [HttpPost]
        [Route("api/Companies/FilterNotActive")]
        public async Task<IActionResult> FetchNonActiveCompanies(int numDays, Dictionary<string, IEnumerable<string>> companies)
        {
            return Ok(await RSSFeed.FilterNotActiveCompanies(companies, numDays));
        }
    }
}
