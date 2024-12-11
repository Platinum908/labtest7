using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace LentaTests
{
    public class Tests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestPageTitle()
        {
            _driver.Navigate().GoToUrl("https://lenta.ru/");
            string expectedTitle = "Лента.Ру: Новости России и мира на сегодня";
            string actualTitle = _driver.Title;
            Assert.That(actualTitle, Is.EqualTo(expectedTitle), "Заголовок страницы не соответствует ожидаемому");
        }

        [Test]
        public void TestElementVisibility()
        {
            _driver.Navigate().GoToUrl("https://lenta.ru/");
            IWebElement element = _wait.Until(driver => driver.FindElement(By.CssSelector("a.b-header-logo__link")));
            Assert.That(element.Displayed, Is.True, "Логотип сайта не отображается");
        }

        [Test]
        public void TestNavigation()
        {
            _driver.Navigate().GoToUrl("https://lenta.ru/");
            IWebElement link = _wait.Until(driver => driver.FindElement(By.CssSelector("a[href*='news']")));
            link.Click();
            Assert.That(_driver.Url, Does.Contain("news"), "Переход по ссылке не выполнен.");
        }

        [Test]
        public void TestFillTextField()
        {
            _driver.Navigate().GoToUrl("https://lenta.ru/");
            IWebElement searchField = _wait.Until(driver => driver.FindElement(By.CssSelector("input.gsc-input")));
            searchField.SendKeys("Тестовый поиск");
            Assert.That(searchField.GetAttribute("value"), Is.EqualTo("Тестовый поиск"), "Значение текстового поля не совпадает");
        }

        [Test]
        public void TestButtonClick()
        {
            _driver.Navigate().GoToUrl("https://lenta.ru/");
            IWebElement searchButton = _wait.Until(driver => driver.FindElement(By.CssSelector("button.gsc-search-button")));
            searchButton.Click();
            Assert.That(_driver.Url, Does.Contain("search"), "Кнопка поиска не сработала.");
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}


