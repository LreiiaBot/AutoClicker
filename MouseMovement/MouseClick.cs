using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseMovement
{
    class MouseClick : InsertAction
    {
        public string Indicator { get; set; } = "M";
        public System.Drawing.Point Position { get; set; } = new System.Drawing.Point(0, 0);

        public MouseClick()
        {

        }

        public MouseClick(System.Drawing.Point p)
        {
            Position = p;
        }

        public void Perform()
        {
            ClickPosition();
        }

        public string ToCsv()
        {
            return $"{Indicator}{Csv.Separator}{Position.X}{Csv.Separator}{Position.Y}";
        }

        private void ClickPosition()
        {
            LeftMouseClick(Position.X, Position.Y);
        }
        public override string ToString()
        {
            return ToCsv();
        }

        #region lowlevel

        //This is a replacement for Cursor.Position in WinForms
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        //This simulates a left mouse click
        public static void LeftMouseClick(int xpos, int ypos)
        {
            SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        #endregion
    }
}
