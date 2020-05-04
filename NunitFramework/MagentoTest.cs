using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace NunitFramework
{
    public class MagentoTest
    {
        IWebDriver driver;

        [SetUp]
        public void Initialization()
        {

            ChromeOptions opt = new ChromeOptions();
            opt.AddArgument("--Headless");//headless browser
            driver = new ChromeDriver(opt);

            //if you want to check out the browser activity when you run the functions....
            //..... comment out the above 3 lines of code and uncomment the line of code below

            //driver = new ChromeDriver();

            //driver = new InternetExplorerDriver();
            driver.Manage().Window.Maximize(); //to maximize the window
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30); // wait for 30 seconds
            driver.Url = "https://magento.com/";
        }

        [TearDown]
        public void Terminate()
        {
            //string filename = "Screenshot" + DateTime.Now.ToString().Replace(":", "-").Replace("/", "-");
            //ITakesScreenshot sc = (ITakesScreenshot)driver; //typecasting is being done here because driver is of IWebDriver datatype and ITakesScreenshots is of different parent class
            ////this typecasting takes place during runtime
            //Screenshot screenshot = sc.GetScreenshot();
            //screenshot.SaveAsFile(filename+".png");

            string fileName = "Screenshot_" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "-");
            ITakesScreenshot sc = (ITakesScreenshot)driver; //type cast check will happen during runtime
            Screenshot screenshot = sc.GetScreenshot();
            screenshot.SaveAsFile("D:\\" + fileName + ".png");

            driver.Quit();
        }

        //this function is for entering the valid login details and checking if those login.....
        //.....details were valid or not

        [Test, Order(1)]//Timeout(5000)
        public void ValidCredentialTest() 
        {
            // Console.WriteLine("Hello world");
            //ChromeDriver driver = new ChromeDriver();
            // runtime polymorphism - method to be called is decided during runtime, based on the object you create
            //IWebDriver driver = new FirefoxDriver(); 

           

            //finding the element "My Account" and clicking on it
            IWebElement myAccEle = driver.FindElement(By.LinkText("My Account")); //FindElement takes 0.5 seconds to check for the given locator
            myAccEle.Click();

            //finding element "email" and entering the emailID
            IWebElement EmailEle = driver.FindElement(By.Id("email"));
            EmailEle.SendKeys("balaji0017@gmail.com");

            //finding element "password" and entering the password
            IWebElement PasswordEle = driver.FindElement(By.Id("pass"));
            PasswordEle.SendKeys("welcome@123");

            //finding the element "Login" and clicking on it
            IWebElement LoginEle = driver.FindElement(By.Id("send2"));
            LoginEle.Click();

            //Thread.Sleep(5000); // waits for 5 seconds but not recommended

            //confirming login
            //string expectedTitle = "My Account";
            //string actualTitle = driver.Title;
            //Assert.AreEqual(expectedTitle, actualTitle);
            Assert.AreEqual("My Account", driver.Title);

        }

        //this function is used to create an account with the magento website
        [Test, Order(2)]//MaxTime(5000)
        public void RegisterTest()
        { 

            IWebElement myAccEle = driver.FindElement(By.LinkText("My Account")); //FindElement takes 0.5 seconds to check for the given locator
            myAccEle.Click();

            ////Registering for new user using id
            ////IWebElement regEle = driver.FindElement(By.Id("register"));
            ////regEle.Click();
            //driver.FindElement(By.Id("register")).Click();
            //clicking on register using xpath
            driver.FindElement(By.XPath("//button[@id='register']")).Click();
            //driver.FindElement(By.XPath("//span[text()='Register']")).Click();


            ////entering first name
            ////IWebElement firstNameEle = driver.FindElement(By.Id("firstname"));
            ////firstNameEle.SendKeys("Shahzad");
            driver.FindElement(By.Id("firstname")).SendKeys("Shahzad");

            ////entering last name
            ////IWebElement lastNameEle = driver.FindElement(By.Id("lastname"));
            ////lastNameEle.SendKeys("Patel");
            driver.FindElement(By.Id("lastname")).SendKeys("Patel");

            ////entering email address
            ////IWebElement EmailEle = driver.FindElement(By.Id("email_address"));
            ////EmailEle.SendKeys("shahzadrp@yahoo.in");
            driver.FindElement(By.Id("email_address")).SendKeys("shahzadrp@yahoo.in");

            ////finding company type from drop down menu
            ////IWebElement cmpTypeEle = driver.FindElement(By.Id("comapny_type"));
            SelectElement cmpType = new SelectElement(driver.FindElement(By.Id("company_type")));
            cmpType.SelectByText("Tech Partner");

            ////finding "my role" from drop down menu
            SelectElement roleType = new SelectElement(driver.FindElement(By.Id("individual_role")));
            roleType.SelectByText("Technical/developer");

            ////finding country from drop down menu
            SelectElement countryEle = new SelectElement(driver.FindElement(By.Id("country")));
            countryEle.SelectByText("United States");

            ////entering password
            driver.FindElement(By.Id("password")).SendKeys("shahzad");

            ////confirming password
            driver.FindElement(By.Id("password-confirmation")).SendKeys("shahzad");

            ////checking the "agree to terms" checkbox
            driver.FindElement(By.Id("agree_terms")).Click();

        }


        //this function also enters the login details but we test them by hard-coding the.....
        //....login details into the Test Case attribute itself
        [TestCase("balaji0017@gmail.com", "welcome@123")]
        //[TestCase("bala@gmail.com","welcome")]
        public void ValidCredentialTest(string username, string password)
        {
            // Console.WriteLine("Hello world");
            //ChromeDriver driver = new ChromeDriver();
            // runtime polymorphism - method to be called is decided during runtime, based on the object you create
            //IWebDriver driver = new FirefoxDriver(); 

            //finding the element "My Account" and clicking on it
            IWebElement myAccEle = driver.FindElement(By.LinkText("My Account")); //FindElement takes 0.5 seconds to check for the given locator
            myAccEle.Click();

            //finding element "email" and entering the emailID
            IWebElement EmailEle = driver.FindElement(By.Id("email"));
            EmailEle.SendKeys(username);

            //finding element "password" and entering the password
            IWebElement PasswordEle = driver.FindElement(By.Id("pass"));
            PasswordEle.SendKeys(password);

            //finding the element "Login" and clicking on it
            IWebElement LoginEle = driver.FindElement(By.Id("send2"));
            LoginEle.Click();

            //Thread.Sleep(5000); // waits for 5 seconds but not recommended

            //confirming login
            //string expectedTitle = "My Account";
            //string actualTitle = driver.Title;
            //Assert.AreEqual(expectedTitle, actualTitle);
            Assert.AreEqual("My Account", driver.Title);

        }

    }
}
