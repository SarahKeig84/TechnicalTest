using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace GenbaTechnicalTest
{

    public class HomePage
    {
        public IWebElement CreateLeagueButton => Properties.driver.FindElement(By.XPath("//li[3]/a[.='Create']"));
        public IWebElement Competition => Properties.driver.FindElement(By.CssSelector("[ng-model='competition']"));
        public IWebElement CreateButton => Properties.driver.FindElement(By.CssSelector(".btn-success"));
        public string LeagueName => "Test League Name";
        public string TeamName => "Test Team Name";
        public string CompetitionName => "EPL 2019/20 - Retro";

        public void VerifyPage()
        {
            WebDriverWait wait = new WebDriverWait(Properties.driver, TimeSpan.FromSeconds(10));
            var leagueTable = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(".page-header")));
            Assert.AreEqual(leagueTable.GetAttribute("innerText"), "Leagues");
            string homePageUrl = Properties.driver.Url;
            Assert.AreEqual(expectedHomePageUrl, homePageUrl);
            Console.WriteLine("Home Page Url is: " + homePageUrl);
        }

        public void CreateLeague(IWebElement selectCreate, string leagueName, string teamName)
        {
            selectCreate.Click();
            WebDriverWait wait = new WebDriverWait(Properties.driver, TimeSpan.FromSeconds(10));
            var nameBox = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("name")));
            var teamBox = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("team")));
            string createPageUrl = Properties.driver.Url;
            Assert.AreEqual(expectedCreatePageUrl, createPageUrl);
            nameBox.SendKeys(leagueName);
            teamBox.SendKeys(teamName);
            SelectCompetition(Competition, CompetitionName);
            ClickCreate(CreateButton);
        }
        //I couldn't seem to get this method to work inside the CreateLeague method so I broke it out
        public void SelectCompetition(IWebElement competition, string competitionName)
        {
            new SelectElement(competition).SelectByText(competitionName);
        }

        public void ClickCreate(IWebElement createButton)
        {
            createButton.Click();
        }

        public void VerifyLeagueCreated(string leagueName, string teamName, string competitionName)
        {
            WebDriverWait wait = new WebDriverWait(Properties.driver, TimeSpan.FromSeconds(10));
            var createdLeagueDetails = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//ui-view[@class='ng-scope']/div[1]/h5[@class='ng-binding']")));
            string currentLeaguesUrl = Properties.driver.Url;
            Assert.AreEqual(expectedHomePageUrl, currentLeaguesUrl);
            var leagueText = createdLeagueDetails.GetAttribute("innerText");
            Assert.IsTrue(leagueText.Contains(leagueName));
            Assert.IsTrue(leagueText.Contains("Team: " + teamName));
            Assert.IsTrue(leagueText.Contains("Competition: " + competitionName));

        }

        //Expected strings
        public static string expectedHomePageUrl = "https://www.galacticoeleven.com/#!/leagues/current";
        public static string expectedCreatePageUrl = "https://www.galacticoeleven.com/#!/leagues/create";
    }
}


