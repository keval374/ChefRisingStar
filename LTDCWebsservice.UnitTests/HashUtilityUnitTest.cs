using LTDCWebservice.Utilities;

namespace LTDCWebsservice.UnitTests
{
    [TestClass]
    public class HashUtilityUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string password = "password123456";
            byte[] salt;
            byte[] tmpSalt;

            string hashedPassword = HashUtility.HashPasword(password, out salt);
            string saltStr1 = Convert.ToBase64String(salt);
            string saltStr2 = Convert.ToHexString(salt);

            tmpSalt = Convert.FromBase64String(saltStr1);
            string saltStr3 = Convert.ToHexString(tmpSalt);

            Assert.AreNotEqual(saltStr1, saltStr2);
            Assert.AreEqual(saltStr2,saltStr3);
        }
        
        [TestMethod]
        public void SuccessHashCheck()
        {
            
        }
    }
}