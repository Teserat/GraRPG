using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraKonsolowaRPG
{
    public class Tarcza : Zbroja
    {
        public override int ModyfikujObrazenia(int obrazenia)
        {
            Zuzycie();
            Random losowanie = new Random(); //losowanie czy blok
            if (losowanie.Next(0,3) == 1)
            {
                return 0;
            }
            obrazenia -= Obrona;
            if (obrazenia <0)
            {
                obrazenia = 0;
            }

            return obrazenia;
        }
    }
}
