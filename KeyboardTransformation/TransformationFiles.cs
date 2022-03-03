using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace KeyboardTransformation
{
    public class TransformationFiles
    {
        protected DynamicTransformation dynamicTransformation { get; set; }
        protected TransformationCommands transformationCommands { get; set; }
        
        public TransformationFiles(string textFilePath, string commandFilePath)
        {
            this.dynamicTransformation = new DynamicTransformation(textFilePath);
            this.transformationCommands = new TransformationCommands(commandFilePath);
        }

        public bool Validate(out IEnumerable<string> messages)
        {
            ICollection<string> errorMessages = new List<string>();
            string message = string.Empty;
            bool isDynamicTransformationValid = default(bool);
            bool isTransformationCommandValid = default(bool);

            isDynamicTransformationValid = this.dynamicTransformation.Validate(out message);

            if (!isDynamicTransformationValid)
                errorMessages.Add(message);

            message = string.Empty;
            isTransformationCommandValid = this.transformationCommands.Validate(out message);

            if (!isTransformationCommandValid)
                errorMessages.Add(message);

            messages = errorMessages;

            return isDynamicTransformationValid && isTransformationCommandValid;
        }

        public void Initialize()
        {
            this.dynamicTransformation.ParseText();
            this.transformationCommands.LoadFile();
        }

        public void Operate()
        {
            foreach (string command in this.transformationCommands)
                if (this.transformationCommands.IsValidCommand(command))
                    if (this.transformationCommands.GetCommandType(command) == TransformationCommands.CommandType.Horizontal)
                        this.dynamicTransformation.HorizontalSwap();
                    else if (this.transformationCommands.GetCommandType(command) == TransformationCommands.CommandType.Vertical)
                        this.dynamicTransformation.VerticalSwap();
                    else if (this.transformationCommands.GetCommandType(command) == TransformationCommands.CommandType.Shift)
                        this.dynamicTransformation.Shift(Convert.ToInt32(command));
        }

        public override string ToString()
        {
            return this.dynamicTransformation.ToString();
        }

        public void SaveToDisk()
        {
            string currentAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace(@"file:\", string.Empty);

            if (currentAssemblyPath.IndexOf(@"\bin") != -1)
                currentAssemblyPath = Path.Combine(currentAssemblyPath.Substring(0, currentAssemblyPath.IndexOf(@"\bin")), @"TestTextFiles\Results");
            else
                currentAssemblyPath = Path.Combine(currentAssemblyPath, @"TestTextFiles\Results");

            currentAssemblyPath = Path.Combine(currentAssemblyPath, string.Concat("Result", DateTime.Now.ToString("MM-dd-yyyy-hh-mm-ss-fff"), ".txt"));

            using (StreamWriter sw = new StreamWriter(currentAssemblyPath))
                sw.WriteLine(this.ToString());
        }

        static void Main(string[] args)
        {
            string currentAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).Replace(@"file:\", string.Empty);
            string textFilePath = string.Empty;
            string commandFilePath = string.Empty;
            IEnumerable<string> errorList;

            if (currentAssemblyPath.IndexOf(@"\bin") != -1)
                currentAssemblyPath = Path.Combine(currentAssemblyPath.Substring(0, currentAssemblyPath.IndexOf(@"\bin")), @"TestTextFiles");
            else
                currentAssemblyPath = Path.Combine(currentAssemblyPath, @"TestTextFiles");

            textFilePath = Path.Combine(currentAssemblyPath, args[0]);
            commandFilePath = Path.Combine(currentAssemblyPath, args[1]);

            TransformationFiles transformationFiles = new TransformationFiles(textFilePath, commandFilePath);

            if (transformationFiles.Validate(out errorList))
            {
                transformationFiles.Initialize();
                transformationFiles.Operate();
                transformationFiles.SaveToDisk();

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("Here is the answer:");
                Console.WriteLine(string.Empty);
                Console.WriteLine(transformationFiles.ToString());
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("validation Errors have been encountered:");
                Console.WriteLine(string.Empty);

                foreach (string errorMessage in errorList)
                    Console.WriteLine(errorMessage);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
    }
}
