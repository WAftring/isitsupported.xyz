using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using Newtonsoft.Json;

namespace isitsupported.xyz.Models
{
    public struct SupportObject
    {
        public string VersionShort { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
	public class ProductModel
	{
        public string Name { get; set; }
        public string ShortName {get; set;}
        public List<SupportObject> SupportList { get; set; }
    
	}
}