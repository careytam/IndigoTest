using Microsoft.VisualStudio.TestTools.UnitTesting;
using KeyboardTransformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyboardTest
{
    [TestClass()]
    public class DynamicKeyboardTransformationTests : TransformationTest
    {
        public DynamicKeyboardTransformationTests()
        {
            base.transformation = new DynamicTransformation(4, 10);
        }

        [TestMethod]
        public override void TestToString()
        {
            base.TestToString();
        }

        [TestMethod]
        public override void VerticalSwap()
        {
            base.VerticalSwap();
        }

        [TestMethod]
        public override void HorizontalSwap()
        {
            base.HorizontalSwap();
        }

        [TestMethod]
        public override void PositiveShiftUnder40()
        {
            base.PositiveShiftUnder40();
        }

        [TestMethod]
        public override void NegativeShiftUnder40()
        {
            base.NegativeShiftUnder40();
        }

        [TestMethod]
        public override void PositiveShiftEqual40()
        {
            base.PositiveShiftEqual40();
        }

        [TestMethod]
        public override void NegativeShiftEqual40()
        {
            base.NegativeShiftEqual40();
        }

        [TestMethod]
        public override void ShiftEqual0()
        {
            base.ShiftEqual0();
        }

        [TestMethod]
        public override void PositiveShiftEqual45()
        {
            base.PositiveShiftEqual45();
        }

        [TestMethod]
        public override void NegativeShiftEqual45()
        {
            base.NegativeShiftEqual45();
        }

        [TestMethod]
        public override void ShiftEqualNegative40()
        {
            base.ShiftEqualNegative40();
        }

    }
}