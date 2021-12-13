using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MouseMovement
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static System.Drawing.Point Remember { get; set; } = default;
        public static List<System.Drawing.Point> LRemember { get; set; } = new List<System.Drawing.Point>();
        public MainWindow()
        {
            InitializeComponent();
            LRemember = Csv.ReadAll();
        }

        public static async void ClickInXSeconds(int s)
        {
            for (int i = 0; i < LRemember.Count; i++)
            {
                ClickPosition(LRemember[i]);
                await Task.Run(() => Thread.Sleep(s));
            }
        }

        private static void ClickPosition()
        {
            LeftMouseClick(Remember.X, Remember.Y);
        }
        private static void ClickPosition(System.Drawing.Point point)
        {
            LeftMouseClick(point.X, point.Y);
        }
        public static async void RememberPosition(int i)
        {
            await Task.Run(() => Thread.Sleep(i));
            Remember = System.Windows.Forms.Cursor.Position;
            //Csv.Save(Remember);
            LRemember.Add(new System.Drawing.Point(Remember.X, Remember.Y));


            System.Windows.MessageBox.Show("Remembered Position");
        }

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Remember = Csv.Read();
        }

        private void BtnSelect(object sender, RoutedEventArgs e)
        {
            RememberPosition(2000);
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            ClickInXSeconds(800);
        }
        private void BtnDelete(object sender, RoutedEventArgs e)
        {
            LRemember.Clear();
        }
        private void BtnWrite(object sender, RoutedEventArgs e)
        {
            Write();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Csv.Save(LRemember);
        }

        private async void Write()
        {

            await Task.Run(() => Thread.Sleep(800));
            string s = tbWrite.Text;
            System.Windows.Forms.SendKeys.SendWait(s);
        }
    }
}
