using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraKonsolowaRPG
{
    public interface IBron
    {
        string Nazwa { get; set; }
        int Cena { get; set; }
        int ModyfikatorObrazen { get; set; }
        bool MozliwoscNoszeniaTarczy { get; } // tylko możliwość pobierania
        int ObliczObrazenia();
    }
}
