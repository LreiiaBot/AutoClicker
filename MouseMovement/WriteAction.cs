using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseMovement
{
    class WriteAction : InsertAction
    {
        public string Indicator { get; set; } = "W";
        public string Text { get; set; } = String.Empty;

        public WriteAction(string text)
        {
            Text = text;
        }

        public void Perform()
        {
            Write();
        }

        public void Write()
        {
            System.Windows.Forms.SendKeys.SendWait(Text);
        }

        public string ToCsv()
        {
            return $"{Indicator}{Csv.Separator}{Text}";
        }

        public override string ToString()
        {
            return ToCsv();
        }
    }
}
