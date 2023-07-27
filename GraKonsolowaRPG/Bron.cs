using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraKonsolowaRPG
{
    public class Bron : IBron //Dziedziczenie po Interfejscie IBroń, dodatkowo trzeba zaimplementować Oblicz obrażenia
    {
        public string Nazwa { get; set; }
        public int Cena { get; set; }
        public int ModyfikatorObrazen { get; set; }
        public bool MozliwoscNoszeniaTarczy
        {
            get
            {
                return true;
            }
        }

        public Bron(string nazwa, int cena, int modyfikatorObrazen) // konstruktor
        {
            Nazwa = nazwa;
            Cena = cena;
            ModyfikatorObrazen = modyfikatorObrazen;

        }


        public int ObliczObrazenia()  // dziedziczone po interfejcie, dodane zwrócenie modyfikatoraobrażen 
        {
            return ModyfikatorObrazen;
        }
    }
}
