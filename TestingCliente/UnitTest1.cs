using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace TestingCliente
{
    public class UnitTest1
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        
        public UnitTest1()
        {
            _driver = new EdgeDriver();
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        public void Test1()
        {
            
        }

        public bool EsMailValido(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,5}$");
        }

        //[Theory]
        //[InlineData("usuario@gmail.com", true)]
        //[InlineData("test@empresa.com", true)]
        //[InlineData("test@empresa1.com", true)]
        //[InlineData("sin_arroba.com", false)]

        //public void ValidarEmail_DeberiaDetectarCorreosValidos(string email, bool esperado) 
        //{
        //  bool resultado = EsMailValido(email);
        //  Assert.Equal(esperado, resultado);
        //}

        //[Fact]
        //public void Test_NavegadorGoogle() {
          //  try
            //{
              //  _driver.Navigate().GoToUrl("https://www.bing.com");

//                var buscarTexto = _wait.Until(d => d.FindElement(By.Name("q")));
//
  //              Thread.Sleep(3000);
  //
    //            buscarTexto.SendKeys("Selenium");
    //
      //          Thread.Sleep(2000);
      //
                //buscarTexto.SendKeys(Keys.Enter);
        //       _driver.FindElement(By.Id("search_icon")).Click();

          //      Thread.Sleep(2000);

            //    var resultados = _wait.Until(d => d.FindElements(By.CssSelector("h3")));

//                Assert.True(resultados.Count > 0, "No se encontraron resultados a la búsqueda.");


                //Adicional que no corresponde a la prueba
  //              Console.WriteLine("Prueba exitosa");
    //            Console.WriteLine("Esperando 10 segundos para cerrar.");
      //          Thread.Sleep(10000);

        //    }
//            catch (Exception ex)
//            {
  //              Console.WriteLine("Error: {ex.Message}");
    //        }
      //      finally 
        //    {
          //      _driver.Quit();
          //  }
//        }

        [Fact]
        public void comprobarCamposVacios() 
        {
            try
            {
                _driver.Navigate().GoToUrl("https://www.automationexercise.com/");

                Thread.Sleep(3000);

                _driver.FindElement(By.CssSelector("a[href='/login']")).Click();

                Thread.Sleep(2000);

                _driver.FindElement(By.CssSelector("[data-qa='login-button']")).Click();

                Thread.Sleep(2000);
                var prueba = _driver.FindElement(By.CssSelector("[data-qa='login-email']")).GetAttribute("validationMessage");
                Console.WriteLine("Prueba: "+ prueba);

                var emailInput = _driver.FindElement(By.CssSelector("[data-qa='login-email']"));
                var validationMessage = (string)((IJavaScriptExecutor)_driver).ExecuteScript("return arguments[0].validationMessage;", emailInput);

                var passwordInput = _driver.FindElement(By.CssSelector("[data-qa='login-password']"));
                var validationMessage2 = (string)((IJavaScriptExecutor)_driver).ExecuteScript("return arguments[0].validationMessage;", passwordInput);

                Assert.False(string.IsNullOrEmpty(validationMessage), "No se mostró el mensaje de validación.");
                Assert.False(string.IsNullOrEmpty(validationMessage2), "No se mostró el mensaje de validación2.");

                Console.WriteLine("Mensaje de validación: " + validationMessage);

                Thread.Sleep(10000);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {ex.Message}");
            }
            finally
            {
                _driver.Quit();
            }
        }
    }
}