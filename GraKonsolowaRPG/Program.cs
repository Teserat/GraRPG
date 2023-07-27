using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraKonsolowaRPG
{
    internal class Program
    {
        private static Bohater _bohater; // <-- Zmienna złożona , zmienna globalna prywatna(taki zapis _bohater)
        private static List<IBron> _bronie;
        private static List<Zbroja> _zbroje;

        static void Main(string[] args) // <-- Metoda
        {
            StworzBronie();
            ObsługaMenu();
        }

        static void StworzBronie()
        {
            _bronie = new List<IBron>(); //deklaracja listy dla obiektu Bron
            _zbroje = new List<Zbroja>();

            Bron bron = new Bron("Kijek rozpaczy", 3, 4);
            _bronie.Add(bron);
            _bronie.Add(new Bron("Miecz Alibaby", 8, 6));
            _bronie.Add(new Bron("Klucz Francuski", 10, 10));
            _bronie.Add(new BronDwureczna("Gigantyczny otwieracz do puszek", 25, 4));

            _zbroje.Add(new Tarcza
            {
                Nazwa = "Tarcza Niebios",
                Obrona = 10,
                PoziomZuzycia = 0,
            }
            );

            Napiersnik napiersnik = new Napiersnik();
            napiersnik.Nazwa = "Zbroja Wojownika Niebios";
            napiersnik.Obrona = 15;
            napiersnik.PoziomZuzycia = 0;
            _zbroje.Add(napiersnik);
        }

        static void ObsługaMenu()
        {

            Console.WriteLine("1. Nowa gra");
            Console.WriteLine("2. Wczytaj gre");
            Console.WriteLine("3. Zakończ");
            string opcja = Console.ReadLine();

            if (opcja == "1")
            {
                StworzPostac();
            }
            else if (opcja == "2")
            {
                // do uzupełnienia
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Dzięki za grę");
                return; // <-- zamyka metode
            }
            
            while (opcja != "5")
            {
                MenuGry();
                opcja = Console.ReadLine();

                if(opcja == "0")
                {
                    _bohater.PokazPostac();
                }
                else if(opcja == "1")
                {
                    IdzNaWyprawe();
                }
                else if(opcja == "2")
                {
                    _bohater.Odpocznij();
                }
                else if(opcja == "3")
                {

                }
                else if(opcja =="4")
                {
                    Sklep();
                }
                _bohater.Przegrana();

                Czekanie();
            }

        }
         static void MenuGry()
            {
            Console.Clear();
            Console.WriteLine("0. Statystyki postaci");
            Console.WriteLine("1. Idź na wyprawę");
            Console.WriteLine("2. Odpocznij");
            Console.WriteLine("3. Ekwipunek");
            Console.WriteLine("4. Sklep");
            Console.WriteLine("5. Koniec");
        }

        static void StworzPostac()
        {
            Console.Clear();
            Console.Write("Podaj imię postaci: ");
            string imie = Console.ReadLine();
            _bohater = new Bohater(imie); // <-- deklaracja obiektu ,  w tym momencie Bohater się materializuje
        }
        static void IdzNaWyprawe()
        {
            Console.Clear();
            Console.WriteLine("Wyruszyłeś na wyprawę");
            bool wynikWalki = Walka();

            if(wynikWalki) 
            {
                BonusyZaZwyciestwo();
            }

        }
        static void BonusyZaZwyciestwo()
        {
            Console.Clear();
            Console.WriteLine( "ZWYCIĘSTWO!" );
            _bohater.Sakwa += 5;

        }
        static bool Walka()
        {
            Random losuj = new Random();
            int zyciePrzeciwnika = losuj.Next(8, 12);

            while (_bohater.PosiadaneZycie > 0)
            {
                if (_bohater.NoszonaBron == null) //Naprawa braku broni w ekwipunku
                {
                    Console.WriteLine("\n Ups, brak broni w ekwipunku! Wracasz w te pędy z wyprawy!" );
                    break;
                }
                int obrazenia = _bohater.NoszonaBron.ObliczObrazenia();
                int obrazeniaZadane = losuj.Next(obrazenia-2, obrazenia+2);
                zyciePrzeciwnika -= obrazeniaZadane;

                if (zyciePrzeciwnika <= 0)  // Czyli zwycięstwo!! <- Dodałem extra nagrodę
                {
                    {
                        _bohater.PunktyDoswiadczenia += losuj.Next(_bohater.PosiadaneZycie, _bohater.MaksymalneZycie) ;
                        if (_bohater.PunktyDoswiadczenia > _bohater.MaksymalneZycie * (10 * _bohater.Level))
                        {
                            ++_bohater.Level;
                            Console.Clear();
                            Console.WriteLine("Bohater " + _bohater.Imie + " osiągną poziom " + _bohater.Level + " !");
                            _bohater.MaksymalneZycie += 5;
                            Czekanie();
                        }
                    }
                    return true;
                }
                    

                int obrazeniaOtrzymane = losuj.Next(0, 4);
                _bohater.PosiadaneZycie -= obrazeniaOtrzymane;
            }
            return false;

        }
        static void Sklep()
        {
            int licznik = 1;
            Console.Clear();
            foreach (IBron bron in _bronie)
            {
                Console.WriteLine(licznik + ". " + bron.Nazwa);
                licznik++;
            }
            foreach(Zbroja zbroja in _zbroje)
            {
                Console.WriteLine(licznik + ". " + zbroja.Nazwa);
                licznik++;
            }

            Console.WriteLine("Wybierz bron: ");
            string odczyt = Console.ReadLine();
            int opcja = int.Parse(odczyt);

            if (opcja <= _bronie.Count)
            {
                IBron wybranaBron = _bronie[opcja - 1];
                _bohater.KupBron(wybranaBron);
            }else
            {
                opcja -= _bronie.Count;
                Zbroja wybranaZbroja = _zbroje[opcja - 1];
                if (_bohater.NoszonaBron != null && (_bohater.NoszonaBron.MozliwoscNoszeniaTarczy == false && wybranaZbroja is Tarcza))
                {
                    Console.WriteLine("Nie można kupić tarczy, do broni dwuręcznej");
                    return;
                }
                if (wybranaZbroja is Tarcza) // zapytanie o szczegółowość tarczy
                {//"is" "as" łączą się razem
                    _bohater.NoszonaTarcza = wybranaZbroja as Tarcza;
                }
                else // przypisanie do klasy Napierśnik, jeśli nie Tarcza
                {
                    //_bohater.NoszonyNapiersnik = wybranaZbroja as Napiersnik;
                    // druga możliwość zapisu rzutowania, działa tak samo 
                     _bohater.NoszonyNapiersnik = (Napiersnik)wybranaZbroja;
                }
            }

        }
        static void Czekanie()
        {
            Console.WriteLine();
            Console.WriteLine("Naciśnij dowolny klawisz by kontynuowac...");
            Console.ReadLine();
        }
    }
}


