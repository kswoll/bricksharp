namespace Bricksharp.Firmware.Display
{
    public static class LcdConsole
    {
        private static ConsoleWriter consoleWriter;

        public static void WriteLine(string format, params object[] arg)
        {
            if (consoleWriter == null)
                consoleWriter = new ConsoleWriter();

            lock (consoleWriter)
            {
                consoleWriter.WriteLine(string.Format(format, arg));
            }
        }

        public static void Clear()
        {
            if (consoleWriter == null)
                consoleWriter = new ConsoleWriter(); //will do a clear
            consoleWriter.Clear();
        }

        private class ConsoleWriter
        {
            private readonly Font font = Font.SmallFont;
            private float lineHeigth;
            private int lines;
            private Rectangle lineSize;
            private int scrollPos;

            public ConsoleWriter()
            {
                Reset();
            }

            private void Reset()
            {
                lines = (int)(Lcd.Height / font.MaxHeight);
                lineHeigth = (float)Lcd.Height / lines;
                lineSize = new Rectangle(new Point(0, 0), new Point(Lcd.Width, (int)font.MaxHeight));
            }

            public void WriteLine(string line)
            {
                var p = new Point(0, (int)(scrollPos * lineHeigth));
                Lcd.WriteTextBox(font, lineSize + p, line, true);
                scrollPos++;

                Lcd.Update((int)(scrollPos * lineHeigth));
                if (scrollPos >= lines)
                    scrollPos = 0;
            }

            public void Clear()
            {
                Lcd.Clear();
                Reset();
            }
        }
    }
}