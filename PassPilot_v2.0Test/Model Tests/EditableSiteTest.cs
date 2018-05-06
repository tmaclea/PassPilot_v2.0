using Microsoft.VisualStudio.TestTools.UnitTesting;
using PassPilot_v2._0.Models;

namespace PassPilot_v2._0Test
{
    [TestClass]
    public class EditableSiteTest
    {
        [TestMethod]
        public void SiteNameReturnsCorrectString()
        {
            //Arrange
            var testSite = new EditableSite
            {
                //Act
                SiteName = "Test Site"
            };
            //Assert
            Assert.AreEqual(testSite.SiteName, "Test Site");
        }

        [TestMethod]
        public void SiteURLReturnsCorrectString()
        {
            //Arrange
            var testSite = new EditableSite
            {
                //Act
                SiteURL = "http://testsite.com"
            };
            //Assert
            Assert.AreEqual(testSite.SiteURL, "http://testsite.com");
        }

        [TestMethod]
        public void UsernameReturnsCorrectString()
        {
            //Arrange
            var testSite = new EditableSite
            {
                //Act
                Username = "Username1"
            };
            //Assert
            Assert.AreEqual(testSite.Username, "Username1");
        }

        [TestMethod]
        public void PasswordReturnsCorrectString()
        {
            //Arrange
            var testSite = new EditableSite
            {
                //Act
                Password = "password"
            };
            //Assert
            Assert.AreEqual(testSite.Password, "password");
        }

        [TestMethod]
        public void EmptySiteNameValidatesRequired()
        {
            Assert.Inconclusive();
            //Arrange
            //Act
            //Assert
        }

        [TestMethod]
        public void SiteURLValidatesURL()
        {
            Assert.Inconclusive();
            //Arrange
            //Act
            //Assert
        }
    }
}
