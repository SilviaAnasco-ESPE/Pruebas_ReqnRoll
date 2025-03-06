using System;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Reqnroll;
using ReqnrollTestProject.Utilities;
using TDDTestingMVC2.Data;
using FluentAssertions;

namespace ReqnrollTestProject.StepDefinitions
{
    [Binding]
    public class PedidoCrearStepDefinitions
    {
        private IWebDriver _driver;
        private static ExtentReports _extent;
        private ExtentTest _test;
        private readonly ScenarioContext _scenarioContext;

        public PedidoCrearStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var sparkReporter = new ExtentSparkReporter("ExtentReport2.html");
            _extent = new ExtentReports();
            _extent.AttachReporter(sparkReporter);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver = WebDriverManager.GetDriver("edge");
            _test = _extent.CreateTest(_scenarioContext.ScenarioInfo.Title);

        }

        [Given("Usuario en la página de crear un pedido")]
        public void GivenUsuarioEnLaPaginaDeCrearUnPedido()
        {
            _driver.Navigate().GoToUrl("https://localhost:7087/Pedidos/Create");
            _test.Log(Status.Info, "Usuario navega a la pagina de crear pedidos");
        }

        [When("Llena el formulario con")]
        public void WhenLlenaElFormularioCon(DataTable dataTable)
        {
            var row = dataTable.Rows[0]; // Primera fila de la tabla de SpecFlow
            string clienteIDEsperado = row["ClienteId"].Trim();
            string montoEsperado = row["Monto"];
            string estadoEsperado = row["Estado"].Trim();


            _driver.FindElement(By.Name("ClienteID")).SendKeys(clienteIDEsperado);
            _driver.FindElement(By.Name("Monto")).SendKeys(montoEsperado);

            IWebElement dropdown = _driver.FindElement(By.Name("Estado"));
            SelectElement select = new SelectElement(dropdown);

            // Selecciona el cliente por su valor (debe coincidir con el value en el <option>)
            select.SelectByValue(estadoEsperado.ToString());

            _test.Log(Status.Info, $"Usuario ingresa cliente id: {clienteIDEsperado}, monto: {montoEsperado} y estado: {estadoEsperado}");
        }

        [When("Clic en el boton de Crear Pedido")]
        public void WhenClicEnElBotonDeCrearPedido()
        {
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            _test.Log(Status.Info, "El usuario hace clic en el botón de Crear pedido");
        }

        [Then("Se redirige a la Lista de Pedidos")]
        public void ThenSeRedirigeALaListaDePedidos()
        {
            Assert.Equal("https://localhost:7087/Pedidos", _driver.Url.Trim(), ignoreCase: true);
            _test.Log(Status.Pass, "El usuario fue redirigido a la lista de pedidos");
        }

        [Then("El pedido aparece en la lista")]
        public void ThenElPedidoApareceEnLaLista(DataTable dataTable)
        {
            IWebElement table = _driver.FindElement(By.ClassName("table"));
            IList<IWebElement> filas = table.FindElements(By.TagName("tr"));
            IWebElement ultimaFila = filas.Last();
            IList<IWebElement> celdas = ultimaFila.FindElements(By.TagName("td"));

            // Extrae los valores de la tabla HTML
            string clienteIDTabla = celdas[1].Text.Trim();
            string montoTabla = celdas[3].Text.Trim();
            string estadoTabla = celdas[4].Text.Trim();

            // Obtiene los valores esperados del DataTable de SpecFlow
            var row = dataTable.Rows[0]; // Primera fila de la tabla de SpecFlow
            string clienteIDEsperado = row["ClienteId"].Trim();
            string montoEsperado = row["Monto"];
            string estadoEsperado = row["Estado"].Trim();

            // Verifica que los valores coincidan usando aserciones de NUnit
            clienteIDTabla.Should().Be(clienteIDEsperado);
            montoTabla.Should().Be(montoEsperado);
            estadoTabla.Should().Be(estadoEsperado);


            _test.Log(Status.Pass, "El pedido se visualiza en la tabla");
        }


        //Escenario 2

        [Given("Usuario se dirige a la página de crear un pedido")]
        public void GivenUsuarioSeDirigeALaPaginaDeCrearUnPedido()
        {
            _driver.Navigate().GoToUrl("https://localhost:7087/Pedidos/Create");
        }

        [When("Llena el formulario con los valores")]
        public void WhenLlenaElFormularioConLosValores(DataTable dataTable)
        {
            var row = dataTable.Rows[0]; // Primera fila de la tabla de SpecFlow
            string clienteIDAInsertar = row["ClienteId"].Trim();
            string montoAInsertar = row["Monto"];
            string estadoAInsertar = row["Estado"].Trim();


            _driver.FindElement(By.Name("ClienteID")).SendKeys(clienteIDAInsertar);
            _driver.FindElement(By.Name("Monto")).SendKeys(montoAInsertar);

            IWebElement dropdown = _driver.FindElement(By.Name("Estado"));
            SelectElement select = new SelectElement(dropdown);

            // Selecciona el cliente por su valor (debe coincidir con el value en el <option>)
            select.SelectByValue(estadoAInsertar.ToString());

            _test.Log(Status.Info, $"Usuario ingresa cliente id: {clienteIDAInsertar}, monto: {montoAInsertar} y estado: {estadoAInsertar}");
        }

        [When("Clic en el boton Crear Pedido")]
        public void WhenClicEnElBotonCrearPedido()
        {
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            _test.Log(Status.Info, "El usuario hace clic en el botón de Crear pedido");
        }

        [Then("Permanece en la misma página de creación")]
        public void ThenPermaneceEnLaMismaPaginaDeCreacion()
        {
            _driver.Url.Should().Contain("/Pedidos/Create");
            _test.Log(Status.Pass, "El usuario se mantiene en la página de creación");
        }

        [Then("Se muestra un mensaje de error {string}")]
        public void ThenSeMuestraUnMensajeDeError(string mensajeEsperado)
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
