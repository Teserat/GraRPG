﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraKonsolowaRPG
{
    internal class BronDwureczna : IBron // Implementacja interfejsu poprzez dziedziczenie
    {
        public string Nazwa { get; set; }
        public int Cena { get; set; }
        public int ModyfikatorObrazen { get; set; }
        public bool MozliwoscNoszeniaTarczy
        {
            get
            {
                return false;
            }
        }

        public BronDwureczna(string nazwa, int cena, int modyfikatorObrazen) // konstruktor
        {
            Nazwa = nazwa;
            Cena = cena;
            ModyfikatorObrazen = modyfikatorObrazen;

        }

        public int ObliczObrazenia()
        {
            return ModyfikatorObrazen *3; // Modyfikator obrażeń większy, by był odzwierciedleniem broni dwuręcznej
        }
    }
}
