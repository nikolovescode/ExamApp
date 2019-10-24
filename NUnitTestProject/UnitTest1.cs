using NUnit.Framework;

namespace Tests
{
    using ExamApp;
    using ExamApp.Services;
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [TestCase]
        public async System.Threading.Tasks.Task Test_Login_User_TrueAsync()
        {
            ApiServices apiServices = new ApiServices();
            bool response = await apiServices.LoginUser("junior@email.com", "B12&Ccp");

            Assert.That(response, Is.Not.Null);
        }

    }
}