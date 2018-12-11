using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karnaugh_Logic;

namespace Test_Programs
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            KarnoughComponent com = new KarnoughComponent(0,TruthValue.True);
            KarnoughMap map = new KarnoughMap();
            map.dimension = 2;
            map.setMapPoint(com, 0, 0);
            map.setMapPoint(com, 1, 0);
            map.setMapPoint(com, 0, 1);
            map.setMapPoint(com, 1, 0);

            Assert.Equals(com, map.getMapPoint(0, 0));
            Assert.Equals(com, map.getMapPoint(1, 0));
            Assert.Equals(com, map.getMapPoint(0, 1));
            Assert.Equals(com, map.getMapPoint(1, 1));
        }
    }
}
