using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseMovement
{
    public interface InsertAction
    {
        string Indicator { get; set; }
        void Perform();
        string ToCsv();
        string ToString();
    }
}
