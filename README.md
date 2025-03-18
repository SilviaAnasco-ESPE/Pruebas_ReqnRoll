# Aplicación Web con ASP.NET Core, Reqnroll y Selenium WebDriver

## Descripción
Este repositorio contiene una aplicación web desarrollada en **ASP.NET Core** utilizando el patrón **Modelo-Vista-Controlador (MVC)**. Además, se han implementado pruebas automatizadas utilizando **Reqnroll** y **Selenium WebDriver** para validar su correcto funcionamiento. La aplicación está conectada a una base de datos en **Microsoft SQL Server Management Studio**.

## Tecnologías Utilizadas
- **ASP.NET Core**: Framework para el desarrollo de aplicaciones web.
- **MVC (Modelo-Vista-Controlador)**: Arquitectura utilizada para estructurar la aplicación.
- **C#**: Lenguaje de programación utilizado.
- **Selenium WebDriver**: Herramienta para la automatización de pruebas en navegadores.
- **Reqnroll**: Framework para pruebas de aceptación basado en Gherkin.
- **Microsoft SQL Server Management Studio**: Base de datos utilizada para el almacenamiento de datos.

## Instalación y Ejecución
### 1. Clonar el Repositorio
```sh
 git clone https://github.com/usuario/proyecto-web-aspnetcore.git
 cd TDDTestingMVC2
```

### 2. Restaurar Dependencias
```sh
dotnet restore
```

### 3. Configurar la Base de Datos
1. Asegúrate de tener **Microsoft SQL Server** instalado y en ejecución.
2. Configura la conexión en `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=TU_SERVIDOR;Database=TU_BASE_DE_DATOS;User Id=TU_USUARIO;Password=TU_CONTRASEÑA;"
}
```
3. Ejecuta las migraciones para generar la base de datos:
```sh
dotnet ef database update
```

## Contribuciones
Si deseas contribuir con mejoras, sigue estos pasos:
1. Haz un fork del repositorio.
2. Crea una nueva rama con una descripción clara de la mejora.
3. Realiza los cambios y confirma los commits.
4. Envía un Pull Request.

## Contacto
Si tienes dudas o sugerencias, contáctanos a través del equipo de desarrollo.

📌 **Mantente actualizado con las últimas versiones de la aplicación y pruebas.**

