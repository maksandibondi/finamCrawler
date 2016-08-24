using System;

using System.IO;

using OpenQA.Selenium;

using OpenQA.Selenium.Firefox;

using OpenQA.Selenium.Support;

using OpenQA.Selenium.Support.UI;

using System.Data.Linq;

using System.Text.RegularExpressions;

using System.Timers;

namespace finamLib {

	public class finamLib {

		public IWebDriver WebDriver;

		string DownloadPath;

		public finamLib() {

			this.DownloadPath = System.IO.Directory.GetCurrentDirectory().ToString() + "/Downloads";

			this.WebDriver = new FirefoxDriver(this.SetProfileForDownloads());
		}

		public FirefoxProfile SetProfileForDownloads() {

			FirefoxProfile firefoxProfile = new FirefoxProfile();

			firefoxProfile.SetPreference("browser.download.folderList",2);

			firefoxProfile.SetPreference ("browser.download.dir", DownloadPath);

			firefoxProfile.SetPreference ("browser.helperApps.neverAsk.saveToDisk","finam/expotfile, text/plain, text/csv");

			return firefoxProfile;

		}

		public void Crawl(string instrumentType, string instrumentName, string beginningdate, string enddate, string periodocity, string format) {

			this.WebDriver.Url = "http://www.finam.ru/profile/akcii-usa-bats/microsoft-corp/export/";
			this.WebDriver.Navigate();


			this.ChooseAnInstrumentType (instrumentType);
			this.ChooseAnInstrument (instrumentName);
			this.ChooseAnInterval (beginningdate, enddate);
			this.ChooseAPeriod (periodocity);
			this.ChooseAFormat (format);

			this.Download ();

		}

		public void ChooseAnInstrumentType(string instrumentType) {

			IWebElement dropdown_exchange = WebDriver.FindElement(By.XPath("//div[@class='finam-ui-quote-selector-market']"));

			IWebElement arrow = dropdown_exchange.FindElement(By.XPath("//div[@class='finam-ui-quote-selector-arrow']"));

			arrow.Click ();

			IWebElement list_exchange = WebDriver.FindElement(By.XPath("//div[@class='finam-ui-dropdown-list'][1]"));

			string exchangeXpath = "//a[.='" + instrumentType + "']";

			IWebElement input_exchange = list_exchange.FindElement(By.XPath(exchangeXpath));

			input_exchange.Click ();
		}

		public void ChooseAnInstrument(string instrumentName) {

			IWebElement dropdown_stock = WebDriver.FindElement(By.XPath("//div[@class='finam-ui-quote-selector-quote']"));

			IWebElement arrow2 = dropdown_stock.FindElement(By.XPath("div[@class='finam-ui-quote-selector-arrow']"));

			arrow2.Click ();

			IWebElement list_stock = WebDriver.FindElement(By.XPath("//div[@class='finam-ui-dropdown-list'][2]"));

			string stockXpath = "//a[.='" + instrumentName + "']";

			IWebElement input_stock = list_stock.FindElement(By.XPath(stockXpath));

			input_stock.Click ();

		}

		public void ChooseAnInterval(string beginning, string end) {

			
			// choose a beginning date of the interval
			IWebElement calendar = WebDriver.FindElement(By.Id("issuer-profile-export-from"));
			calendar.Click();
			SelectElement month = new SelectElement(WebDriver.FindElement (By.ClassName("ui-datepicker-month")));
			month.SelectByValue(beginning.Substring(3,2));
			SelectElement year = new SelectElement(WebDriver.FindElement (By.ClassName("ui-datepicker-year")));
			year.SelectByValue(beginning.Substring(6,4));

			if (beginning.Substring(0,1) == "0") {

			string xpath = "//a[@class='ui-state-default'][.='" + beginning.Substring(1,1) + "']";

			IWebElement day = WebDriver.FindElement (By.XPath(xpath));
			day.Click ();
			}

			else {

			string xpath = "//a[@class='ui-state-default'][.='" + beginning.Substring(0,2) + "']";

			IWebElement day = WebDriver.FindElement (By.XPath(xpath));
			day.Click ();
			}


			// choose an end date of the interval
			IWebElement calendar_end = WebDriver.FindElement(By.Id("issuer-profile-export-to"));
			calendar_end.Click();

			SelectElement month_end = new SelectElement(WebDriver.FindElement (By.ClassName("ui-datepicker-month")));
			month_end.SelectByValue(end.Substring(3,2));

			SelectElement year_end = new SelectElement(WebDriver.FindElement (By.ClassName("ui-datepicker-year")));
			year_end.SelectByValue(end.Substring(6,4));

			if (end.Substring(0,1) == "0") {

				string xpath = "//a[@class='ui-state-default'][.='" + beginning.Substring(1,1) + "']";

				IWebElement day_end = WebDriver.FindElement (By.XPath(xpath));
				day_end.Click ();

			}

			else {

				string xpath = "//a[@class='ui-state-default'][.='" + beginning.Substring(0,2) + "']";

				IWebElement day_end = WebDriver.FindElement (By.XPath(xpath));
				day_end.Click ();
			}

		}

		public void ChooseAPeriod(string periodicity) {

			IWebElement periodarrow = WebDriver.FindElement(By.XPath("//div[@class='finam-ui-controls-select-arrow']"));

			periodarrow.Click ();

			string xpath = "//a[.='" + periodicity + "']";

			IWebElement period = WebDriver.FindElement (By.XPath (xpath));

			period.Click ();
		}

		public void ChooseAFormat(string format){

			IWebElement datatypearrow = WebDriver.FindElement(By.XPath("//div[@class='finam-ui-controls-select-title'][.='.txt']"));

			datatypearrow.Click();

			string xpath = "//a[.='." + format + "']";

			IWebElement datatype = WebDriver.FindElement(By.XPath (xpath));

			datatype.Click ();
		}

		public void Download(){

			IWebElement button = WebDriver.FindElement(By.ClassName("finam-ui-dialog-button-cancel"));

			button.Submit();
		}

	}
}