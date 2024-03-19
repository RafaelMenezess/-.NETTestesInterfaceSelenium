using Alura.ByteBank.WebApp.Testes.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Alura.ByteBank.WebApp.Testes
{
    public class AposRealizarLogin
    {
        public ITestOutputHelper SaidaConsoleTeste;

        public AposRealizarLogin(ITestOutputHelper _saidaConsoleTeste)
        {
            SaidaConsoleTeste = _saidaConsoleTeste;
        }

        [Fact]
        public void AposRealizarLoginVerificaOpcaoAgenciaMenu()
        {

            //Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            //Act
            loginPO.PreencherCampos("rafael@email.com", "senha01");
            loginPO.btnClick();

            //Assert
            Assert.Contains("Agência", driver.PageSource);

            driver.Close();
        }

        [Fact]
        public void TentaLogarSemPreencherDados()
        {

            //Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            //Arrange
            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            //Act
            loginPO.PreencherCampos("", "");
            loginPO.btnClick();

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

            //Arrange
            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            //Act
            loginPO.PreencherCampos("rafael@email.com", "senha02");
            loginPO.btnClick();

            //Assert
            Assert.Contains("Login", driver.PageSource);

            driver.Close();
        }

        [Fact]
        public void RealizarLoginAcessaMenuECadastraCliente()
        {
            //Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");


            loginPO.PreencherCampos("rafael@email.com", "senha01");
            loginPO.btnClick();

            driver.FindElement(By.LinkText("Cliente")).Click();

            driver.FindElement(By.LinkText("Adicionar Cliente")).Click();

            driver.FindElement(By.Name("Identificador")).Click();
            driver.FindElement(By.Name("Identificador")).SendKeys("2df71922-ca7d-4d43-b142-0767b32f822a");
            driver.FindElement(By.Name("CPF")).Click();
            driver.FindElement(By.Name("CPF")).SendKeys("69981034096");
            driver.FindElement(By.Name("Nome")).Click();
            driver.FindElement(By.Name("Nome")).SendKeys("Tobey Garfield");
            driver.FindElement(By.Name("Profissao")).Click();
            driver.FindElement(By.Name("Profissao")).SendKeys("Cientista");

            //Act
            driver.FindElement(By.CssSelector(".btn-primary")).Click();
            driver.FindElement(By.LinkText("Home")).Click();

            //Assert 
            Assert.Contains("Logout", driver.PageSource);

            driver.Close();
        }

        [Fact]
        public void RealizarLoginAcessaListagemDeContas()
        {
            //Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            //Act
            loginPO.PreencherCampos("rafael@email.com", "senha01");
            loginPO.btnClick();

            //Conta Corrente
            driver.FindElement(By.Id("contacorrente")).Click();

            IReadOnlyCollection<IWebElement> elements =
                driver.FindElements(By.TagName("a"));

            //***IMPRIMINDO TODOS ELEMENTOS NO CONSOLE***
            //foreach (var element in elements)
            //{
            //    SaidaConsoleTeste.WriteLine(element.Text);
            //}

            var elemento = (from webElemento in elements
                            where webElemento.Text.Contains("Detalhes")
                            select webElemento).First();

            //Act
            elemento.Click();

            //Assert 
            Assert.Contains("Voltar", driver.PageSource);

            driver.Close();
        }

        [Fact]
        public void RealizarLoginAcessaListagemDeContasHomePO()
        {
            //Arrange
            IWebDriver driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");

            //Act
            loginPO.PreencherCampos("rafael@email.com", "senha01");
            loginPO.btnClick();

            var homePo = new HomePO(driver);
            homePo.LinkContaCorrenteClick();

            //Assert
            Assert.Contains("Adicionar Conta-Corrente", driver.PageSource);

            //driver.Close();
        }
    }
}
