using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using ToDoListLib.Helper;


namespace ToDoListTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod_GetDateTimeCurrent()
        {
            var dateTime = DateTime.Now;
            var expected = dateTime.ToString("HH_mm_ss_ffff", CultureInfo.InvariantCulture.DateTimeFormat);

            var actual = CommonHelper.GetTimeCurrentStr(dateTime);

            Assert.AreEqual(expected, actual, "Get datetime current str not availble");
        }
    }
}
