namespace Bazooka.Core
{
    using System;
    using System.Management.Automation.Host;

    /// <summary>
    ///     Fake user raw interface for Powershell host
    /// </summary>
    class HostRawUserInterface : PSHostRawUserInterface
    {
        public override KeyInfo ReadKey(ReadKeyOptions options)
        {
            throw new NotImplementedException();
        }

        public override void FlushInputBuffer()
        {
        }

        public override void SetBufferContents(Coordinates origin, BufferCell[,] contents)
        {
            throw new NotImplementedException();
        }

        public override void SetBufferContents(Rectangle rectangle, BufferCell fill)
        {
            throw new NotImplementedException();
        }

        public override BufferCell[,] GetBufferContents(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }

        public override void ScrollBufferContents(Rectangle source, Coordinates destination, Rectangle clip, BufferCell fill)
        {
            throw new NotImplementedException();
        }

        public override ConsoleColor ForegroundColor
        {
            get { return Console.ForegroundColor; }
            set { Console.ForegroundColor = value; }
        }

        public override ConsoleColor BackgroundColor
        {
            get { return Console.BackgroundColor; }
            set { Console.BackgroundColor = value; }
        }

        public override Coordinates CursorPosition
        {
            get { return new Coordinates(Console.CursorLeft, Console.CursorTop); }
            set { Console.SetCursorPosition(value.X, value.Y); }
        }

        public override Coordinates WindowPosition
        {
            get { return new Coordinates(Console.WindowLeft, Console.WindowTop); }
            set { Console.SetWindowPosition(value.X, value.Y); }
        }

        public override int CursorSize
        {
            get { return Console.CursorSize; }
            set { Console.CursorSize = value; }
        }

        public override Size BufferSize
        {
            get { return new Size(Console.BufferWidth, Console.BufferHeight); }
            set { Console.SetBufferSize(value.Width, value.Height); }
        }

        public override Size WindowSize
        {
            get { return new Size(Console.WindowWidth, Console.WindowHeight); }
            set { Console.SetWindowSize(value.Width, value.Height); }
        }

        public override Size MaxWindowSize
        {
            get { return new Size(Console.LargestWindowWidth, Console.LargestWindowHeight); }
        }

        public override Size MaxPhysicalWindowSize
        {
            get { return new Size(Console.LargestWindowWidth, Console.LargestWindowHeight); }
        }

        public override bool KeyAvailable
        {
            get { return Console.KeyAvailable; }
        }

        public override string WindowTitle
        {
            get { return Console.Title; }
            set { Console.Title = value; }
        }
    }
}
