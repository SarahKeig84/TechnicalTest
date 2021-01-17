using NUnit.Framework;

namespace GenbaTechnicalTest
{
    class APITests
    {
        APIData api = new APIData();

        [Test, Order(1)]

        public void GetToken()
        {
            api.GetToken();
        }

        [Test, Order(2)]

        public void CreateLeague()
        {
            api.CreateLeague();
        }

    }
}
