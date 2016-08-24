//still needed to addpop-ups processing and filedownload control, some threading, do OOP version

using System;

using System.IO;

using OpenQA.Selenium;

using OpenQA.Selenium.Support;

using OpenQA.Selenium.Support.UI;

using OpenQA.Selenium.Firefox;

using System.Data.Linq;

using System.Threading;

namespace TrainingWebCrawler {

	class MainClass {

		public static void Main(string[] args) {

			FirefoxProfile firefoxProfile = new FirefoxProfile();

			string DownloadPath = System.IO.Directory.GetCurrentDirectory().ToString() + "/Downloads";

			firefoxProfile.SetPreference("browser.download.folderList",2);

			firefoxProfile.SetPreference ("browser.download.dir", DownloadPath);

			firefoxProfile.SetPreference ("browser.helperApps.neverAsk.saveToDisk","finam/expotfile, text/plain, text/csv");

			IWebDriver WebDriver = new FirefoxDriver(firefoxProfile);

			Console.WriteLine("WebDriver configured instance is created!");
	
			WebDriver.Url = "http://www.finam.ru/profile/akcii-usa-bats/microsoft-corp/export/";

			WebDriver.Navigate();
			 // choose an exchange from dropdown list
			IWebElement dropdown_exchange = WebDriver.FindElement(By.XPath("//div[@class='finam-ui-quote-selector-market']"));
			IWebElement arrow = dropdown_exchange.FindElement(By.XPath("//div[@class='finam-ui-quote-selector-arrow']"));
			arrow.Click ();
			IWebElement list_exchange = WebDriver.FindElement(By.XPath("//div[@class='finam-ui-dropdown-list'][1]"));
			IWebElement input_exchange = list_exchange.FindElement(By.XPath("//a[.='ETF']"));
			input_exchange.Click ();
			 // choose a stock from dropdown list
			IWebElement dropdown_stock = WebDriver.FindElement(By.XPath("//div[@class='finam-ui-quote-selector-quote']"));
			IWebElement arrow2 = dropdown_stock.FindElement(By.XPath("div[@class='finam-ui-quote-selector-arrow']"));
			arrow2.Click ();
			IWebElement list_stock = WebDriver.FindElement(By.XPath("//div[@class='finam-ui-dropdown-list'][2]"));
			IWebElement input_stock = list_stock.FindElement(By.XPath("//a[.='Euro Trust']"));
			input_stock.Click ();

			// choose a beginning date of the interval
			IWebElement calendar = WebDriver.FindElement(By.Id("issuer-profile-export-from"));
			calendar.Click();
			SelectElement month = new SelectElement(WebDriver.FindElement (By.ClassName("ui-datepicker-month")));
			month.SelectByValue("0");
			SelectElement year = new SelectElement(WebDriver.FindElement (By.ClassName("ui-datepicker-year")));
			year.SelectByValue("2011");
			IWebElement day = WebDriver.FindElement (By.XPath ("//a[@class='ui-state-default'][.='1']"));
			day.Click ();

			// choose an end date of the interval
			IWebElement calendar_end = WebDriver.FindElement(By.Id("issuer-profile-export-to"));
			calendar_end.Click();
			SelectElement month_end = new SelectElement(WebDriver.FindElement (By.ClassName("ui-datepicker-month")));
			month_end.SelectByValue("3");
			SelectElement year_end = new SelectElement(WebDriver.FindElement (By.ClassName("ui-datepicker-year")));
			year_end.SelectByValue("2012");
			IWebElement day_end = WebDriver.FindElement (By.XPath ("//a[@class='ui-state-default'][.='22']"));
			day_end.Click ();

			// choose a periodicity of data
			IWebElement periodarrow = WebDriver.FindElement(By.XPath("//div[@class='finam-ui-controls-select-arrow']"));
			periodarrow.Click ();
			IWebElement period = WebDriver.FindElement (By.XPath ("//a[.='тики']"));
			period.Click ();

			// choose a data format
			IWebElement datatypearrow = WebDriver.FindElement(By.XPath("//div[@class='finam-ui-controls-select-title'][.='.txt']"));
			datatypearrow.Click();
			IWebElement datatype = WebDriver.FindElement(By.XPath ("//a[.='.csv']"));
			datatype.Click ();
	 
			// click a download button
			IWebElement button = WebDriver.FindElement(By.ClassName("finam-ui-dialog-button-cancel"));
			button.Submit();
			Thread.Sleep (6000);
			Console.WriteLine("saved!");
			Console.ReadLine();
		}


	}

}

