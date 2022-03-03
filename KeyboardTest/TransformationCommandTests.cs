using KeyboardTransformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;

namespace KeyboardTest
{
    [TestClass()]
    public class TransformationCommandTests
    {
       
        [TestMethod()]
        public void ValidateCommandFileIsEmptyTest()
        {
            string errorMessage = string.Empty;
            string commandFilePath = this.GetFilePath("EmptyTextFile.txt");
            bool isValid = true;
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);

            isValid = transformationCommands.Validate(out errorMessage);

            Assert.IsFalse(isValid);
            Assert.AreSame(errorMessage, "Command file is empty");
        }

        [TestMethod()]
        public void ValidateCommandFileIsMoreThanOneLine()
        {
            string errorMessage = string.Empty;
            string commandFilePath = this.GetFilePath("TransformationCommandMoreThanOneLine.txt");
            bool isValid = true;
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);

            isValid = transformationCommands.Validate(out errorMessage);

            Assert.IsFalse(isValid);
            Assert.AreSame(errorMessage, "Command file must contain more than one line");
        }

        [TestMethod()]
        public void ValidateCommandFileNoValidCommand()
        {
            string errorMessage = string.Empty;
            string commandFilePath = this.GetFilePath("TransformationCommandNoValidCommand.txt");
            bool isValid = true;
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);

            isValid = transformationCommands.Validate(out errorMessage);

            Assert.IsFalse(isValid);
            Assert.AreSame(errorMessage, "The command file does not contain any valid commands");
        }

        [TestMethod()]
        public void ValidateCommandFileNoValidCommandOneCommand()
        {
            string errorMessage = string.Empty;
            string commandFilePath = this.GetFilePath("TransformationCommandNoValidCommandOneCommand.txt");
            bool isValid = true;
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);

            isValid = transformationCommands.Validate(out errorMessage);

            Assert.IsFalse(isValid);
            Assert.AreSame(errorMessage, "The command file does not contain any valid commands");
        }

        [TestMethod()]
        public void ValidateCommandFileSuccess()
        {
            string errorMessage = string.Empty;
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            bool isValid = false;
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);

            isValid = transformationCommands.Validate(out errorMessage);

            Assert.IsTrue(isValid);
            Assert.AreSame(errorMessage, string.Empty);
        }

        [TestMethod()]
        public void ValidateCommandFileSuccessOneCommandAlpha()
        {
            string errorMessage = string.Empty;
            string commandFilePath = this.GetFilePath("TransformationCommandOneCommandAlpha.txt");
            bool isValid = false;
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);

            isValid = transformationCommands.Validate(out errorMessage);

            Assert.IsTrue(isValid);
            Assert.AreSame(errorMessage, string.Empty);
        }

        [TestMethod()]
        public void ValidateCommandFileSuccessOneCommandNumeric()
        {
            string errorMessage = string.Empty;
            string commandFilePath = this.GetFilePath("TransformationCommandOneCommandNumeric.txt");
            bool isValid = false;
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);

            isValid = transformationCommands.Validate(out errorMessage);

            Assert.IsTrue(isValid);
            Assert.AreSame(errorMessage, string.Empty);
        }

        [TestMethod()]
        public void IsValidCommandVerticalTest()
        {
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);
            bool isValid = false;

            isValid = transformationCommands.IsValidCommand("V");

            Assert.IsTrue(isValid);
        }

        [TestMethod()]
        public void IsValidCommandHorizontalTest()
        {
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);
            bool isValid = false;

            isValid = transformationCommands.IsValidCommand("H");

            Assert.IsTrue(isValid);
        }

        [TestMethod()]
        public void IsValidCommandShiftNumericTest()
        {
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);
            bool isValid = false;

            isValid = transformationCommands.IsValidCommand("12");

            Assert.IsTrue(isValid);
        }

        [TestMethod()]
        public void IsValidCommandFailTest()
        {
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);
            bool isValid = true;

            isValid = transformationCommands.IsValidCommand("C");

            Assert.IsFalse(isValid);
        }

        [TestMethod()]
        public void GetCommandTypeHorizontalTest()
        {
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);
            TransformationCommands.CommandType commandType;

            commandType = transformationCommands.GetCommandType("H");

            Assert.AreEqual(commandType, TransformationCommands.CommandType.Horizontal);
        }

        [TestMethod()]
        public void GetCommandTypeVerticalTest()
        {
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);
            TransformationCommands.CommandType commandType;

            commandType = transformationCommands.GetCommandType("V");

            Assert.AreEqual(commandType, TransformationCommands.CommandType.Vertical);
        }

        [TestMethod()]
        public void GetCommandTypeShiftTest()
        {
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);
            TransformationCommands.CommandType commandType;

            commandType = transformationCommands.GetCommandType("12");

            Assert.AreEqual(commandType, TransformationCommands.CommandType.Shift);
        }

        [TestMethod()]
        public void GetCommandTypeNoneTest()
        {
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);
            TransformationCommands.CommandType commandType;

            commandType = transformationCommands.GetCommandType("C");

            Assert.AreEqual(commandType, TransformationCommands.CommandType.None);
        }

        [TestMethod()]
        public void LoadFileTest()
        {
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);

            transformationCommands.LoadFile();

            Assert.AreEqual(5, transformationCommands.Commands.Count);
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            string commandFilePath = this.GetFilePath("TransformationCommand.txt");
            TransformationCommands transformationCommands = new TransformationCommands(commandFilePath);
            int totalCount = default(int);

            transformationCommands.LoadFile();

            foreach (string command in transformationCommands)
                totalCount++;

            Assert.AreEqual(5, totalCount);
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