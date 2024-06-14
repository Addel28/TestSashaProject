using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Edge;

namespace TestBlazorWebProject
{
    [TestFixture]
    public class BookstoreTests
    {
        private IWebDriver driver;
        [SetUp]
        public void SetUp()
        {
            // Инициализация WebDriver перед каждым тестом
            driver = new EdgeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            // Освобождение WebDriver после каждого теста
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
        private WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            var edgeOptions = new EdgeOptions();
            driver = new EdgeDriver(@"C:\msedgedriver.exe", edgeOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

        }

        [Test]
        public void AddEditBookPageNavigationTest()
        {
            driver.Navigate().GoToUrl("https://localhost:7055/");
            Thread.Sleep(2000);
            IWebElement addWindow = wait.Until(d => d.FindElement(By.XPath("/html[1]/body[1]/div[1]/div[1]/div[2]/nav[1]/div[2]/a[1]")));
            addWindow.Click();
            Thread.Sleep(2000);
            string bookTitle = "фыы";
            IWebElement addBox = wait.Until(d => d.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/article[1]/form[1]/div[1]/input[1]")));
            addBox.Click();
            addBox.SendKeys("ф");
            addBox = wait.Until(d => d.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/article[1]/form[1]/div[2]/input[1]")));
            addBox.Click();
            addBox.SendKeys(bookTitle);
            addBox = wait.Until(d => d.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/article[1]/form[1]/div[3]/input[1]")));
            addBox.Click();
            addBox.SendKeys("12");
            string imagePath = @"C:\Users\User\Pictures\макс2.jpg";
            IWebElement element = driver.FindElement(By.Id("image"));
            element.SendKeys(imagePath);

            IWebElement addButton = wait.Until(d => d.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/article[1]/form[1]/button[1]")));
            addButton.Click();
            Thread.Sleep(1000);
        }

        [Test]
        public void BuyBookTest()
        {
            driver.Navigate().GoToUrl("https://localhost:7055/");
            Thread.Sleep(2000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            //IWebElement bookRow = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'Евгений Онегин')]/ancestor::tr")));

            try
            {
                IWebElement buyButton = wait.Until(d=>d.FindElement(By.XPath("//button[contains(text(),'Купить')]")));

                buyButton.Click();

                // Пример: ожидание появления заголовка страницы
                wait.Until(d => d.Title != "Страница загрузки...");

            }
            catch (StaleElementReferenceException ex)
            {
                Console.WriteLine($"Ошибка StaleElementReferenceException: {ex.Message}");
            }
        }

        [Test]
        public void TestPurchasedBookPresence()
        {
            driver.Navigate().GoToUrl("https://localhost:7055/");
            Thread.Sleep(2000);
            IWebElement searchBox = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Поиск']")));
            string purchasedBookTitle = "фыы";
            searchBox.SendKeys(purchasedBookTitle);

            IWebElement searchButton = driver.FindElement(By.XPath("//button[contains(text(),'Поиск')]"));
            searchButton.Click();
            Thread.Sleep(5000);

            try
            {
                // Найти первую книгу в результате поиска
                IWebElement firstBookElement = driver.FindElement(By.XPath("//body/div[1]/main[1]/article[1]/div[1]/input[1]"));

                // Получить название первой книги
                string firstBookTitle = firstBookElement.Text;

                // Сравнить названия купленной книги и первой найденной книги
                if (firstBookTitle == purchasedBookTitle)
                {
                    Assert.Fail("Купленная книга присутствует на сайте.");
                }
            }
            catch (NoSuchElementException)
            {
                Assert.Pass("Купленная книга не найдена на сайте.");
            }
        }
        [Test]
        public void TestSearchBook()
        {
            driver.Navigate().GoToUrl("https://localhost:7055/");
            Thread.Sleep(2000);
            IWebElement searchBox = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='Поиск']")));
            string purchasedBookTitle = "Ура";
            searchBox.SendKeys(purchasedBookTitle);

            IWebElement searchButton = driver.FindElement(By.XPath("//button[contains(text(),'Поиск')]"));
            searchButton.Click();
            Thread.Sleep(5000);

            try
            {
                // Найти первую книгу в результате поиска
                IWebElement firstBookElement = driver.FindElement(By.XPath("//body/div[1]/main[1]/article[1]/div[1]/input[1]"));

                // Получить название первой книги
                string firstBookTitle = firstBookElement.Text;

                // Сравнить названия купленной книги и первой найденной книги
                if (firstBookTitle == purchasedBookTitle)
                {
                    Assert.Pass("Купленная книга присутствует на сайте.");
                }
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Купленная книга не найдена на сайте.");
            }
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
        }
    }
}
