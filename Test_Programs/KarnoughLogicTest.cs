using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karnaugh_Logic;
using Karnaugh_Logic.Interfaces;
using System.Collections.Generic;

namespace Test_Programs
{
    [TestClass]
    public class KarnoughLogicTest
    {
        /// <summary>
        /// KarnoguhLogicのテスト
        /// </summary>
        [TestMethod]
        public void Test()
        {
            KarnoughLogic log = new KarnoughLogic();
            log.valueNames.Add("value1");
            log.valueNames.Add("value2");
            log.valueNames.Add("value3");

            List<TruthValue> list1 = new List<TruthValue>();
            list1.Add(TruthValue.False);
            list1.Add(TruthValue.Null);
            list1.Add(TruthValue.True);
            List<TruthValue> list2 = new List<TruthValue>();
            list2.Add(TruthValue.True);
            list2.Add(TruthValue.Null);
            list2.Add(TruthValue.Null);
            List<TruthValue> list3 = new List<TruthValue>();
            list3.Add(TruthValue.True);
            list3.Add(TruthValue.True);
            list3.Add(TruthValue.Null);

            log.values.Add(list1);
            log.values.Add(list2);
            log.values.Add(list3);

            string outputStr = log.genLogicExpression();
            string truStr = "not(value1)*value3+value1+value1*value2";
            Assert.AreEqual(outputStr, truStr);
        }
    }
}
