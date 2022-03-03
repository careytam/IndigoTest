using System;
using System.Text;

namespace KeyboardTransformation
{
    public class Transformation
    {
        protected char[,] keyboard { get; set; }

        public Transformation()
        {
            this.keyboard = new char[,] {
                { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'},
                { 'q','w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' },
                { 'a','s', 'd', 'f', 'g', 'h', 'j', 'k', 'l', ';' },
                { 'z','x', 'c', 'v', 'b', 'n', 'm', ',', '.', '/' }
            };
        }

        public virtual void Shift(int shift)
        {
            //[0, 0] = (0 * 10) + (0 + 1) = 1
            //[1, 0] = (1 * 10) + (0 + 1) = 11
            //[11 / 10 = 1.1, Floor(1.1) = 1, 11 MOD 10 = 1

            char[] flattened = new char[40];
            char[] shifted = new char[40];
            int newIndex = default(int);
            int newRowIndex = default(int);
            int newColumnIndex = default(int);

            if (shift >= 40)
                shift = shift % 40;

            if (shift <= -40)
            {
                shift = Math.Abs(shift);
                shift = shift % 40;
                shift = shift * -1;
            }

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 10; j++)
                    flattened[(i * 10) + j] = this.keyboard[i, j];

            for (int i = 0; i < 40; i++)
            {
                if (i + shift > 39)
                    newIndex = i + shift - 40;
                else if (i + shift < 0)
                    newIndex = 40 + i + shift;
                else
                    newIndex = i + shift;

                shifted[newIndex] = flattened[i];
            }

            for (int i = 0 ; i < 40; i++)
            {
                newRowIndex = Convert.ToInt32(Math.Floor(Convert.ToDecimal(i) / 10m));
                newColumnIndex = i % 10;

                this.keyboard[newRowIndex, newColumnIndex] = shifted[i];
            }
        }

        public virtual void HorizontalSwap()
        {
            char swap = default(char);

            for (int i = 0; i < 4; i++)
            {
                swap = this.keyboard[i, 0];
                this.keyboard[i, 0] = this.keyboard[i, 9];
                this.keyboard[i, 9] = swap;

                swap = this.keyboard[i, 1];
                this.keyboard[i, 1] = this.keyboard[i, 8];
                this.keyboard[i, 8] = swap;

                swap = this.keyboard[i, 2];
                this.keyboard[i, 2] = this.keyboard[i, 7];
                this.keyboard[i, 7] = swap;

                swap = this.keyboard[i, 3];
                this.keyboard[i, 3] = this.keyboard[i, 6];
                this.keyboard[i, 6] = swap;

                swap = this.keyboard[i, 4];
                this.keyboard[i, 4] = this.keyboard[i, 5];
                this.keyboard[i, 5] = swap;
            }
        }

        public virtual void VerticalSwap()
        {
            char swap = default(char);

            for (int i = 0; i < 10; i++)
            {
                swap = this.keyboard[0, i];
                this.keyboard[0, i] = this.keyboard[3, i];
                this.keyboard[3, i] = swap;

                swap = this.keyboard[1, i];
                this.keyboard[1, i] = this.keyboard[2, i];
                this.keyboard[2, i] = swap;
            }
        }

        public override string ToString()
        {
            StringBuilder keyboardBuilder = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 10; j++)
                    keyboardBuilder.Append(this.keyboard[i, j]);
                
                keyboardBuilder.AppendLine();
            }

            return keyboardBuilder.ToString();
        }
    }
}
