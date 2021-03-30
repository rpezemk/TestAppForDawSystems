using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace WindowsFormsApp6
{
    [Serializable] public class Header
    {
        public int ID { get; set; }
        public int Numer { get; set; }
        public int Rok { get; set; }
        public string Seria { get; set; }
        public DateTime Data { get; set; }
        public string Kontrahent { get; set; }
        public decimal Wartosc { get; set; }
        public bool Zatwierdzony { get; set; }
        public string PelnyNumer {
            get
            {
                return Numer.ToString("D4") + "/" + (Rok % 100).ToString() + "/" + Seria;
            }
            set { }
        }

        public Header()
        {
            ID = 0;
            Numer = 0;
            Rok = 0;
            Seria = "";
            Data = DateTime.MinValue;
            Kontrahent = "";
            Wartosc = 0.0M;
            Zatwierdzony = false;
        }
    }


}
