using Microsoft.VisualStudio.TestTools.UnitTesting;
using KeyboardTransformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace KeyboardTest
{
    [TestClass()]
    public class DynamicKeyboardFromFileTransformationTests : TransformationTest
    {
        public DynamicKeyboardFromFileTransformationTests()
        {
            string message = string.Empty;
            string currentAssemblyPath = this.GetFilePath("KeyboardText.txt");
            DynamicTransformation dynamicTransformation;

            base.transformation = new DynamicTransformation(currentAssemblyPath);
            dynamicTransformation = (DynamicTransformation)base.transformation;
            dynamicTransformation.Validate(out message);
        }

        [TestMethod]
        public void ValidateEmptyFile()
        {
            string message = string.Empty;
            DynamicTransformation dynamicTransformation = (DynamicTransformation)base.transformation;
            bool isValid = true;

            dynamicTransformation.TextFilePath = this.GetFilePath("EmptyTextFile.txt");
            isValid = dynamicTransformation.Validate(out message);

            Assert.IsFalse(isValid);
            Assert.AreSame(message, "Text file is empty");
        }

        [TestMethod]
        public void ValidateEmptyFilePath()
        {
            string message = string.Empty;
            DynamicTransformation dynamicTransformation = (DynamicTransformation)base.transformation;
            bool isValid = true;

            dynamicTransformation.TextFilePath = string.Empty;
            isValid = dynamicTransformation.Validate(out message);

            Assert.IsFalse(isValid);
            Assert.AreSame(message, "Text file path is empty.  You might be testing out of memory");
        }

        [TestMethod]
        public void ValidateEverySameNumber()
        {
            string message = string.Empty;
            DynamicTransformation dynamicTransformation = (DynamicTransformation)base.transformation;
            bool isValid = true;

            dynamicTransformation.TextFilePath = this.GetFilePath("NotEveryLineSameNumberTextFile.txt");
            isValid = dynamicTransformation.Validate(out message);

            Assert.IsFalse(isValid);
            Assert.AreSame(message, "Every line in the file has to be the same length");
        }

        [TestMethod]
        public void ValidateMoreThanOneLine()
        {
            string message = string.Empty;
            DynamicTransformation dynamicTransformation = (DynamicTransformation)base.transformation;
            bool isValid = true;

            dynamicTransformation.TextFilePath = this.GetFilePath("OneLineTextFile.txt");
            isValid = dynamicTransformation.Validate(out message);

            Assert.IsFalse(isValid);
            Assert.AreSame(message, "Text file must contain more than one line");
        }

        [TestMethod]
        public void ValidateSuccess()
        {
            string message = string.Empty;
            DynamicTransformation dynamicTransformation = (DynamicTransformation)base.transformation;
            bool isValid = true;

            dynamicTransformation.TextFilePath = this.GetFilePath("KeyboardText.txt");
            isValid = dynamicTransformation.Validate(out message);

            Assert.IsTrue(isValid);
            Assert.AreSame(message, string.Empty);
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

        private string GetFilePath(string fileName)
        {
            string currentAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace(@"file:\", string.Empty);
            currentAssemblyPath = Path.Combine(currentAssemblyPath.Substring(0, currentAssemblyPath.IndexOf(@"\bin")), @"TestTextFiles");
            currentAssemblyPath = Path.Combine(currentAssemblyPath, fileName);

            return currentAssemblyPath;
        }
    }
}