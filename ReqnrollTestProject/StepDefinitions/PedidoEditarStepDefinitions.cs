using System;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using Reqnroll;
using ReqnrollTestProject.Utilities;
using OpenQA.Selenium.Support.UI;
using FluentAssertions;

namespace ReqnrollTestProject.StepDefinitions
{
    [Binding]
    public class PedidoEditarStepDefinitions
    {
        private IWebDriver _driver;
        private static ExtentReports _extent;
        private ExtentTest _test;
        private readonly ScenarioContext _scenarioContext;

        public PedidoEditarStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var sparkReporter = new ExtentSparkReporter("ExtentReport.html");
            _extent = new ExtentReports();
            _extent.AttachReporter(sparkReporter);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver = WebDriverManager.GetDriver("edge");
            _test = _extent.CreateTest(_scenarioContext.ScenarioInfo.Title);

        }

        [Given("Usuario hace clic en el botón de editar de un pedido de id: {int}")]
        public void GivenUsuarioHaceClicEnElBotonDeEditarDeUnPedidoDeId(int p0)
        {
            _driver.Navigate().GoToUrl("https://localhost:7087/Pedidos/Edit?codigo="+p0);
            _test.Log(Status.Info, "Usuario navega a la pagina de editar pedido");
        }

        [When("Cambia los valores del formulario con")]
        public void WhenCambiaLosValoresDelFormularioCon(DataTable dataTable)
        {
            var row = dataTable.Rows[0]; // Primera fila de la tabla de SpecFlow
            string clienteIDCambiado = row["ClienteId"].Trim();
            string montoCambiado = row["Monto"];
            string estadoCambiado = row["Estado"].Trim();


            _driver.FindElement(By.Name("ClienteID")).Clear();
            _driver.FindElement(By.Name("ClienteID")).SendKeys(clienteIDCambiado);

            _driver.FindElement(By.Name("Monto")).Clear();
            _driver.FindElement(By.Name("Monto")).SendKeys(montoCambiado);

            IWebElement dropdown = _driver.FindElement(By.Name("Estado"));
            SelectElement select = new SelectElement(dropdown);

            select.SelectByValue(estadoCambiado.ToString());

            _test.Log(Status.Info, $"Usuario cambia los datos por cliente id: {clienteIDCambiado}, monto: {montoCambiado} y estado: {estadoCambiado}");
        }

        [When("Clic en el boton de Editar Pedido")]
        public void WhenClicEnElBotonDeEditarPedido()
        {
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            _test.Log(Status.Info, "El usuario hace clic en el botón de Editar pedido");
        }

        [Then("Se redirige a la Lista")]
        public void ThenSeRedirigeALaLista()
        {
            Assert.Equal("https://localhost:7087/Pedidos", _driver.Url.Trim(), ignoreCase: true);
            _test.Log(Status.Pass, "El usuario fue redirigido a la lista de pedidos");
        }

        [Then("El pedido aparece editado en la lista")]
        public void ThenElPedidoApareceEditadoEnLaLista(DataTable dataTable)
        {
            var row = dataTable.Rows[0];
            string idPedidoCambiado = row["PedidoID"].Trim(); 
            string clienteIDEsperado = row["ClienteId"].Trim();
            string montoEsperado = row["Monto"];
            string estadoEsperado = row["Estado"].Trim();

      
            IWebElement table = _driver.FindElement(By.ClassName("table"));
            IList<IWebElement> filas = table.FindElements(By.TagName("tr"));

            IWebElement filaCorrecta = null;

            foreach (var fila in filas.Skip(1)) 
            {
                IList<IWebElement> celdas = fila.FindElements(By.TagName("td"));

                if (celdas.Count > 0)
                {
                    string pedidoIDTabla = celdas[0].Text.Trim(); 
                    if (pedidoIDTabla == idPedidoCambiado)
                    {
                        filaCorrecta = fila;
                        break; 
                    }
                }
            }

            filaCorrecta.Should().NotBeNull($"No se encontró un pedido con ID {idPedidoCambiado}");

            
            IList<IWebElement> celdasCorrectas = filaCorrecta.FindElements(By.TagName("td"));
            string clienteIDTabla = celdasCorrectas[1].Text.Trim();
            string montoTabla = celdasCorrectas[3].Text.Trim();
            string estadoTabla = celdasCorrectas[4].Text.Trim();

            // Verifica que los valores coincidan usando aserciones de FluentAssertions
            clienteIDTabla.Should().Be(clienteIDEsperado, "El ClienteID en la tabla no coincide");
            montoTabla.Should().Be(montoEsperado, "El Monto en la tabla no coincide");
            estadoTabla.Should().Be(estadoEsperado, "El Estado en la tabla no coincide");

            _test.Log(Status.Pass, "El pedido editado se visualiza correctamente en la tabla");
        }


        [When("Deja los campos vacios")]
        public void WhenDejaLosCamposVacios()
        {

            _driver.FindElement(By.Name("ClienteID")).Clear();

            _driver.FindElement(By.Name("Monto")).Clear();

            _test.Log(Status.Info, $"Usuario deja los campos vacios");
        }

        [Then("Permanece en la pagina de Edicion")]
        public void ThenPermaneceEnLaPaginaDeEdicion()
        {
            _driver.Url.Should().Contain("/Pedidos/Edit");
            _test.Log(Status.Pass, "El usuario se mantiene en la página de creación");
        }


        [Then("Se muestra el error {string}")]
        public void ThenSeMuestraElError(string mensajeEsperado)
        {
            IWebElement mensajeError = _driver.FindElement(By.ClassName("text-danger"));
            var error = mensajeError.Text;
            error.Should().Contain(mensajeEsperado);
            _test.Log(Status.Pass, "Se visualiza un mensaje de error");
        }

        [AfterScenario]
        public void Down()
        {
            _driver.Quit();
            _extent.Flush();
        }
    }
}
