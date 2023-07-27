using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraKonsolowaRPG
{
    public class Bohater // public by była dostępna wszędzie
    {
        public string Imie { get; set; }
        public int MaksymalneZycie { get; set; }
        public int PosiadaneZycie { get; set; }
        public int Obrazenia { get; set; }
        public int Level { get; set; }
        public int PunktyDoswiadczenia { get; set; }
        public int Sakwa { get; set; }
        public IBron NoszonaBron { get; set; }  // wgląd wszędzie, edycja tylko w ramach metody w tej klasie
        public Napiersnik NoszonyNapiersnik { get; set; }
        public Tarcza NoszonaTarcza { get; set; }


        //Konstruktor - tworzy obiekt, nazwa jak klasa
        public Bohater( string imie) // -  żeby stworzyć obiekt, musimy podać imie. A pozostałe parametry się podstawiają
        {
            Imie = imie;
            MaksymalneZycie = 10;
            PosiadaneZycie = 10;
            Level = 1;
            PunktyDoswiadczenia = 0;
            Sakwa = 15;
        }



        public void Przegrana()
        {
            if(PosiadaneZycie <= 0 )
            {
                Console.WriteLine("Poniosłeś klęskę");
            } 
        }
        public void Odpocznij()
        {
            Console.WriteLine("Rozbiłeś obóz, odnawiasz energię");
            PosiadaneZycie++;
            if (MaksymalneZycie < PosiadaneZycie)
                PosiadaneZycie = MaksymalneZycie;
            Console.WriteLine( Imie + "po odpoczynku,  posiada " + PosiadaneZycie + " punktów zdrowia" );
        }
        public void PokazPostac()
        {
            Console.WriteLine(Imie + " Lvl: " + Level);
            Console.WriteLine("Życie: " + PosiadaneZycie + "/" + MaksymalneZycie);
            Console.WriteLine("Sakwa: " + Sakwa + " golda");
            Console.WriteLine("Punkty Doświadczenia: " + PunktyDoswiadczenia + " !");
            if (NoszonaBron != null)
            {
                Console.WriteLine(NoszonaBron.Nazwa + " obrażenia " + NoszonaBron.ModyfikatorObrazen);
            }
            if (NoszonaTarcza != null)
            {
                Console.WriteLine(NoszonaTarcza.Nazwa + " obrona " + NoszonaTarcza.Obrona); ;
            }
            if (NoszonyNapiersnik != null)
            {
                Console.WriteLine(NoszonyNapiersnik.Nazwa + " obrona " + NoszonyNapiersnik.Obrona);
            }
        }
        public void KupBron(IBron bron)
        {
            if(bron.Cena <= Sakwa)
            {
                if(NoszonaBron != null && (NoszonaBron.MozliwoscNoszeniaTarczy == false && bron is Tarcza ))
                {
                    Console.WriteLine("Nie można kupić tarczy, do broni dwuręcznej");
                    return;
                }
                
                if( NoszonaTarcza != null && bron is BronDwureczna)
                {
                    Console.WriteLine("Nie można kupić miecza dwuręcznego do tarczy");
                    return;
                }


                Sakwa -= bron.Cena;
                NoszonaBron = bron;
                Console.WriteLine("Kupiłeś " + bron.Nazwa);
            }
            else
            {
                Console.WriteLine("Niewystarczające środki na zakup broni");
            }
        }
    }
}
