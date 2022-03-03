using System;
using System.IO;
using System.Text;

namespace KeyboardTransformation
{
    public class DynamicTransformation : Transformation
    {
        protected int rows { get; set; }

        protected int columns { get; set; }

        public string TextFilePath { get; set; }
        public DynamicTransformation(int rows, int columns) : base()
        {
            this.rows = rows;
            this.columns = columns;
        }

        public DynamicTransformation(string textFilePath)
        {
            this.rows = default(int);
            this.columns = default(int);
            this.TextFilePath = textFilePath;
        }

        public bool Validate(out string message)
        {
            string line = string.Empty;

            this.rows = 0;
            this.columns = 0;

            if (this.TextFilePath == string.Empty)
            {
                message = "Text file path is empty.  You might be testing out of memory";
                return false;
            }

            if (new FileInfo(this.TextFilePath).Length == 0)
            {
                message = "Text file is empty";
                return false;
            }

            using (StreamReader textReader = new StreamReader(this.TextFilePath))
            {
                while (!textReader.EndOfStream)
                {
                    line = textReader.ReadLine();
                    
                    if (this.rows != 0 && this.columns != line.Length)
                    {
                        message = "Every line in the file has to be the same length";
                        return false;
                    }

                    this.columns = line.Length;
                    this.rows++;
                }
            }

            if (this.rows == 1)
            {
                message = "Text file must contain more than one line";
                return false;
            }

            message = string.Empty;
            return true;
        }

        public override void Shift(int shift)
        {
            char[] flattened = new char[base.keyboard.Length];
            char[] shifted = new char[base.keyboard.Length];
            int newIndex = default(int);
            int newRowIndex = default(int);
            int newColumnIndex = default(int);

            if (shift >= base.keyboard.Length)
                shift = shift % base.keyboard.Length;

            if (shift <= (base.keyboard.Length * -1))
            {
                shift = Math.Abs(shift);
                shift = shift % base.keyboard.Length;
                shift = shift * -1;
            }

            for (int i = 0; i < this.rows; i++)
                for (int j = 0; j < this.columns; j++)
                    flattened[(i * this.columns) + j] = base.keyboard[i, j];

            for (int i = 0; i < base.keyboard.Length; i++)
            {
                if (i + shift > (base.keyboard.Length - 1))
                    newIndex = i + shift - base.keyboard.Length;
                else if (i + shift < 0)
                    newIndex = base.keyboard.Length + i + shift;
                else
                    newIndex = i + shift;

                shifted[newIndex] = flattened[i];
            }

            for (int i = 0; i < base.keyboard.Length; i++)
            {
                newRowIndex = Convert.ToInt32(Math.Floor(Convert.ToDecimal(i) / Convert.ToDecimal(this.columns)));
                newColumnIndex = i % this.columns;

                base.keyboard[newRowIndex, newColumnIndex] = shifted[i];
            }
        }

        public override void HorizontalSwap()
        {
            char[,] newKeyboard = new char[this.rows, this.columns];
            int newColumnIndex = default(int);

            for (int i = 0; i < this.rows; i++)
            {
                for (int j = this.columns - 1; j >= 0; j--)
                {
                    newKeyboard[i, newColumnIndex] = base.keyboard[i, j];
                    newColumnIndex++;
                }

                newColumnIndex = 0;
            }

            base.keyboard = newKeyboard;
        }

        public override void VerticalSwap()
        {
            char[,] newKeyboard = new char[this.rows, this.columns];
            int newRowIndex = default(int);

            for (int i = this.rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < this.columns; j++)
                    newKeyboard[newRowIndex, j] = base.keyboard[i, j];

                newRowIndex++;
            }

            base.keyboard = newKeyboard;
        }

        public void ParseText()
        {
            string line = string.Empty;
            int rowIndex = default(int);
            int columnIndex = default(int);

            base.keyboard = new char[this.rows, this.columns];

            using (StreamReader textReader = new StreamReader(this.TextFilePath))
            {
                while (!textReader.EndOfStream)
                {
                    line = textReader.ReadLine();

                    foreach (char c in line)
                    {
                        base.keyboard[rowIndex, columnIndex] = c;
                        columnIndex++;
                    }

                    rowIndex++;
                    columnIndex = 0;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder keyboardBuilder = new StringBuilder();

            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                    keyboardBuilder.Append(this.keyboard[i, j]);

                keyboardBuilder.AppendLine();
            }

            return keyboardBuilder.ToString();
        }
    }
}
