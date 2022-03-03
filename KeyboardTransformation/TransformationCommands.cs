using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KeyboardTransformation
{
    public class TransformationCommands : IEnumerable<string>
    {
        public ICollection<string> Commands { get; set; }
        public enum CommandType { None = 0, Vertical = 1, Horizontal = 2, Shift = 3 }

        protected string commandFilePath { get; set; }

        protected ICollection<string> validCommands { get; set; }

        public TransformationCommands(string commandFilePath)
        {
            this.commandFilePath = commandFilePath;
            validCommands = new List<string>() { "H", "V" };
        }

        public bool Validate(out string message)
        {
            int lineCounter = default(int);
            string line = string.Empty;
            ICollection<string> checkCommand;
            bool matched = false;

            if (new FileInfo(this.commandFilePath).Length == 0)
            {
                message = "Command file is empty";
                return false;
            }

            using (StreamReader textReader = new StreamReader(this.commandFilePath))
            {
                while (!textReader.EndOfStream)
                {
                    line = textReader.ReadLine();
                    lineCounter++;
                }
            }

            if (lineCounter > 1)
            {
                message = "Command file must contain more than one line";
                return false;
            }

            checkCommand = line.Split(',');

            foreach (string command in checkCommand)
            {
                if (this.IsValidCommand(command))
                { 
                    matched = true;
                    break;
                }
            }

            if (!matched)
            {
                message = "The command file does not contain any valid commands";
                return false;
            }

            message = string.Empty;
            return true;
        }

        public bool IsValidCommand(string command)
        {
            return this.validCommands.Contains(command) || int.TryParse(command, out _);
        }

        public CommandType GetCommandType(string command)
        {
            if (command == "H")
                return CommandType.Horizontal;
            else if (command == "V")
                return CommandType.Vertical;
            else if (int.TryParse(command, out _))
                return CommandType.Shift;
            else
                return CommandType.None;
        }

        public void LoadFile()
        {
            string line = string.Empty;

            using (StreamReader textReader = new StreamReader(this.commandFilePath))
            {
                while (!textReader.EndOfStream)
                    line = textReader.ReadLine();

                this.Commands = line.Split(',').ToList();
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Commands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Commands).GetEnumerator();
        }
    }
}
