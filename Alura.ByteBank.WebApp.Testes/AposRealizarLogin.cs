using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Reflection;
using Xunit;

namespace Alura.ByteBank.WebApp.Testes
{
    public class AposRealizarLogin
    {
        [Fact]
        public void AposRealizarLoginVerificaOpcaoAgenciaMenu()
        {

            //Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            var login = driver.FindElement(By.Id("Email")); //Seleciona elementos HTML
            var senha = driver.FindElement(By.Id("Senha")); //Seleciona elementos HTML
            var btnLogar = driver.FindElement(By.Id("btn-logar")); //Seleciona elementos HTML

            login.SendKeys("rafael@email.com");
            senha.SendKeys("senha01");

            //Act
            btnLogar.Click();

            //Assert
            Assert.Contains("Agência", driver.PageSource);

            driver.Close();
        }

        [Fact]
        public void TentaLogarSemPreencherDados()
        {

            //Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            var login = driver.FindElement(By.Id("Email")); //Seleciona elementos HTML
            var senha = driver.FindElement(By.Id("Senha")); //Seleciona elementos HTML
            var btnLogar = driver.FindElement(By.Id("btn-logar")); //Seleciona elementos HTML

            //login.SendKeys("rafael@email.com");
            //senha.SendKeys("senha01");

            //Act
            btnLogar.Click();

            //Assert
            Assert.Contains("The Email field is required.", driver.PageSource);
            Assert.Contains("The Senha field is required.", driver.PageSource);

            driver.Close();
        }

        [Fact]
        public void SenhaInvalida()
        {

            //Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            var login = driver.FindElement(By.Id("Email")); //Seleciona elementos HTML
            var senha = driver.FindElement(By.Id("Senha")); //Seleciona elementos HTML
            var btnLogar = driver.FindElement(By.Id("btn-logar")); //Seleciona elementos HTML

            login.SendKeys("rafael@email.com");
            senha.SendKeys("senha02");

            //Act
            btnLogar.Click();

            //Assert
            Assert.Contains("Login", driver.PageSource);

            driver.Close();
        }
    }
}
