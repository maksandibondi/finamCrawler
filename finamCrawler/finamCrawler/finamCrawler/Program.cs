using System;

using System.IO;

using finamLib;

using System.Text.RegularExpressions;

namespace finamCrawler {

	class MainClass {

		public static void Main(string[] args)

		{

			Console.WriteLine("crawl!");

			finamLib.finamLib crawler = new finamLib.finamLib();

			crawler.Crawl ("ETF", "Canada Index", "01/11/2011", "09/10/2015", "тики", "csv");

			crawler.WebDriver.Quit();

		}
	}  

}