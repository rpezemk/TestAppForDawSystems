using System;
using System.Collections.Generic;

namespace WindowsFormsApp6
{
    public class RowFilter
    {
        public bool EnableFilter { get; set; }

        public int ID { get; set; }
        public int Numer { get; set; }
        public string Towar { get; set; }
        public decimal Cena { get; set; }

        public List<Row> View(List<Row> rows)
        {

            if(EnableFilter == false)
            {
                return rows.FindAll(r => (r.ID == ID) & (r.Numer == Numer));
            }
            else
            {
                if(Towar == null)
                {
                    Towar = "";
                }
                return rows.FindAll(
                    r =>
                    (r.ID == ID) & (r.Numer == Numer)
                    & ((Towar == "") | (r.Towar.Contains(Towar)))
                    &((Cena == 0.0M) | (r.Cena == Cena))
                );
            }
            
        }
    }

}
