using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace scrape
{
    class Program
    {
        static Tuple<string, string> WinEnterprise = new Tuple<string, string>("Windows 10","https://docs.microsoft.com/en-us/lifecycle/products/windows-10-enterprise-and-education");
        static char[] SpecialCharArray = { '\n', '\t', '\r' };
        public struct SupportObject 
        {
            public string VersionShort;
            public string StartDate;
            public string EndDate;

            public string ToString()
            {
                return String.Format($"{VersionShort} {StartDate} {EndDate}");
            }
        }

        public struct ProductObject
        {
            public string ProductName;
            public SupportObject Support;
        }

        static void Main(string[] args)
        {
            List<Tuple<string, string>> ProcessList = new List<Tuple<string, string>>();
            List<ProductObject> ProductList = new List<ProductObject>();
            ProcessList.Add(WinEnterprise);
            HtmlWeb Web = new HtmlWeb();

            foreach (var Process in ProcessList)
            {
                ProductObject Product = new ProductObject();
                Product.ProductName = Process.Item1;

                var HtmlDoc = Web.Load(Process.Item2);
                var Node = HtmlDoc.DocumentNode.SelectNodes("//tbody/tr/td");

                var Support = new SupportObject();

                for (int i = 0; i < Node.Count; i++)
                {
                    if (Node[i].InnerText.Contains("Version"))
                    {
                        // Check the siblings

                        var Version = Node[i].InnerText;
                        var StartDate = Node[i].NextSibling.NextSibling.FirstChild.NextSibling.InnerText;
                        var EndDate = Node[i].NextSibling.NextSibling.NextSibling.NextSibling.InnerText;

                        WinSupport.VersionShort = RemoveExtraCharacters(Version).Split(" ")[1];
                        WinSupport.StartDate = RemoveExtraCharacters(StartDate);
                        WinSupport.EndDate = RemoveExtraCharacters(EndDate);

                        string result = JsonConvert.SerializeObject(WinSupport);

                        Console.WriteLine(result);
                    }
                }

                Product.Support = Support;
            }

        }

        static string RemoveExtraCharacters(string Input)
        {
            foreach(var SpecialChar in SpecialCharArray)
            {
                Input = Input.Replace(SpecialChar, ' ');
            }
            return Input.Trim();
        }
    }
}

