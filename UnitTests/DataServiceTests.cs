using System;
using System.Linq;
using rawdata_portfolioproject_2;
using Xunit;

namespace UnitTests
{
    public class DataServiceTests
    {
        // add functions to test DataService like so:
        [Fact]
        public void DummyTest_ProfileClassExists()
        {
            Assert.NotNull(new Profile());
        }
        [Fact]
        public void GetPostWithValidId()
        {
            var service = new DataService();
            var post = service.GetPost(19);
            Assert.Equal(164, post.Score);
        }

        [Fact]
        public void LoginWithInvalidIdReturnFalse()
        {
            var service = new DataService();
            var login = service.Login("bademail", "badpassword");
            Assert.False(login);
        }

        [Fact]
        public void CreateNewProfileAndLoginWithValidIdReturnTrue()
        {
            var email = "test@email.com";
            var password = "testpassword";
            var service = new DataService();
            service.CreateProfile(email, password);
            var login = service.Login(email, password);
            Assert.True(login);

            service.DeleteProfile(email, password);
        }
    }
}