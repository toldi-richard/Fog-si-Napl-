using System;
using System.Collections.Generic;

namespace Hashelés
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> jelszavak = new List<string>();

            jelszavak.Add("admin1987");
            jelszavak.Add("Agoston21");
            jelszavak.Add("ADA912jja12..");
            jelszavak.Add("Klarika12");
            jelszavak.Add("ufsa5dwf21");
            jelszavak.Add("Jancsika1220");
            jelszavak.Add("balazs1230");
            jelszavak.Add("DA..23ADaad2cafA13*_");
            jelszavak.Add("uafr6awwd");
            jelszavak.Add("dader7ADF.._");
            jelszavak.Add("u34Ar9dsg");
            jelszavak.Add("ASDer1165");
            jelszavak.Add("uad10amsk");
            jelszavak.Add("uasdr8dssg");
            jelszavak.Add("uad10amsk");


            foreach (var item in jelszavak)
            {
                Console.WriteLine(item + " jelszó hashelve: ");
                Console.WriteLine(BCrypt.Net.BCrypt.HashPassword(item)+"\n");
            }
        }
    }
}
