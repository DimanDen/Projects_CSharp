using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProgramForCreatingListingTest
{
    [TestClass]
    public class ActionLogicUnitTests
    {
        [TestMethod]
        public void GetFileNameFromPathTestMethod()
        {

            string path = @"C:\Users\Дмитрий33\Dropbox\HTML+CSS+JAVASCRIPT\pom.css";
            string expected = "pom.css";

            string actual = ProgramForCreatingListing.ActionLogic.GetFileNameFromPath(path);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void FormDataStringTestMethod()
        {
            string[] paths = new string[2];
            paths[0] = @"C:\Users\Дмитрий33\Dropbox\HTML+CSS+JAVASCRIPT\1.txt";
            paths[1] = @"C:\Users\Дмитрий33\Dropbox\HTML+CSS+JAVASCRIPT\2.txt";
            string expectedResult = "sss" + "\r\n" + "aaa" + "\r\n";
            string actualResult = ProgramForCreatingListing.ActionLogic.FormDataString(paths);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
