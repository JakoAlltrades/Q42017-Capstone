using Microsoft.VisualStudio.TestTools.UnitTesting;
using CapstoneModelLib;
namespace LibraryTest
{
    [TestClass]
    public class UserActionTests
    {
        UserActionsDB UAD = new UserActionsDB("mongod//192.168.1.200/27017");
        [TestMethod]
        public void SignInTest()
        {
            UAD.client;
        }

        [TestMethod]
        public void CreateAccountTest()
        {

        }
    }
}
