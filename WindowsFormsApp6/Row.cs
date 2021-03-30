using System;


namespace WindowsFormsApp6
{
    [Serializable] public class Row
    {
        public int ID { get; set; }
        public int Numer { get; set; }
        public int Rok { get; set; }
        public string Seria { get; set; }
        public int Lp { get; set; }
        public string Towar { get; set; }
        public decimal Cena { get; set; }
        public decimal Ilosc { get; set; }
        public decimal Wartosc
        {
            get
            {
                return Cena * Ilosc;
            }
        }
    }
}
