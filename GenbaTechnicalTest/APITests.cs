using NUnit.Framework;

namespace GenbaTechnicalTest
{
    class APITests
    {
        APIData api = new APIData();
        HomePage homePage = new HomePage();

        [Test, Order(1)]

        public void GetToken()
        {
            api.GetToken();
        }

        [Test, Order(2)]

        public void CreateLeague()
        {
            api.CreateLeague("Some league name", 4);
        }

        [Test, Order(3)]

        public void CheckLeagueCreated()
        {
            api.CheckLeagueCreated("Some league name", 4);
        }

    }
}
