using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace scrape
{
    class Program
    {
        static Tuple<string,string, string> WinEnterprise = new Tuple<string, string, string>("Windows 10","win10", "https://docs.microsoft.com/en-us/lifecycle/products/windows-10-enterprise-and-education");
        static char[] SpecialCharArray = { '\n', '\t', '\r' };

        static void Main(string[] args)
        {
            Console.WriteLine("scrape: Scrape product website for specific support dates");
            bool bPrint = false;
            if(args.Length == 0)
            {
                Console.WriteLine("-p\tPrint the output");
                Console.WriteLine("-f\tWrite the output to a file");
                return;
            }
            if(args[0] == "-p")
                bPrint = true;
            
            ProcessSupportList(bPrint);
        }

        static void ProcessSupportList(bool Print)
        {
            List<Tuple<string, string, string>> ProcessList = new List<Tuple<string, string, string>>();
            List<Product> ProductList = new List<Product>();
            ProcessList.Add(WinEnterprise);

            foreach (var Process in ProcessList)
            {
                ProductList.Add(new Product(Process.Item1, Process.Item2, Process.Item3));
            }

            string ProductJson = JsonConvert.SerializeObject(ProductList);
            
            if(Print)
            {
               Console.WriteLine(ProductJson);
            }
            else
            {
                File.WriteAllText("products.json", ProductJson);
            }
        }
    }
}

