using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MouseMovement
{
    class Delay : InsertAction
    {
        public string Indicator { get; set; } = "D";
        public int Time { get; set; } = 1000;

        public Delay(int time)
        {
            Time = time;
        }

        public void Perform()
        {
            Sleep();
        }

        public async void Sleep()
        {
            //await Task.Run(() => Thread.Sleep(Time));
            Thread.Sleep(Time);
        }

        public string ToCsv()
        {
            return $"{Indicator}{Csv.Separator}{Time}";
        }
        public override string ToString()
        {
            return ToCsv();
        }
    }
}
