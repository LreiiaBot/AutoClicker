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
        private List<InsertAction> lAction;

        public List<InsertAction> LAction
        {
            get { return lAction; }
            set { lAction = value; }
        }

        public InsertAction Selected { get; set; } = null;
        public MainWindow()
        {
            InitializeComponent();
            LAction = Csv.ReadAll();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSelect(object sender, RoutedEventArgs e)
        {
            RemeberSelected(2000);
        }

        private async void RemeberSelected(int time)
        {
            await Task.Run(() => Thread.Sleep(time));
            var remember = System.Windows.Forms.Cursor.Position;
            LAction.Add(new MouseClick(new System.Drawing.Point(remember.X, remember.Y)));

            System.Windows.MessageBox.Show("Remembered Position");
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < LAction.Count; i++)
            {
                LAction[i].Perform();
            }
        }
        private void BtnDelete(object sender, RoutedEventArgs e)
        {

        }
        private void BtnWrite(object sender, RoutedEventArgs e)
        {
            try
            {
                LAction.Add(new WriteAction(tbWrite.Text));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void BtnDelay(object sender, RoutedEventArgs e)
        {
            try
            {
                LAction.Add(new Delay(Convert.ToInt32(tbDelay.Text)));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Csv.Save(LAction);
        }
    }
}
