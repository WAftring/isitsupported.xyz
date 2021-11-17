// APIController.cs

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using isitsupported.xyz.Models;

namespace isitsupported.xyz.Controllers
{
	public class ApiController : Controller
	{
		private readonly ILogger<ApiController> _logger;
		public ApiController(ILogger<ApiController> logger)
		{
			_logger = logger;
		}
		public string Enumerate()
		{
			// NOTE(will): List all the currently listed products
			return "Hello World";
		}
		public string Error()
		{
			return "Error";
		}
	}
}