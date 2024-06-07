using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;
using System.Windows.Forms;

namespace WebDriver_16_NguyenLeHoaiHieu
{
    public partial class Form1 : Form
    {
        private string registeredEmail;
        private string registeredPassword;

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "https://www.facebook.com/";
            textBox2.Text = "https://www.facebook.com/";
        }

        private void btn_CLick1_Click(object sender, EventArgs e)
        {
            // Tắt màn hình đen
            ChromeDriverService chromeService = ChromeDriverService.CreateDefaultService();
            chromeService.HideCommandPromptWindow = true;

            // Thiết lập ngôn ngữ trình duyệt là tiếng Việt
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--lang=vi");

            // Điều hướng trình duyệt
            IWebDriver driver = new ChromeDriver(chromeService, options);

            try
            {
                // Lấy URL từ textBox1
                string url = textBox1.Text;

                // Điều hướng đến URL
                driver.Navigate().GoToUrl(url);

                // Chờ cho đến khi nút "Tạo tài khoản mới" có thể được click
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement createAccountButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Tạo tài khoản mới")));

                // Nhấn vào nút "Tạo tài khoản mới" và chuyển đến form đăng ký
                createAccountButton.Click();

                // Chờ cho form đăng ký hiển thị
                wait.Until(ExpectedConditions.ElementIsVisible(By.Name("firstname")));

                // Lấy thông tin họ tên, email, pass
                Thread.Sleep(1000); // Chờ 1 giây
                driver.FindElement(By.Name("lastname")).SendKeys("Hiếu");
                Thread.Sleep(1000); // Chờ 1 giây
                driver.FindElement(By.Name("firstname")).SendKeys("Nè");
                Thread.Sleep(1000); // Chờ 1 giây
                driver.FindElement(By.Name("reg_email__")).SendKeys("0989908100");
                registeredEmail = "0989908100";
                Thread.Sleep(1000); // Chờ 1 giây
                driver.FindElement(By.Name("reg_passwd__")).SendKeys("taone123@@");
                registeredPassword = "taone123@@";

                // Lấy thông tin ngày, tháng, năm sinh
                SelectElement daySelect = new SelectElement(driver.FindElement(By.Name("birthday_day")));
                Thread.Sleep(500); // Chờ nửa giây
                daySelect.SelectByValue("15");

                SelectElement monthSelect = new SelectElement(driver.FindElement(By.Name("birthday_month")));
                Thread.Sleep(500); // Chờ nửa giây
                monthSelect.SelectByValue("1");

                SelectElement yearSelect = new SelectElement(driver.FindElement(By.Name("birthday_year")));
                Thread.Sleep(500); // Chờ nửa giây
                yearSelect.SelectByValue("2000");

                // Chọn giới tính Nam
                IWebElement genderMale = driver.FindElement(By.CssSelector("input[type='radio'][name='sex'][value='2']"));
                Thread.Sleep(500); // Chờ nửa giây
                if (!genderMale.Selected)
                {
                    genderMale.Click();
                }

                // Nhấn nút Đăng ký
                Thread.Sleep(1000); // Chờ 1 giây
                driver.FindElement(By.Name("websubmit")).Click();

                // Chờ một chút để đăng ký hoàn tất
                Thread.Sleep(5000); // Chờ 5 giây để đảm bảo quá trình đăng ký hoàn tất

                // Load lại trang Facebook chính
                driver.Navigate().GoToUrl("https://www.facebook.com/");
                Thread.Sleep(5000); // Chờ 5 giây để trang load hoàn tất
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
            finally
            {
                // Đóng trình duyệt
                driver.Quit();
            }
        }

        private void btn_Click2_Click(object sender, EventArgs e)
        {
            // Lấy URL từ textBox2
            string url = textBox2.Text;
            Login(url, registeredEmail, registeredPassword);
        }

        private void Login(string url, string email, string password)
        {
            // Tắt màn hình đen
            ChromeDriverService chromeService = ChromeDriverService.CreateDefaultService();
            chromeService.HideCommandPromptWindow = true;

            // Thiết lập ngôn ngữ trình duyệt là tiếng Việt
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--lang=vi");

            // Điều hướng trình duyệt
            IWebDriver driver = new ChromeDriver(chromeService, options);

            try
            {
                // Điều hướng đến URL
                driver.Navigate().GoToUrl(url);

                // Chờ cho đến khi các trường đăng nhập có thể được sử dụng
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("pass")));

                // Điền thông tin đăng nhập và nhấn nút đăng nhập
                driver.FindElement(By.Id("email")).SendKeys(email);
                driver.FindElement(By.Id("pass")).SendKeys(password);
                driver.FindElement(By.Name("login")).Click();

                // Chờ đợi một chút để đảm bảo quá trình đăng nhập hoàn tất
                Thread.Sleep(5000); // Chờ 5 giây để đảm bảo quá trình đăng nhập hoàn tất
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
            finally
            {
                // Đóng trình duyệt
                driver.Quit();
            }
        }

        // Định nghĩa lớp SelectElement
        public class SelectElement
        {
            private IWebElement webElement;

            public SelectElement(IWebElement webElement)
            {
                this.webElement = webElement;
            }

            public void SelectByValue(string value)
            {
                var options = webElement.FindElements(By.TagName("option"));
                foreach (var option in options)
                {
                    if (option.GetAttribute("value") == value)
                    {
                        option.Click();
                        break;
                    }
                }
            }
        }
    }
}
