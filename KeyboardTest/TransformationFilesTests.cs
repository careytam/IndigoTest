using KeyboardTransformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KeyboardTest
{
    [TestClass()]
    public class TransformationFilesTests
    {
        

        [TestMethod()]
        public void ValidateFailOnTextFileTest()
        {
            string textFilePath = this.GetFilePath("EmptyTextFile.txt");
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            IEnumerable<string> errorMessages;
            bool isValid = true;
            TransformationFiles transformationFiles = new TransformationFiles(textFilePath, commandFilePath);

            isValid = transformationFiles.Validate(out errorMessages);

            Assert.IsFalse(isValid);
            Assert.IsTrue(errorMessages.Count() == 1);
            Assert.AreSame("Text file is empty", errorMessages.FirstOrDefault());
        }

        [TestMethod()]
        public void ValidateFailOnCommandFileTest()
        {
            string textFilePath = this.GetFilePath("KeyboardText.txt");
            string commandFilePath = this.GetFilePath("TransformationCommandMoreThanOneLine.txt");
            IEnumerable<string> errorMessages;
            bool isValid = true;
            TransformationFiles transformationFiles = new TransformationFiles(textFilePath, commandFilePath);

            isValid = transformationFiles.Validate(out errorMessages);

            Assert.IsFalse(isValid);
            Assert.IsTrue(errorMessages.Count() == 1);
            Assert.AreSame("Command file must contain more than one line", errorMessages.FirstOrDefault());
        }

        [TestMethod()]
        public void ValidateFailOnBothFilesTest()
        {
            string textFilePath = this.GetFilePath("NotEveryLineSameNumberTextFile.txt");
            string commandFilePath = this.GetFilePath("TransformationCommandMoreThanOneLine.txt");
            IEnumerable<string> errorMessages;
            bool isValid = true;
            TransformationFiles transformationFiles = new TransformationFiles(textFilePath, commandFilePath);

            isValid = transformationFiles.Validate(out errorMessages);

            Assert.IsFalse(isValid);
            Assert.IsTrue(errorMessages.Count() == 2);
            Assert.IsTrue(errorMessages.Contains("Command file must contain more than one line"));
            Assert.IsTrue(errorMessages.Contains("Every line in the file has to be the same length"));
        }

        [TestMethod()]
        public void ValidateSuccessTest()
        {
            string textFilePath = this.GetFilePath("KeyboardText.txt");
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            IEnumerable<string> errorMessages;
            bool isValid = false;
            TransformationFiles transformationFiles = new TransformationFiles(textFilePath, commandFilePath);

            isValid = transformationFiles.Validate(out errorMessages);

            Assert.IsTrue(isValid);
            Assert.IsTrue(errorMessages.Count() == 0);
        }

        [TestMethod()]
        public void InitializeTest()
        {
            string textFilePath = this.GetFilePath("KeyboardText.txt");
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            IEnumerable<string> errorMessages;
            bool isValid = false;
            TransformationFiles transformationFiles = new TransformationFiles(textFilePath, commandFilePath);

            isValid = transformationFiles.Validate(out errorMessages);
            transformationFiles.Initialize();

            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void OperateTest1()
        {
            string textFilePath = this.GetFilePath("KeyboardText.txt");
            string commandFilePath = this.GetFilePath("TransformationCommand-1.txt");
            IEnumerable<string> errorMessages;
            bool isValid = false;
            TransformationFiles transformationFiles = new TransformationFiles(textFilePath, commandFilePath);
            string expectedResult = "0987654321" + Environment.NewLine +
                                    "poiuytrewq" + Environment.NewLine +
                                    ";lkjhgfdsa" + Environment.NewLine +
                                    "/.,mnbvcxz" + Environment.NewLine;
            string actualResult = string.Empty;

            isValid = transformationFiles.Validate(out errorMessages);
            transformationFiles.Initialize();
            transformationFiles.Operate();
            actualResult = transformationFiles.ToString();
            transformationFiles.SaveToDisk();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void OperateTest2()
        {
            string textFilePath = this.GetFilePath("KeyboardText.txt");
            string commandFilePath = this.GetFilePath("TransformationCommand-2.txt");
            IEnumerable<string> errorMessages;
            bool isValid = false;
            TransformationFiles transformationFiles = new TransformationFiles(textFilePath, commandFilePath);
            string expectedResult = "/.,mnbvcxz" + Environment.NewLine +
                                    ";lkjhgfdsa" + Environment.NewLine +
                                    "poiuytrewq" + Environment.NewLine +
                                    "0987654321" + Environment.NewLine;
            string actualResult = string.Empty;

            isValid = transformationFiles.Validate(out errorMessages);
            transformationFiles.Initialize();
            transformationFiles.Operate();
            actualResult = transformationFiles.ToString();
            transformationFiles.SaveToDisk();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void OperateTest3()
        {
            string textFilePath = this.GetFilePath("KeyboardText.txt");
            string commandFilePath = this.GetFilePath("TransformationCommand-3.txt");
            IEnumerable<string> errorMessages;
            bool isValid = false;
            TransformationFiles transformationFiles = new TransformationFiles(textFilePath, commandFilePath);
            string expectedResult = "54321/.,mn" + Environment.NewLine +
                                    "bvcxz;lkjh" + Environment.NewLine +
                                    "gfdsapoiuy" + Environment.NewLine +
                                    "trewq09876" + Environment.NewLine;
            string actualResult = string.Empty;

            isValid = transformationFiles.Validate(out errorMessages);
            transformationFiles.Initialize();
            transformationFiles.Operate();
            actualResult = transformationFiles.ToString();
            transformationFiles.SaveToDisk();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void OperateTest4()
        {
            string textFilePath = this.GetFilePath("KeyboardText.txt");
            string commandFilePath = this.GetFilePath("TransformationCommand-4.txt");
            IEnumerable<string> errorMessages;
            bool isValid = false;
            TransformationFiles transformationFiles = new TransformationFiles(textFilePath, commandFilePath);
            string expectedResult = "trewq09876" + Environment.NewLine +
                                    "gfdsapoiuy" + Environment.NewLine +
                                    "bvcxz;lkjh" + Environment.NewLine +
                                    "54321/.,mn" + Environment.NewLine;
            string actualResult = string.Empty;

            isValid = transformationFiles.Validate(out errorMessages);
            transformationFiles.Initialize();
            transformationFiles.Operate();
            actualResult = transformationFiles.ToString();
            transformationFiles.SaveToDisk();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void OperateTest5()
        {
            string textFilePath = this.GetFilePath("KeyboardText.txt");
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            IEnumerable<string> errorMessages;
            bool isValid = false;
            TransformationFiles transformationFiles = new TransformationFiles(textFilePath, commandFilePath);
            string expectedResult = "dsapoiuybv" + Environment.NewLine +
                                    "cxz;lkjh54" + Environment.NewLine +
                                    "321/.,mntr" + Environment.NewLine +
                                    "ewq09876gf" + Environment.NewLine;
            string actualResult = string.Empty;

            isValid = transformationFiles.Validate(out errorMessages);
            transformationFiles.Initialize();
            transformationFiles.Operate();
            actualResult = transformationFiles.ToString();
            transformationFiles.SaveToDisk();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void OperateBiggerDimensionTest()
        {
            string textFilePath = this.GetFilePath("BiggerDimensionText.txt");
            string commandFilePath = this.GetFilePath("BiggerDimensionCommand.txt");
            IEnumerable<string> errorMessages;
            bool isValid = false;
            TransformationFiles transformationFiles = new TransformationFiles(textFilePath, commandFilePath);
            string expectedResult = "qbb0987654ds" + Environment.NewLine +
                                    "abcpoiuytrcx" + Environment.NewLine +
                                    "zbd;lkjhgf32" + Environment.NewLine +
                                    "1be/.,mnbvew" + Environment.NewLine +
                                    "qbf098765432" + Environment.NewLine +
                                    "1bapoiuytrew" + Environment.NewLine;
            string actualResult = string.Empty;

            isValid = transformationFiles.Validate(out errorMessages);
            transformationFiles.Initialize();
            transformationFiles.Operate();
            actualResult = transformationFiles.ToString();
            transformationFiles.SaveToDisk();

            Assert.AreEqual(expectedResult, actualResult);
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