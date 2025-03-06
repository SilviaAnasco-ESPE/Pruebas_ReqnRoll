using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollTestProject.Utilities
{
    public static class WebDriverManager
    {
        public static IWebDriver GetDriver(string navegador) 
        {
            return navegador.ToLower() switch
            {
                "firefox" => new FirefoxDriver(),
                "chrome" => new ChromeDriver(),
                "edge" => new EdgeDriver(),
                _ => throw new ArgumentNullException("Navegador no soportado.")
            };
        }
    }
}
