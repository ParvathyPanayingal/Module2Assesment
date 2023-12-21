using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyNibblesSel.Utilities
{
    internal class ScreenShots
    {
        public static string TakeScreenShot(IWebDriver driver)
        {
            ITakesScreenshot iss = (ITakesScreenshot)driver;
            Screenshot ss = iss.GetScreenshot();

            string currdir = Directory.GetParent(@"../../../").FullName;
            string? filepath = currdir + "/Screenshots/ss_" +
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

            ss.SaveAsFile(filepath);
            return filepath;

        }
    }
}
