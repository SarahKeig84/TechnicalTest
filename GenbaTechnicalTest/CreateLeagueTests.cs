using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace GenbaTechnicalTest
{
    
    class CreateLeagueTests
    {
        LoginPage loginPage = new LoginPage();
        Users users = new Users();
        HomePage homePage = new HomePage();

        [OneTimeSetUp]
         public void Initialize()
        {
            Properties.driver = new ChromeDriver();
            //Properties.driver = new EdgeDriver();
            Properties.driver.Manage().Window.Maximize();
            Properties.driver.Navigate().GoToUrl("https://www.galacticoeleven.com/#!/login");

        }

        [Test, Order(1)]
        public void Login()
        {
            //verifies title of login page
            //logs in
            //verifies homepage title

            loginPage.VerifyPage();
            loginPage.Login(users.email, users.password, loginPage.LoginButton);
            homePage.VerifyPage();
        }

        [Test, Order(2)]
        public void CreateLeague()
        {
            //creates a league
            homePage.CreateLeague(homePage.CreateLeagueButton, homePage.LeagueName, homePage.TeamName);
        }

        [Test, Order(3)]
        public void VerifyLeague()
        {
            homePage.VerifyLeagueCreated(homePage.LeagueName, homePage.TeamName, homePage.CompetitionName);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Properties.driver.Close();
        }
    }
}
