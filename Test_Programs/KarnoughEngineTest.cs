﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karnaugh_Logic;
using Karnaugh_Logic.Interfaces;
using System.IO;

namespace Test_Programs
{
    [TestClass]
    public class KarnoughEngineTest
    {
        [TestMethod]
        public void jsontest()
        {
            //KarnoughEngine en = new KarnoughEngine();
            string jsonpath = @"TestJson.json";
            string json;

            using (FileStream fs = File.OpenRead(jsonpath))
            {
                using(StreamReader sr = new StreamReader(fs)){
                    json = sr.ReadToEnd();
                }
            }

            IKarnoughMap map = KarnoughEngine.deserializer(json);

            Assert.AreEqual("value1", map.valueNames[0]);
            Assert.AreEqual("value2", map.valueNames[1]);
            Assert.AreEqual("value3", map.valueNames[2]);

            Assert.AreEqual(TruthValue.True, map.getMapPoint(0, 0).values);
            Assert.AreEqual(TruthValue.True, map.getMapPoint(1, 0).values);
            Assert.AreEqual(TruthValue.True, map.getMapPoint(0, 1).values);
            Assert.AreEqual(TruthValue.True, map.getMapPoint(1, 1).values);

            Assert.AreEqual((byte)1, map.getMapPoint(0, 0).blockValue);
            Assert.AreEqual((byte)1, map.getMapPoint(1, 0).blockValue);
            Assert.AreEqual((byte)1, map.getMapPoint(0, 1).blockValue);
            Assert.AreEqual((byte)1, map.getMapPoint(1, 1).blockValue);
        }
    }
}
