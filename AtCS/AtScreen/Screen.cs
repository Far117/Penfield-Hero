namespace AtCS.AtScreen
{
    public class Screen
    {
        private readonly int width, height;
        private char[,] lastFrame, buffer;
        private System.ConsoleColor[,] colorMask;

        public Screen(int width, int height)
        {
            this.width = width;
            this.height = height;

            this.lastFrame = new char[height, width];
            this.buffer = new char[height, width];
            this.colorMask = new System.ConsoleColor[height, width];

            this.ClearMask(System.ConsoleColor.Gray);

            return;
        }

        public char ReadAt(int x, int y)
        {
            return this.lastFrame[y, x];
        }

        public void Clear(char c)
        {
            for (int j = 0; j < this.height; j++)
                for (int i = 0; i < this.width; i++)
                    this.buffer[j, i] = c;

            return;
        }

        public void ClearMask(System.ConsoleColor color)
        {
            for (int j = 0; j < this.height; j++)
                for (int i = 0; i < this.width; i++)
                    this.colorMask[j, i] = color;

            return;
        }

        public void Draw()
        {
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    if (lastFrame[y, x] != buffer[y, x])
                    {
                        lastFrame[y, x] = buffer[y, x];

                        System.Console.SetCursorPosition(x, y);
                        System.Console.ForegroundColor = this.colorMask[y, x];
                        System.Console.Write(lastFrame[y, x]);
                    }
                }
            }

            return;
        }

        public bool PutChar(char c, int x, int y)
        {
            if (x >= this.width || x < 0 || y >= this.height || y < 0)
                return false;

            this.buffer[y, x] = c;
            this.colorMask[y, x] = System.ConsoleColor.Gray;
            return true;
        }

        public bool PutCharColor(char c, int x, int y, System.ConsoleColor col)
        {
            if (x >= this.width || x < 0 || y >= this.height || y < 0)
                return false;

            this.buffer[y, x] = c;
            this.colorMask[y, x] = col;
            return true;
        }

        public bool PutString(string str, int x, int y)
        {
            if (y < 0 || y >= this.height ||
                x < 0 || x + str.Length - 1 >= this.width)
                return false;

            foreach (char c in str)
            {
                this.PutChar(c, x, y);
                x++;
            }

            return true;
        }

        public bool PutStringColor(string str, int x, int y, System.ConsoleColor col)
        {
            if (y < 0 || y >= this.height ||
                x < 0 || x + str.Length - 1 >= this.width)
                return false;

            foreach (char c in str)
            {
                this.buffer[y, x] = c;
                this.colorMask[y, x] = col;
                x++;
            }

            return true;
        }

        public bool PutStringCenteredColor(string str, int y, System.ConsoleColor col)
        {
            if (str.Length > this.width - 1)
                return false;

            int x = this.width / 2 - str.Length / 2;
            return PutStringColor(str, x, y, col);
        }

        public bool PutInt(int i, int x, int y)
        {
            return this.PutString(i.ToString(), x, y);
        }

        public bool PutDouble(double d, int x, int y)
        {
            return this.PutString(d.ToString(), x, y);
        }

        public int GetWidth() { return this.width; }
        public int GetHeight() { return this.height; }
    }
}
