using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealmWatcher.Http
{
    public class HttpRealmWatcher : SylvanasWatcher
    {
        private HtmlNode GetNodeByClass(HtmlNode node, string tag, string value)
        {
            return node.Descendants(tag).Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value == value)
                                        .First();
        }

        protected override RealmInfo GetRealmInfo()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd(USER_AGENT);

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(REALMS_URL);
            HtmlNode legionInfo = doc.DocumentNode.Descendants("div").Where(d => d.Attributes.Contains("class") &&
                                    d.Attributes["class"].Value == "realm legion").First();

            return new RealmInfo
            {
                Name = GetNodeByClass(legionInfo, "span", "name").InnerText.Trim(),
                Players = GetNodeByClass(legionInfo, "div", "players").InnerText.Trim(),
                Status = GetNodeByClass(legionInfo, "div", "realm_top").Descendants("span").First().InnerText.Trim()
            };
        }
        
        private const string REALMS_URL = "https://firestorm-servers.com/en/welcome/realms";
        private const string USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36 OPR/38.0.2220.41";

    }
}
