// APIController.cs

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using isitsupported.xyz.Models;

namespace isitsupported.xyz.Controllers
{
	public class ApiController : Controller
	{

		enum QueryOptions
		{
			CheckSupport,
			Unknown
		}
		private readonly ILogger<ApiController> _logger;
		//public ApiController(){}
		public ApiController(ILogger<ApiController> logger)
		{
			_logger = logger;
		}
		[Route("api")]
		[Route("api/Enumerate")]
		public string Enumerate()
		{
			return System.IO.File.ReadAllText("products.json");
		}

		[Route("api/Query/{product}")]
		public string Query(string product)
		{			
			var ProductList = JsonConvert.DeserializeObject<List<ProductModel>>(System.IO.File.ReadAllText("products.json"));
			var Result = "NOT FOUND";
			foreach(var Product in ProductList)
			{
				if(Product.ShortName == product)
				{
					Result = JsonConvert.SerializeObject(Product);
					break;
				}
			}
			return Result;
		}

		[Route("api/Check/{product}/{version}")]
		public string Check(string product, string version)
        {
        	product = product.ToLower();
        	version = version.ToLower();
			var ProductList = JsonConvert.DeserializeObject<List<ProductModel>>(System.IO.File.ReadAllText("products.json"));
			var Result = "NOT FOUND";
			SupportObject ProductVersion = new SupportObject();
			
			if(GetSupportObject(ProductList, ref ProductVersion, product, version))
			{
				Result = DateTime.Compare(DateTime.Now, DateTime.Parse(ProductVersion.EndDate)) > 0 ? "FALSE" : "TRUE";
			}

			return Result;
		}

		// TODO(will) Figure out how to handle the query parameters...
		[Route("api/Query/{product}/{version}")]
		public string Query(string product, string version)
		{
			product = product.ToLower();
			version = version.ToLower();
			var ProductList = JsonConvert.DeserializeObject<List<ProductModel>>(System.IO.File.ReadAllText("products.json"));
			var Result = "NOT FOUND";
			SupportObject ProductVersion = new SupportObject();
			
			if(GetSupportObject(ProductList, ref ProductVersion, product, version))
			{
				Result = JsonConvert.SerializeObject(ProductVersion);
			}

			return Result;
		}
		private bool GetSupportObject(List<ProductModel> ProductList, ref SupportObject ProductVersion, string product, string version)
		{
			foreach(var Product in ProductList)
			{
				if(Product.ShortName.ToLower() == product)
				{
					foreach(var ProductVersionObject in Product.SupportList)
					{
						if(ProductVersionObject.VersionShort.ToLower() == version)
						{	
							ProductVersion = ProductVersionObject;
							return true;
						}
					}
				}
			}
			return false;
		}

		public string Error()
		{
			return "Error";
		}
	}
}