using System;
using System.Collections.Generic;
using System.Text;
using PuppeteerSharp;
using System.IO;

namespace PuppeteerSharp.Example
{
    public class Demo
    {
        public async void Execute()
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            var page = await browser.NewPageAsync();
            await page.GoToAsync("https://flights.ctrip.com/itinerary/oneway/SHA-URC?date=2020-10-01");
            var result = await page.GetContentAsync();
            using (FileStream fs = new FileStream("output.html", FileMode.OpenOrCreate, FileAccess.Write))
            {
                using(StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(result);
                    sw.Flush();
                }
            }
        }
    }
}