/* Notatki:
 * kwalfikator tekstu - ""
 * intelisense -  forma automatycznego uzupełniania zawartego w Microsoft Visual Studio oraz Visual Studio Code
 * plik *.pdb - zawiera informacje o błędach 
 * string napis = ... <- mała litera zmiennej (n)  to zmienna lokalna, czyli dostępna tylko w danym zakresie
 * if - jest warunkiem
 * wszystko co ma wąsy w sobie, nie ma średnika np if (){}
 * Parsowanie i ToString
 * Refaktoring - przerabianie kodu by był lepszy, bardziej optymalny czytelniejszy
 * static? metoda statuczna?
 * return w metodzie służy do zakończenia metody dodatkowo zwraca wynik
 * Klasa a obiekt
 * 
 * Wszystko w Interfejsie jest publiczne (nie podaje się public czy private)
 * Interfejs to same "nagłówki", żadnej implementacji. Użytkownik żeby mógł coś użyć, musi zaimplementować "to" i "to"  
 * Budowa klasy Parametry na górze Konstruktor i potem metody
 * 
 * intelisense - > control spacja uzupełnia (warto poszukać więcej)
 * 
 * interfejs jakby formularz osoby, klasa jak osoba
 * 
 * Interfejs - odwracanie zależności - Obiekt decyduje jak się zachowa mechanizm a nie klasa
 *      Interfejs mówi z czego można używać, klasa  musi to spełnić. ALE nie można użyć z klasy niczego, czego nie ma w interfejscie
 * obiekty typu interfejs, Interfejs połączył broń 2rećzną i jednoręczną 
 * Lista też może wykożystywać Interfejs (pewnie więcej rzeczy to może robić)
 * 
 * Klasa abstrakcyjna - pomiędzyinterfejsem, a klasą. JEŚLI CHCEMY BY JAKAŚ METODA BYŁA SPECYFICZNA DLA KAŻDEJ KLASY DAJEMY NAGŁÓWEK, JAK DLA KAŻDEJ TAK SAMO TO KLASA ABSTRAKCYJNA
 * posiada implementacje, co odróznia ją od interfejsu (jest też kompilowalna jak klasa)
 * 
 *     
 */