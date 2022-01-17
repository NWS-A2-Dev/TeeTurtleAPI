using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;
using Common;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace Scrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            List<TShirt> list = new List<TShirt>();

            int count = 1;

            while (count < 6)
            {
                HtmlDocument doc = web.Load($"https://www.teeturtle.com/collections/all-shirt-designs?page={count}");

                IEnumerable<TShirt> tshirts = doc.DocumentNode.SelectNodes("//div[contains(@class, 'product-wrapper')]")
                    .Select(i => new TShirt
                    {
                        Name = i.SelectSingleNode(".//h6").InnerHtml,
                        Link = i.SelectSingleNode(".//a")
                            .GetAttributeValue("href", ""),
                        ImageURL = i.SelectSingleNode(".//img").GetAttributeValue("data-original", ""),
                        Price = i.SelectSingleNode(".//span").InnerHtml,
                        Flag = i.SelectSingleNode(".//div[@class='badge-content']")
                            .Then(o => o != null ? o.InnerHtml : "")
                    })
                    .ToArray();

                foreach (TShirt tshirt in tshirts)
                {
                    HtmlDocument doc2 = web.Load(tshirt.Link);

                    tshirt.Description = doc2.DocumentNode.SelectSingleNode("//div[contains(@class, 'product-description')]//p").InnerHtml;
                }

                list.AddRange(tshirts);
                ++count;
            }
            
            string data = JsonConvert.SerializeObject(list);

            File.WriteAllText("api.json", data);
        }
    }
}
