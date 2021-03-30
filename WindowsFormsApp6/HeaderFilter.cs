using System;
using System.Collections.Generic;

namespace WindowsFormsApp6
{
    public class HeaderFilter
    {
        public bool EnableFilter { get; set; }
        public int Numer { get; set; }
        public int Rok { get; set; }
        public string Seria { get; set; }
        public DateTime Data { get; set; }
        public string Kontrahent { get; set; }
        public bool Zatwierdzony { get; set; }

        public List<Header> View(List<Header> headers)
        {
            if (Kontrahent == null) Kontrahent = "";
            if (Seria == null) Seria = "";

            if (EnableFilter == true)
            {
                return headers.FindAll(
                    h =>
                    (h.Zatwierdzony == Zatwierdzony)
                    &((Numer == 0) | (h.Numer == Numer))
                    &((Rok == 0) | (h.Rok == Rok))
                    &((Seria == "") | (h.Seria.Contains(Seria)))
                    &((Data == null) | (h.Data == Data))
                    &((Kontrahent == "") | (h.Kontrahent == Kontrahent))
                );
            }
            else
            {
                return headers;
            }

        }
    }
}
