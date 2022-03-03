using KeyboardTransformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KeyboardTest
{
    [TestClass]
    public class TransformationTest
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        protected Transformation transformation { get; set; }

        public TransformationTest()
        {
            transformation = new Transformation();
        }

        [TestMethod]
        public virtual void TestToString()
        {
            string expectedResult = "1234567890" + Environment.NewLine +
                                    "qwertyuiop" + Environment.NewLine +
                                    "asdfghjkl;" + Environment.NewLine +
                                    "zxcvbnm,./" + Environment.NewLine;
            string actualResult = transformation.ToString();

            TestContext.WriteLine(transformation.ToString());

            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public virtual void VerticalSwap()
        {
            transformation.VerticalSwap();
            string expectedResult = "zxcvbnm,./" + Environment.NewLine +
                                    "asdfghjkl;" + Environment.NewLine +
                                    "qwertyuiop" + Environment.NewLine +
                                    "1234567890" + Environment.NewLine;
            string actualResult = transformation.ToString();

            TestContext.WriteLine(transformation.ToString());

            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public virtual void HorizontalSwap()
        {
            transformation.HorizontalSwap();
            string expectedResult = "0987654321" + Environment.NewLine +
                                    "poiuytrewq" + Environment.NewLine +
                                    ";lkjhgfdsa" + Environment.NewLine +
                                    "/.,mnbvcxz" + Environment.NewLine;
            string actualResult = transformation.ToString();

            TestContext.WriteLine(transformation.ToString());

            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public virtual void PositiveShiftUnder40()
        {
            transformation.Shift(5);
            string expectedResult = "nm,./12345" + Environment.NewLine +
                                    "67890qwert" + Environment.NewLine +
                                    "yuiopasdfg" + Environment.NewLine +
                                    "hjkl;zxcvb" + Environment.NewLine;
            string actualResult = transformation.ToString();

            TestContext.WriteLine(transformation.ToString());

            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public virtual void NegativeShiftUnder40()
        {
            transformation.Shift(-5);
            string expectedResult = "67890qwert" + Environment.NewLine +
                                    "yuiopasdfg" + Environment.NewLine +
                                    "hjkl;zxcvb" + Environment.NewLine +
                                    "nm,./12345" + Environment.NewLine;
            string actualResult = transformation.ToString();

            TestContext.WriteLine(transformation.ToString());

            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public virtual void PositiveShiftEqual40()
        {
            transformation.Shift(40);
            string expectedResult = "1234567890" + Environment.NewLine +
                                     "qwertyuiop" + Environment.NewLine +
                                     "asdfghjkl;" + Environment.NewLine +
                                     "zxcvbnm,./" + Environment.NewLine;
            string actualResult = transformation.ToString();

            TestContext.WriteLine(transformation.ToString());

            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public virtual void NegativeShiftEqual40()
        {
            transformation.Shift(-40);
            string expectedResult = "1234567890" + Environment.NewLine +
                                    "qwertyuiop" + Environment.NewLine +
                                    "asdfghjkl;" + Environment.NewLine +
                                    "zxcvbnm,./" + Environment.NewLine;
            string actualResult = transformation.ToString();

            TestContext.WriteLine(transformation.ToString());

            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public virtual void ShiftEqual0()
        {
            transformation.Shift(0);
            string expectedResult = "1234567890" + Environment.NewLine +
                                     "qwertyuiop" + Environment.NewLine +
                                     "asdfghjkl;" + Environment.NewLine +
                                     "zxcvbnm,./" + Environment.NewLine;
            string actualResult = transformation.ToString();

            TestContext.WriteLine(transformation.ToString());

            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public virtual void PositiveShiftEqual45()
        {
            transformation.Shift(45);
            string expectedResult = "nm,./12345" + Environment.NewLine +
                                    "67890qwert" + Environment.NewLine +
                                    "yuiopasdfg" + Environment.NewLine +
                                    "hjkl;zxcvb" + Environment.NewLine;
            string actualResult = transformation.ToString();

            TestContext.WriteLine(transformation.ToString());

            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public virtual void NegativeShiftEqual45()
        {
            transformation.Shift(-45);
            string expectedResult = "67890qwert" + Environment.NewLine +
                                    "yuiopasdfg" + Environment.NewLine +
                                    "hjkl;zxcvb" + Environment.NewLine +
                                    "nm,./12345" + Environment.NewLine;
            string actualResult = transformation.ToString();

            TestContext.WriteLine(transformation.ToString());

            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public virtual void ShiftEqualNegative40()
        {
            transformation.Shift(-40);
            string expectedResult = "1234567890" + Environment.NewLine +
                                     "qwertyuiop" + Environment.NewLine +
                                     "asdfghjkl;" + Environment.NewLine +
                                     "zxcvbnm,./" + Environment.NewLine;
            string actualResult = transformation.ToString();

            TestContext.WriteLine(transformation.ToString());

            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
