using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace scrape
{
    class Product
    {
        static char[] SpecialCharArray = { '\n', '\t', '\r' };
        public struct SupportObject
        {
            public string VersionShort { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }

            public SupportObject(string _VersionShort, string _StartDate, string _EndDate)
            {
                VersionShort = _VersionShort;
                StartDate = _StartDate;
                EndDate = _EndDate;
            }
            public string ToString()
            {
                return String.Format($"{VersionShort} {StartDate} {EndDate}");
            }
        }
        public string Name { get; set; }
        public string ShortName {get; set;}
        public List<SupportObject> SupportList { get; set; }

        public Product(string _Name, string _ShortName, string Url)
        {
            Name = _Name;
            ShortName = _ShortName;
            HtmlWeb Web = new HtmlWeb();

            var HtmlDoc = Web.Load(Url);
            var Node = HtmlDoc.DocumentNode.SelectNodes("//tbody/tr/td");

            SupportList = new List<SupportObject>();

            for (int i = 0; i < Node.Count; i++)
            {
                if (Node[i].InnerText.Contains("Version"))
                {
                    var Version = Node[i].InnerText;
                    var StartDate = Node[i].NextSibling.NextSibling.FirstChild.NextSibling.InnerText;
                    var EndDate = Node[i].NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                    SupportList.Add(new SupportObject(RemoveExtraCharacters(Version).Split(" ")[1],
                        RemoveExtraCharacters(StartDate),
                        RemoveExtraCharacters(EndDate)));
                }
            }
        }

        string RemoveExtraCharacters(string Input)
        {
            foreach (var SpecialChar in SpecialCharArray)
            {
                Input = Input.Replace(SpecialChar, ' ');
            }
            return Input.Trim();
        }
    }
}