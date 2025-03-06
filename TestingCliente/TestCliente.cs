using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingCliente
{
    public class TestCliente
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public TestCliente()
        {
            _driver = new EdgeDriver();
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        //[Fact]
        public void FormularioCreateVacio() 
        {
            _driver.Navigate().GoToUrl("https://localhost:7087/Cliente/Create");
            Thread.Sleep(1000);

            _driver.FindElement(By.ClassName("btn-success")).Click();
            Thread.Sleep(3000);

            //Encontrar los mensajes de error en el html
            var cedulaError = _driver.FindElement(By.Id("cedulaError"));
            var apellidosError = _driver.FindElement(By.Id("apellidosError"));
            var nombresError = _driver.FindElement(By.Id("nombresError"));
            var fechaNacimientoError = _driver.FindElement(By.Id("fechaNacimientoError"));
            var mailError = _driver.FindElement(By.Id("mailError"));
            var telefonoError = _driver.FindElement(By.Id("telefonoError"));
            var direccionError = _driver.FindElement(By.Id("direccionError"));
            Thread.Sleep(2000);

            //Validacion de mensajes de error
            Assert.True(cedulaError.Displayed && cedulaError.Text == "El campo Cédula está vacío.", "No se mostró mensaje de error");
            Assert.True(apellidosError.Displayed && apellidosError.Text == "El campo Apellidos está vacío.", "No se mostró mensaje de error");
            Assert.True(nombresError.Displayed && nombresError.Text == "El campo Nombres está vacío.", "No se mostró mensaje de error");
            Assert.True(fechaNacimientoError.Displayed, "No se mostró mensaje de error");
            Assert.True(mailError.Displayed && mailError.Text == "El campo Mail está vacío.", "No se mostró mensaje de error");
            Assert.True(telefonoError.Displayed && telefonoError.Text == "El campo Teléfono está vacío.", "No se mostró mensaje de error");
            Assert.True(direccionError.Displayed && direccionError.Text == "El campo Dirección está vacío.", "No se mostró mensaje de error");

            Thread.Sleep(2000);

            //Validar que no se redirija a la pagina de inicio
            Assert.Equal("https://localhost:7087/Cliente/Create", _driver.Url);

            Thread.Sleep(2000);

            // Adicional que no corresponde a la prueba
            Console.WriteLine("Prueba exitosa");
            Console.WriteLine("Esperando 10 segundos para cerrar.");
            Thread.Sleep(4000);

            _driver.Quit();
        }

        //[Fact]
        public void formularioCreateValido()
        {
            _driver.Navigate().GoToUrl("https://localhost:7087/Cliente/Create");
            _driver.FindElement(By.Name("Cedula")).SendKeys("1753951027");
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Apellidos")).SendKeys("Añasco Rivadeneira");
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Nombres")).SendKeys("Silvia Ivon");
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("FechaNacimiento")).SendKeys("30/05/2003");
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Mail")).SendKeys("silvia@gmail.com");
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Direccion")).SendKeys("Chillogallo");
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Telefono")).SendKeys("0991426315");
            Thread.Sleep(1000);

            _driver.FindElement(By.ClassName("btn-success")).Click();
            Thread.Sleep(1000);

            //Validar que no se redirija a la pagina de inicio
            Assert.Equal("https://localhost:7087/Cliente", _driver.Url);

            Thread.Sleep(2000);

            //Validar que se creo
            Assert.True(_driver.FindElement(By.XPath("//td[text()='1753951027']")).Displayed, "Registro no creado");
            Thread.Sleep(2000);

            // Adicional que no corresponde a la prueba
            Console.WriteLine("Prueba exitosa");
            Console.WriteLine("Esperando 10 segundos para cerrar.");
            Thread.Sleep(4000);

            _driver.Quit();

            _driver.Quit();
        }

        //[Fact]
        public void formularioCreateValoresInvalidos()
        {
            _driver.Navigate().GoToUrl("https://localhost:7087/Cliente/Create");
            _driver.FindElement(By.Name("Cedula")).SendKeys("absajkd");
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Apellidos")).SendKeys("827387");
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Nombres")).SendKeys("34782");
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("FechaNacimiento")).SendKeys("30/05/2003");
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Mail")).SendKeys("silviagmai");
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Direccion")).SendKeys("Chillogallo");
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Telefono")).SendKeys("dhjsd");
            Thread.Sleep(1000);

            _driver.FindElement(By.ClassName("btn-success")).Click();
            Thread.Sleep(1000);

            Assert.Equal("https://localhost:7087/Cliente/Create", _driver.Url);
            Thread.Sleep(2000);

            //Encontrar los mensajes de error en el html
            var cedulaError = _driver.FindElement(By.Id("cedulaError"));
            var apellidosError = _driver.FindElement(By.Id("apellidosError"));
            var nombresError = _driver.FindElement(By.Id("nombresError"));
            var mailError = _driver.FindElement(By.Id("mailError"));
            var telefonoError = _driver.FindElement(By.Id("telefonoError"));
            Thread.Sleep(2000);

            //Validacion de mensajes de error
            Assert.True(cedulaError.Displayed && cedulaError.Text == "Solo se admiten números en el campo Cédula.", "No se mostró mensaje de error");
            Assert.True(apellidosError.Displayed && apellidosError.Text == "Solo se admiten letras y espacios en el campo Apellidos.", "No se mostró mensaje de error");
            Assert.True(nombresError.Displayed && nombresError.Text == "Solo se admiten letras y espacios en el campo Nombres.", "No se mostró mensaje de error");
            Assert.True(mailError.Displayed && mailError.Text == "Por favor, ingresar un correo en el formato válido.", "No se mostró mensaje de error");
            Assert.True(telefonoError.Displayed && telefonoError.Text == "Solo se admiten números en el campo Teléfono.", "No se mostró mensaje de error");
            Thread.Sleep(2000);

            //Validar que no se redirija a la pagina de inicio
            Assert.Equal("https://localhost:7087/Cliente/Create", _driver.Url);

            // Adicional que no corresponde a la prueba
            Console.WriteLine("Prueba exitosa");
            Console.WriteLine("Esperando 10 segundos para cerrar.");
            Thread.Sleep(4000);

            _driver.Quit();
        }

        //[Fact]
        public void FormularioEditVacio()
        {
            _driver.Navigate().GoToUrl("https://localhost:7087/Cliente/Edit?codigo=9");
            Thread.Sleep(1000);

            //Vaciar todos los campos
            _driver.FindElement(By.Name("Cedula")).Clear();
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Apellidos")).Clear();
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Nombres")).Clear();
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("FechaNacimiento")).Clear();
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Mail")).Clear();
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Direccion")).Clear();
            Thread.Sleep(1000);
            _driver.FindElement(By.Name("Telefono")).Clear();
            Thread.Sleep(1000);

            _driver.FindElement(By.ClassName("btn-primary")).Click();
            Thread.Sleep(3000);

            //Encontrar los mensajes de error en el html
            var cedulaError = _driver.FindElement(By.Id("cedulaError"));
            var apellidosError = _driver.FindElement(By.Id("apellidosError"));
            var nombresError = _driver.FindElement(By.Id("nombresError"));
            var fechaNacimientoError = _driver.FindElement(By.Id("fechaNacimientoError"));
            var mailError = _driver.FindElement(By.Id("mailError"));
            var telefonoError = _driver.FindElement(By.Id("telefonoError"));
            var direccionError = _driver.FindElement(By.Id("direccionError"));
            Thread.Sleep(2000);

            //Validacion de mensajes de error
            Assert.True(cedulaError.Displayed && cedulaError.Text == "El campo Cédula está vacío.", "No se mostró mensaje de error");
            Assert.True(apellidosError.Displayed && apellidosError.Text == "El campo Apellidos está vacío.", "No se mostró mensaje de error");
            Assert.True(nombresError.Displayed && nombresError.Text == "El campo Nombres está vacío.", "No se mostró mensaje de error");
            Assert.True(fechaNacimientoError.Displayed, "No se mostró mensaje de error");
            Assert.True(mailError.Displayed && mailError.Text == "El campo Mail está vacío.", "No se mostró mensaje de error");
            Assert.True(telefonoError.Displayed && telefonoError.Text == "El campo Teléfono está vacío.", "No se mostró mensaje de error");
            Assert.True(direccionError.Displayed && direccionError.Text == "El campo Dirección está vacío.", "No se mostró mensaje de error");

            Thread.Sleep(2000);

            //Validar que no se redirija a la pagina de inicio
            Assert.Equal("https://localhost:7087/Cliente/Edit", _driver.Url);

            Thread.Sleep(2000);

            // Adicional que no corresponde a la prueba
            Console.WriteLine("Prueba exitosa");
            Console.WriteLine("Esperando 10 segundos para cerrar.");
            Thread.Sleep(4000);

            _driver.Quit();
        }

        [Fact]
        public void FormularioEditValido()
        {
            _driver.Navigate().GoToUrl("https://localhost:7087/Cliente/Edit?codigo=9");
            Thread.Sleep(1000);

            //Vaciar todos los campos
            var cedulaField = _driver.FindElement(By.Name("Cedula"));
            cedulaField.Clear();
            cedulaField.SendKeys("2100219183");
            Thread.Sleep(1000);
            
            var apellidosField = _driver.FindElement(By.Name("Apellidos"));
            apellidosField.Clear();
            apellidosField.SendKeys("Fernandez Lopez");
            Thread.Sleep(1000);

            var nombresField = _driver.FindElement(By.Name("Nombres"));
            nombresField.Clear();
            nombresField.SendKeys("Juan Luis");
            Thread.Sleep(1000);

            _driver.FindElement(By.ClassName("btn-primary")).Click();
            Thread.Sleep(3000);

            //Validar que se redirija a la pagina de inicio
            Assert.Equal("https://localhost:7087/Cliente", _driver.Url);

            //Validar que se creo
            Assert.True(_driver.FindElement(By.XPath("//td[text()='2100219183']")).Displayed, "Registro no modificado");
            Thread.Sleep(2000);

            Thread.Sleep(2000);

            // Adicional que no corresponde a la prueba
            Console.WriteLine("Prueba exitosa");
            Console.WriteLine("Esperando 10 segundos para cerrar.");
            Thread.Sleep(4000);

            _driver.Quit();
        }

        //[Fact]
        public void formularioDeleteCorrecto()
        {
            //Para esperar que el formulario de creacion valido se ejecute
            Thread.Sleep(25000);

            _driver.Navigate().GoToUrl("https://localhost:7087/Cliente/Delete?codigo=7");
            Thread.Sleep(2000);

            //Encontrar el botón de eliminación
            _driver.FindElement(By.ClassName("btn-danger")).Click();
            Thread.Sleep(1000);

            Assert.Equal("https://localhost:7087/Cliente", _driver.Url);
            Thread.Sleep(2000);

            //Intentar ver si el elemento existe
            var registroEliminado = _driver.FindElements(By.XPath("//td[text()='0708091011']"));

            Assert.Empty(registroEliminado);
            Thread.Sleep(2000);

            // Adicional que no corresponde a la prueba
            Console.WriteLine("Prueba exitosa");
            Console.WriteLine("Esperando 10 segundos para cerrar.");
            Thread.Sleep(4000);

            _driver.Quit();
        }
    }
}
