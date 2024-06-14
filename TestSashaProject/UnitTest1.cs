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
            // ������������� WebDriver ����� ������ ������
            driver = new EdgeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            // ������������ WebDriver ����� ������� �����
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
            string bookTitle = "���";
            IWebElement addBox = wait.Until(d => d.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/article[1]/form[1]/div[1]/input[1]")));
            addBox.Click();
            addBox.SendKeys("�");
            addBox = wait.Until(d => d.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/article[1]/form[1]/div[2]/input[1]")));
            addBox.Click();
            addBox.SendKeys(bookTitle);
            addBox = wait.Until(d => d.FindElement(By.XPath("/html[1]/body[1]/div[1]/main[1]/article[1]/form[1]/div[3]/input[1]")));
            addBox.Click();
            addBox.SendKeys("12");
            string imagePath = @"C:\Users\User\Pictures\����2.jpg";
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
            //IWebElement bookRow = wait.Until(d => d.FindElement(By.XPath("//a[contains(text(),'������� ������')]/ancestor::tr")));

            try
            {
                IWebElement buyButton = wait.Until(d=>d.FindElement(By.XPath("//button[contains(text(),'������')]")));

                buyButton.Click();

                // ������: �������� ��������� ��������� ��������
                wait.Until(d => d.Title != "�������� ��������...");

            }
            catch (StaleElementReferenceException ex)
            {
                Console.WriteLine($"������ StaleElementReferenceException: {ex.Message}");
            }
        }

        [Test]
        public void TestPurchasedBookPresence()
        {
            driver.Navigate().GoToUrl("https://localhost:7055/");
            Thread.Sleep(2000);
            IWebElement searchBox = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='�����']")));
            string purchasedBookTitle = "���";
            searchBox.SendKeys(purchasedBookTitle);

            IWebElement searchButton = driver.FindElement(By.XPath("//button[contains(text(),'�����')]"));
            searchButton.Click();
            Thread.Sleep(5000);

            try
            {
                // ����� ������ ����� � ���������� ������
                IWebElement firstBookElement = driver.FindElement(By.XPath("//body/div[1]/main[1]/article[1]/div[1]/input[1]"));

                // �������� �������� ������ �����
                string firstBookTitle = firstBookElement.Text;

                // �������� �������� ��������� ����� � ������ ��������� �����
                if (firstBookTitle == purchasedBookTitle)
                {
                    Assert.Fail("��������� ����� ������������ �� �����.");
                }
            }
            catch (NoSuchElementException)
            {
                Assert.Pass("��������� ����� �� ������� �� �����.");
            }
        }
        [Test]
        public void TestSearchBook()
        {
            driver.Navigate().GoToUrl("https://localhost:7055/");
            Thread.Sleep(2000);
            IWebElement searchBox = wait.Until(d => d.FindElement(By.XPath("//input[@placeholder='�����']")));
            string purchasedBookTitle = "���";
            searchBox.SendKeys(purchasedBookTitle);

            IWebElement searchButton = driver.FindElement(By.XPath("//button[contains(text(),'�����')]"));
            searchButton.Click();
            Thread.Sleep(5000);

            try
            {
                // ����� ������ ����� � ���������� ������
                IWebElement firstBookElement = driver.FindElement(By.XPath("//body/div[1]/main[1]/article[1]/div[1]/input[1]"));

                // �������� �������� ������ �����
                string firstBookTitle = firstBookElement.Text;

                // �������� �������� ��������� ����� � ������ ��������� �����
                if (firstBookTitle == purchasedBookTitle)
                {
                    Assert.Pass("��������� ����� ������������ �� �����.");
                }
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("��������� ����� �� ������� �� �����.");
            }
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Quit();
        }
    }
}
